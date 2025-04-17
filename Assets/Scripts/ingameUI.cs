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

    public GameObject questPanel;
    public TextMeshProUGUI firstObjective;
    public TextMeshProUGUI secondObjective;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(showObjectives());
        }

        if (GameObject.Find("GameManager").GetComponent<GameManager>().hasKey) 
        {
            secondObjective.fontStyle = FontStyles.Strikethrough;
            secondObjective.color = Color.black;
        }
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

        if (enemyCount == 0) 
        {
            firstObjective.fontStyle = FontStyles.Strikethrough;
            firstObjective.color = Color.black;
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

    IEnumerator showObjectives()
    {
        questPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        questPanel.SetActive(false);
    }


}
