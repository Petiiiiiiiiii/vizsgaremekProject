using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class LobbyHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Canvas LobbyUI;
    [SerializeField] Canvas SkillsUI;
    [SerializeField] Canvas PlayUI;

    private string username;
    public TextMeshProUGUI levelAndName;
    public TextMeshProUGUI sp;
    public TextMeshProUGUI playmenuLevel;
    public TextMeshProUGUI playmenuName;
    public TextMeshProUGUI skillsmenuName;
    public TextMeshProUGUI skillsmenuLevel;


    private void Start()
    {
        username = PlayerPrefs.GetString("Username");
        StartCoroutine(Upload());
    }
    public void PlayButton() 
    {
        StartCoroutine(WaitForAnimPlay(true));  
    }

    public void PlayBackButton() 
    {
        StartCoroutine(WaitForAnimPlay(false));
    }

    public void SkillsButton() 
    {
        StartCoroutine(WaitForAnimSkills(true));
    }

    public void SkillsBackButton() 
    {
        StartCoroutine(WaitForAnimSkills(false));
    }

    IEnumerator WaitForAnimSkills(bool value) 
    {
        if (value)
        {
            animator.SetBool("Skills",value);
            LobbyUI.gameObject.SetActive(!value);
            yield return new WaitForSeconds(2.6f);
            SkillsUI.gameObject.SetActive(value);
        }
        else 
        {
            animator.SetBool("Skills", value);
            SkillsUI.gameObject.SetActive(value);
            yield return new WaitForSeconds(2.6f);
            LobbyUI.gameObject.SetActive(!value);
        }
    }
    IEnumerator WaitForAnimPlay(bool value)
    {
        if (value)
        {
            animator.SetBool("Play", value);
            LobbyUI.gameObject.SetActive(!value);
            yield return new WaitForSeconds(1f);
            PlayUI.gameObject.SetActive(value);
        }
        else 
        {
            animator.SetBool("Play", value);
            PlayUI.gameObject.SetActive(value);
            yield return new WaitForSeconds(1f);
            LobbyUI.gameObject.SetActive(!value);
        }

    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/dungeonmaster/lobby_start_query.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Hálózati hiba: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            string level = response.Split(';')[0];

            levelAndName.text = $"Level {level} - {username}";
            sp.text = $"{PlayerPrefs.GetInt("SP")} SP";

            playmenuLevel.text = level;
            playmenuName.text = username;
            skillsmenuLevel.text = level;
            skillsmenuName.text = username;

            PlayerPrefs.SetString("Permission", response.Split(';')[1]);
        }
    }
}
