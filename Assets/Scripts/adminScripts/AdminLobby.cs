using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class AdminLobby : MonoBehaviour
{
    public int permission;
    public GameObject adminPanel;
    void Start()
    {
        permission = PlayerPrefs.GetInt("Permission");
    }

    void Update()
    {
        if (permission == 1)
        {
            if (Input.GetKeyDown(KeyCode.P) && !adminPanel.activeInHierarchy)
            {
                adminPanel.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.P) && adminPanel.activeInHierarchy)
            {
                adminPanel.SetActive(false);
            }
        }
    }

    public void Reset()
    {
        PlayerPrefs.SetString("dmg_boost","locked");
        PlayerPrefs.SetString("hp_boost","locked");
        PlayerPrefs.SetString("headshot_boost","locked");
        PlayerPrefs.SetString("firerate_boost","locked");
        PlayerPrefs.SetString("movement_boost","locked");
        PlayerPrefs.SetString("mag_boost","locked");

        PlayerPrefs.SetString("AR_weapon","locked");
    }

    public void SkillPointHack() 
    {
        PlayerPrefs.SetInt("SP",PlayerPrefs.GetInt("SP") + 10);
    }
}
