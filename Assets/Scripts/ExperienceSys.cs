using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ExperienceSys : MonoBehaviour
{
    public GameObject levelUPtext;
    private int xp;

    public Slider xpSlider;
    private void Start()
    {
        xp = PlayerPrefs.GetInt("playerLevel") * 500;
        PlayerPrefs.SetInt("playerXP", xp);
    }
    public void enemyKill() 
    {
        xpSlider.value += 0.1f;
        PlayerPrefs.SetInt("playerXP", PlayerPrefs.GetInt("playerXP") + 50);
        xp = PlayerPrefs.GetInt("playerXP");
        int level = xp / 500;
        if((xp / 500) > PlayerPrefs.GetInt("playerLevel")) 
        {
            StartCoroutine(levelUP());
            StartCoroutine(levelSave(level));
        }
        PlayerPrefs.SetInt("playerLevel", level);
    }
    IEnumerator levelUP() 
    {
        levelUPtext.SetActive(true);
        xpSlider.value = 0f;
        yield return new WaitForSeconds(3f);
        levelUPtext.SetActive(false);
    }
    public void bossKill() 
    {
        xpSlider.value += 0.5f;
        PlayerPrefs.SetInt("playerXP", PlayerPrefs.GetInt("playerXP") + 250);
        xp = PlayerPrefs.GetInt("playerXP");
        int level = xp / 500;
        if ((xp / 500) > PlayerPrefs.GetInt("playerLevel"))
        {
            StartCoroutine(levelUP());
            StartCoroutine(levelSave(level));
        }
        PlayerPrefs.SetInt("playerLevel", level);
    }

    private class player
    {
        public int playerId;
        public string username;
        public string passwordHash;
        public string email;
        public int level;
        public int sp;
        public int permission;
        public string regDate;
    }

    IEnumerator levelSave(int level)
    {
        player Player = new();
        Player.playerId = PlayerPrefs.GetInt("playerID");
        Player.username = PlayerPrefs.GetString("Username");
        Player.passwordHash = PlayerPrefs.GetString("passwordHash");
        Player.email = PlayerPrefs.GetString("playerEmail");
        Player.level = level;
        Player.sp = PlayerPrefs.GetInt("SP");
        Player.permission = PlayerPrefs.GetInt("Permission");
        DateTime.TryParse(PlayerPrefs.GetString("regDate"), out DateTime regDate);
        Player.regDate = regDate.ToString("o");

        using UnityWebRequest www = UnityWebRequest.Put($"http://localhost:7000/api/Players/{Player.playerId}", JsonUtility.ToJson(Player));

        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.result + " " + www.responseCode + " " + www.downloadHandler.text);
        }

    }

}
