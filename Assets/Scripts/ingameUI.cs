using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ingameUI : MonoBehaviour
{
    private TextMeshProUGUI playerLevel;
    private TextMeshProUGUI playerName;
    void Start()
    {
        playerLevel = GameObject.Find("levelBG").GetComponentInChildren<TextMeshProUGUI>();
        playerName = GameObject.Find("usernameBG").GetComponentInChildren<TextMeshProUGUI>();

        playerLevel.text = "999";
        playerName.text = PlayerPrefs.GetString("Username");
    }
}
