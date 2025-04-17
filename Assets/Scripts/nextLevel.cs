using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    public string nextSceneName;
    public GameObject player;
    public static int kills, allAmmo, magAmmo;
    public static float timeInSeconds, currentHP;
    private void OnTriggerEnter(Collider other)
    {
        kills = GameObject.Find("GameManager").GetComponent<GameManager>().kills;
        timeInSeconds = GameObject.Find("GameManager").GetComponent<GameTimer>().elapsedTime;
        currentHP = player.GetComponent<HealthSystem>().currentHealth;
        allAmmo = player.GetComponent<EquippedWeapon>().weapon.allAmmo;
        magAmmo = player.GetComponent<EquippedWeapon>().weapon.currentMag;
        SceneManager.LoadScene(nextSceneName);
    }
}
