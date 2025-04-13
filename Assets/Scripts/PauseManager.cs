using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject settingsMenu;
    public GameObject playerUI;

    public GameObject player;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuCanvas.activeInHierarchy)
        {
            pauseMenuCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            player.GetComponent<PlayerController>().canMove = false;

            if (GameObject.Find("G36C")) player.GetComponentInChildren<AR>().canShoot = false;
            else if (GameObject.Find("SMG")) player.GetComponentInChildren<SMG>().canShoot = false;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuCanvas.activeInHierarchy) Resume();
    }

    public void SettingsMenu() 
    {
        settingsMenu.SetActive(true);
        playerUI.SetActive(false);
        pauseMenuCanvas.SetActive(false);
    }
    public void SettingsMenuBack()
    {
        pauseMenuCanvas.SetActive(true);
        settingsMenu.SetActive(false); 
    }
    public void Resume() 
    {
        playerUI.SetActive(true);
        pauseMenuCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;

        player.GetComponent<PlayerController>().canMove = true;

        if (GameObject.Find("G36C")) player.GetComponentInChildren<AR>().canShoot = true;
        else if (GameObject.Find("SMG")) player.GetComponentInChildren<SMG>().canShoot = true;
    }
    public void BackToLobby()
    {
        Time.timeScale = 1;
        StartCoroutine(SaveMatchlog());
        SceneManager.LoadScene("Lobby");
    }

    private class matchStats
    {
        public int playerId;
        public int kills;
        public int matchDuration;
        public bool win;
    }

    IEnumerator SaveMatchlog()
    {
        matchStats thisMatch = new();
        thisMatch.playerId = PlayerPrefs.GetInt("playerID");
        thisMatch.kills = GameObject.Find("GameManager").GetComponent<GameManager>().kills;
        thisMatch.matchDuration = (int)GameObject.Find("GameManager").GetComponent<GameTimer>().elapsedTime;
        thisMatch.win = false;

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost:7000/api/MatchLogs", JsonUtility.ToJson(thisMatch), "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.result + " " + www.responseCode + " " + www.downloadHandler.text);
        }
    }
}
