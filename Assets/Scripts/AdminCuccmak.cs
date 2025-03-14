using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCuccmak : MonoBehaviour
{
    private Transform player;
    private string permission;
    private void Awake()
    {
        player = transform;
        permission = PlayerPrefs.GetString("Permission");
    }
    private void Update()
    {
        if (permission == "1")
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                PlayerPrefs.SetInt("playerLevel", 0);
                PlayerPrefs.SetInt("playerXP", 0);
                Debug.Log("player level: " + PlayerPrefs.GetInt("playerLevel") + " (resetelve)");
                Debug.Log("player xp: " + PlayerPrefs.GetInt("playerXP") + " (resetelve)");
            }
        }
    }
}
