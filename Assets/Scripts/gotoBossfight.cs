using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gotoBossfight : MonoBehaviour
{
    public List<GameObject> lights;

    public string nextSceneName;
    public GameObject player;
    public static int kills, allAmmo, magAmmo;
    public static float timeInSeconds, currentHP, expSliderValue;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].SetActive(false);
            }
            StartCoroutine(waitSec(1f));

            kills = GameObject.Find("GameManager").GetComponent<GameManager>().kills;
            timeInSeconds = GameObject.Find("GameManager").GetComponent<GameTimer>().elapsedTime;
            currentHP = player.GetComponent<HealthSystem>().currentHealth;
            allAmmo = player.GetComponent<EquippedWeapon>().weapon.allAmmo;
            magAmmo = player.GetComponent<EquippedWeapon>().weapon.currentMag;
            expSliderValue = GameObject.Find("XP_slider").GetComponent<Slider>().value;

            SceneManager.LoadScene(nextSceneName);
        }
    }
    IEnumerator waitSec(float value) 
    {
        yield return new WaitForSeconds(value);
    }

}
