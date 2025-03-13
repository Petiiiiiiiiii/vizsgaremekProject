using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

        playerLevel.text = "999";
        playerName.text = PlayerPrefs.GetString("Username");
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
