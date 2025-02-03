using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCuccmak : MonoBehaviour
{
    private Transform player;
    private void Awake()
    {
        player = transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerPrefs.SetInt("playerLevel", 0);
            PlayerPrefs.SetInt("playerXP", 0);
            Debug.Log("player level: " + PlayerPrefs.GetInt("playerLevel") + " (resetelve)");
            Debug.Log("player xp: " + PlayerPrefs.GetInt("playerXP") + " (resetelve)");
        }
    }
}
