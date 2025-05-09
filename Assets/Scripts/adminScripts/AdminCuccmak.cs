using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCuccmak : MonoBehaviour
{
    private Transform player;
    private int permission;
    public GameObject adminPanel;

    private void Awake()
    {
        player = transform;
        permission = PlayerPrefs.GetInt("Permission");
    }
    private void Update()
    {
        if (permission == 1)
        {
            if (Input.GetKeyDown(KeyCode.P) && !adminPanel.activeInHierarchy)
            {
                adminPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (Input.GetKeyDown(KeyCode.P) && adminPanel.activeInHierarchy)
            {
                adminPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    public void playerKill() 
    {
        this.GetComponent<HealthSystem>().TakeDmg(100);
    }
    public void xpReset()
    {
        PlayerPrefs.SetInt("playerLevel", 0);
        PlayerPrefs.SetInt("playerXP", 0);
        Debug.Log("player level: " + PlayerPrefs.GetInt("playerLevel") + " (resetelve)");
        Debug.Log("player xp: " + PlayerPrefs.GetInt("playerXP") + " (resetelve)");
    }
    public void killAllEnemy() 
    {
        GameObject.Find("Enemies").SetActive(false);
    }
}
