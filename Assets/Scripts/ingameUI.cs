using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ingameUI : MonoBehaviour
{
    private TextMeshProUGUI playerLevel;
    private TextMeshProUGUI playerName;
    public TextMeshProUGUI remainingEnemies;
    void Start()
    {
        playerLevel = GameObject.Find("levelBG").GetComponentInChildren<TextMeshProUGUI>();
        playerName = GameObject.Find("usernameBG").GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(UpdateEnemyCounterRoutine());

        playerLevel.text = PlayerPrefs.GetInt("playerLevel").ToString();
        playerName.text = PlayerPrefs.GetString("Username");
    }
    private void FixedUpdate()
    {
        playerLevel.text = PlayerPrefs.GetInt("playerLevel").ToString();
    }

    public void UpdateEnemyCount()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        int enemyCount = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == enemyLayer)
            {
                enemyCount++;
            }
        }

        remainingEnemies.text = "Enemies Left: " + enemyCount;

        if (enemyCount == 0 && SceneManager.GetActiveScene().name == "Map-1") 
        {
            Debug.Log("nyert a player, match feltoltes");
        }

    }

    IEnumerator UpdateEnemyCounterRoutine()
    {
        while (true)
        {
            UpdateEnemyCount();
            yield return new WaitForSeconds(1f);
        }
    }


}
