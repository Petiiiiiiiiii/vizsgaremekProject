using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string player;
    private GameObject[] allObjects;
    public bool isBossDead;
    public GameObject bossRoom;
    public bool hasKey;

    int refreshRate = 60;

    public int kills = 0;

    private IEnumerator Start()
    {
        yield return null;

        allObjects = FindObjectsOfType<GameObject>();
        isBossDead = false;
        hasKey = false;

        switch (PlayerPrefs.GetInt("refreshRate"))
        {
            case 60: refreshRate = 60; break;
            case 120: refreshRate = 120; break;
            case 144: refreshRate = 144; break;
            case 165: refreshRate = 165; break;
            case 200: refreshRate = 200; break;
        }

        Application.targetFrameRate = refreshRate;

        if (SceneManager.GetActiveScene().name.Contains("Map2"))
        {
            kills = nextLevel.kills;
            this.GetComponent<GameTimer>().elapsedTime = nextLevel.timeInSeconds;
            GameObject.Find(player).GetComponent<HealthSystem>().currentHealth = nextLevel.currentHP;
            GameObject.Find(player).GetComponent<EquippedWeapon>().weapon.currentMag = nextLevel.magAmmo;
            GameObject.Find(player).GetComponent<EquippedWeapon>().weapon.allAmmo = nextLevel.allAmmo;
        }
    }
    private void Update()
    {
        UpdateEnemyCount();

        if (isBossDead)
        {
            bossRoom.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void UpdateEnemyCount()
    {
        allObjects = FindObjectsOfType<GameObject>();

        int boss = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == "enemyBOSS")
            {
                boss++;
            }
        }

        if (boss == 0) isBossDead = true;
        else isBossDead = false;

    }

    public void GameOver()
    {
        SceneManager.LoadScene("Lobby");
    }
}
