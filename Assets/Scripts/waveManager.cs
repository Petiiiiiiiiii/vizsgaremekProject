using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class waveManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject spawnpoint, spawnpoint2, spawnpoint3;
    int enemyLayer;

    public TextMeshProUGUI waveText;
    Dictionary<int, int> waves = new()
    {
        {1,3}, {2,5}, {3,7}, {4,9}, {5,12}, {6,1 }
    };
    bool isWaveActive = false;
    int currentWave = 1;

    public GameObject winScreen;
    public TextMeshProUGUI kills;
    public TextMeshProUGUI playtime;

    public Animator creditsAnim;
    public GameObject credits;

    public AudioSource creditsMusic;

    public GameObject player;

    private void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    IEnumerator spawnEnemies(int enemyCount) 
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int pos = Random.Range(1,4);
            int enemy = Random.Range(0,3);
            switch (pos)
            {
                case 1:
                    Instantiate(enemies[enemy], new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, spawnpoint.transform.position.z), Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                    break;
                case 2:
                    Instantiate(enemies[enemy], new Vector3(spawnpoint2.transform.position.x, spawnpoint2.transform.position.y, spawnpoint2.transform.position.z), Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                    break;
                case 3:
                    Instantiate(enemies[enemy], new Vector3(spawnpoint3.transform.position.x, spawnpoint3.transform.position.y, spawnpoint3.transform.position.z), Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                    break;
                default:
                    break;
            }
        }
    }
    public void StartWave() 
    {
        StartCoroutine(spawnEnemies(waves[currentWave]));
        isWaveActive = true;
    }

    public void UpdateEnemyCount()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        int enemyCount = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == enemyLayer)
            {
                enemyCount++;
            }
        }

        if (isWaveActive && enemyCount == 0) 
        {
            currentWave++;
            if (currentWave == 6)
            {
                isWaveActive = false;
                player.GetComponent<AudioSource>().Play();
                kills.text = $"kills: {GameObject.Find("GameManager").GetComponent<GameManager>().kills}";
                float elapsedTime = GameObject.Find("GameManager").GetComponent<GameTimer>().elapsedTime;
                int hours = Mathf.FloorToInt(elapsedTime / 3600);
                int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                playtime.text = $"match length: {string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds)}";
                winScreen.SetActive(true);
                if (GameObject.Find("G36C")) player.GetComponentInChildren<AR>().canShoot = false;
                else if (GameObject.Find("SMG_F")) player.GetComponentInChildren<SMG>().canShoot = false;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
            }
            else
            {
                StartWave();
            }
        }

        waveText.text = $"Wave {currentWave}  ( {enemyCount} / {waves[currentWave]} )";
    }

    private void Update()
    {
        UpdateEnemyCount();
    }

    IEnumerator StartCredits() 
    {
        creditsAnim.Play("credits");
        creditsMusic.Play();
        yield return new WaitForSeconds(27.3f);
        SceneManager.LoadScene("Lobby");
    }

    public void StartCreditsBTN() 
    {
        credits.SetActive(true);
        StartCoroutine(StartCredits());
    }
}
