using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LoginController : MonoBehaviour
{
    private TMP_InputField nameInput, passInput;
    private Button logBTN;
    public GameObject warningText;
    private void Start()
    {
        nameInput = GameObject.Find("nameINPUT").GetComponent<TMP_InputField>();
        passInput = GameObject.Find("passINPUT").GetComponent<TMP_InputField>();
        logBTN = GameObject.Find("LoginBTN").GetComponent<Button>();
    }
    private class user
    {
        public string username;
        public string passwordHash;
    }
    public void CallLogin() 
    {
        StartCoroutine(Login());
        logBTN.interactable = false;
    }

    IEnumerator Login()
    {
        user user = new();

        user.username = nameInput.text;
        user.passwordHash = passInput.text;

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost:7000/api/Players/login",JsonUtility.ToJson(user),"application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            StartCoroutine(Warning("Something went wrong, try again later!", false));
        }
        else
        {
            switch (www.responseCode)
            {
                case 200:
                    StartCoroutine(Warning("Successful login!",true));
                    var player = JsonConvert.DeserializeObject<PlayerModel>(www.downloadHandler.text);
                    PlayerPrefs.SetString("Username", player.username);
                    PlayerPrefs.SetInt("playerLevel", player.level);
                    PlayerPrefs.SetInt("Permission", player.permission);
                    PlayerPrefs.SetInt("SP", player.sp);
                    PlayerPrefs.SetInt("playerID", player.playerId);
                    PlayerPrefs.SetString("playerEmail", player.email);
                    PlayerPrefs.SetString("passwordHash", player.passwordHash);
                    PlayerPrefs.SetString("regDate", player.regDate.ToString());

                    PlayerPrefs.SetString("dmg_boost", "locked");
                    PlayerPrefs.SetString("hp_boost", "locked");
                    PlayerPrefs.SetString("headshot_boost", "locked");
                    PlayerPrefs.SetString("firerate_boost", "locked");
                    PlayerPrefs.SetString("movement_boost", "locked");
                    PlayerPrefs.SetString("mag_boost", "locked");
                    PlayerPrefs.SetString("AR_weapon", "locked");

                    for (int i = 0; i < player.unlockedSkills.Count; i++) 
                    {
                        switch (player.unlockedSkills[i].Skill)
                        {
                            case 0:
                                PlayerPrefs.SetString("dmg_boost", "unlocked");
                                break;
                            case 1:
                                PlayerPrefs.SetString("hp_boost", "unlocked");
                                break;
                            case 2:
                                PlayerPrefs.SetString("firerate_boost", "unlocked");
                                break;
                            case 3:
                                PlayerPrefs.SetString("headshot_boost", "unlocked");
                                break;
                            case 4:
                                PlayerPrefs.SetString("movement_boost", "unlocked");
                                break;
                            case 5:
                                PlayerPrefs.SetString("mag_boost", "unlocked");
                                break;
                            case 6:
                                PlayerPrefs.SetString("AR_weapon", "unlocked");
                                break;
                            default:
                                Debug.Log("nincs még semmi kitanulva");
                                break;
                        }
                    }

                    Debug.Log(www.downloadHandler.text);
                    break;
                default:
                    StartCoroutine(Warning("Something went wrong, try again later!", false));
                    logBTN.interactable = true;
                    break;
            }
        }
    }

    IEnumerator Warning(string warningMessage, bool success)
    {
        if (success)
        {
            warningText.GetComponentInChildren<TextMeshProUGUI>().text = warningMessage;
            warningText.SetActive(true);
            PlayerPrefs.SetString("Username",nameInput.text);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            warningText.GetComponentInChildren<TextMeshProUGUI>().text = warningMessage;
            warningText.SetActive(true);
            yield return new WaitForSeconds(2f);
            warningText.SetActive(false);
        }

    }

    public void VerifyData() 
    {
        logBTN.interactable = (nameInput.text.Trim() != "" && passInput.text.Trim() != "" && passInput.text.Length >= 4 && nameInput.text.Length >= 3);
    }
}
