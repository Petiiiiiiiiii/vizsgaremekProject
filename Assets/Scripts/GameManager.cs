using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string player;
    private GameObject[] allObjects;
    [SerializeField] private bool isBossDead;
    public GameObject bossRoom;

    void Start()
    {
        player = "player - G36C";
        allObjects = FindObjectsOfType<GameObject>();
        isBossDead = false;
    }
    private void Update()
    {
        UpdateEnemyCount();

        if (isBossDead)
        {
            Debug.Log("boss meghalt");
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
