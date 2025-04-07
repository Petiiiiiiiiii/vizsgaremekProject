using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LobbyHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Canvas LobbyUI;
    [SerializeField] Canvas SkillsUI;
    [SerializeField] Canvas PlayUI;
    [SerializeField] GameObject wrongWeaponType;

    public TextMeshProUGUI levelAndName;
    public TextMeshProUGUI playmenuLevel;
    public TextMeshProUGUI playmenuName;
    public TextMeshProUGUI skillsmenuName;
    public TextMeshProUGUI skillsmenuLevel;

    public TMP_Dropdown difficulty;
    private string selectedDifficulty = "Medium";

    public TextMeshProUGUI displayText;
    public UnityEngine.UI.Button leftButton;
    public UnityEngine.UI.Button rightButton;
    private List<string> weapons = new List<string> {"AR","SMG"};
    private int currentIndex = 0;

    private void Start()
    {
        levelAndName.text = $"Level {PlayerPrefs.GetInt("playerLevel")} - {PlayerPrefs.GetString("Username")}";
        playmenuLevel.text = PlayerPrefs.GetInt("playerLevel").ToString();
        skillsmenuLevel.text = PlayerPrefs.GetInt("playerLevel").ToString();
        playmenuName.text = PlayerPrefs.GetString("Username");
        skillsmenuName.text = PlayerPrefs.GetString("Username");

        difficulty.onValueChanged.AddListener(OnDropdownValueChanged);
        UpdateDisplay();
        leftButton.onClick.AddListener(OnLeftButtonClicked);
        rightButton.onClick.AddListener(OnRightButtonClicked);
    }
    void OnLeftButtonClicked()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = weapons.Count - 1;
        }
        UpdateDisplay();
    }

    void OnRightButtonClicked()
    {
        currentIndex++;
        if (currentIndex >= weapons.Count)
        {
            currentIndex = 0;
        }
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayText.text = weapons[currentIndex];
    }
    void OnDropdownValueChanged(int index)
    {
        selectedDifficulty = difficulty.options[index].text;
    }
    public void PlayGame() 
    {
        if (weapons[currentIndex] == "AR")
        {
            if (PlayerPrefs.GetString("AR_weapon") == "unlocked")
            {
                SceneManager.LoadScene($"Map1-{weapons[currentIndex]}-{selectedDifficulty}");
            }
            else StartCoroutine(AR_not_unlocked());
        }
        else SceneManager.LoadScene($"Map1-{weapons[currentIndex]}-{selectedDifficulty}");

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

    public void SettingsButton() 
    {
        StartCoroutine(WaitForAnimSettings(true));
    }
    public void SettingsBackButton() 
    {
        StartCoroutine(WaitForAnimSettings(false));
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
    IEnumerator WaitForAnimSettings(bool value)
    {
        if (value)
        {
            animator.SetBool("Settings", value);
            LobbyUI.gameObject.SetActive(!value);
            yield return new WaitForSeconds(1f);
        }
        else
        {
            animator.SetBool("Settings", value);
            yield return new WaitForSeconds(1f);
            LobbyUI.gameObject.SetActive(!value);
        }

    }

    public void Exit() 
    {
        Debug.Log("ki lett lépve");
        Application.Quit();
    }

    IEnumerator AR_not_unlocked()
    {
        wrongWeaponType.SetActive(true);
        yield return new WaitForSeconds(2f);
        wrongWeaponType.SetActive(false);
    }
}
