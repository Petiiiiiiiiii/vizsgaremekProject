using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        SceneManager.LoadScene("Lobby");
        Debug.Log("még el kell menteni");
    }
}
