using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Skills : MonoBehaviour
{
    [SerializeField] private int availableSP;
    [SerializeField] private TextMeshProUGUI skillsSP_text;
    [SerializeField] private TextMeshProUGUI lobbySP_text;

    [SerializeField] private List<Button> btns;
    [SerializeField] private List<GameObject> checkmarks;

    [SerializeField] private GameObject errorPanel;

    private void Start()
    {
        if (PlayerPrefs.HasKey("AR_weapon")) 
        {
            switch (PlayerPrefs.GetString("AR_weapon"))
            {
                case "unlocked":
                    btns[6].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[6].SetActive(true);
                    break;
                case "locked":
                    btns[6].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[6].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("dmg_boost"))
        {
            switch (PlayerPrefs.GetString("dmg_boost"))
            {
                case "unlocked":
                    btns[0].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[0].SetActive(true);
                    break;
                case "locked":
                    btns[0].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[0].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("hp_boost"))
        {
            switch (PlayerPrefs.GetString("hp_boost"))
            {
                case "unlocked":
                    btns[1].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[1].SetActive(true);
                    break;
                case "locked":
                    btns[1].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[1].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("headshot_boost"))
        {
            switch (PlayerPrefs.GetString("headshot_boost"))
            {
                case "unlocked":
                    btns[3].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[3].SetActive(true);
                    break;
                case "locked":
                    btns[3].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[3].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("mag_boost"))
        {
            switch (PlayerPrefs.GetString("mag_boost"))
            {
                case "unlocked":
                    btns[5].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[5].SetActive(true);
                    break;
                case "locked":
                    btns[5].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[5].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("firerate_boost"))
        {
            switch (PlayerPrefs.GetString("firerate_boost"))
            {
                case "unlocked":
                    btns[2].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[2].SetActive(true);
                    break;
                case "locked":
                    btns[2].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[2].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("movement_boost"))
        {
            switch (PlayerPrefs.GetString("movement_boost"))
            {
                case "unlocked":
                    btns[4].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[4].SetActive(true);
                    break;
                case "locked":
                    btns[4].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[4].SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    private void Update()
    {
        availableSP = PlayerPrefs.GetInt("SP");
        skillsSP_text.text = availableSP.ToString() + " SP";
        lobbySP_text.text = availableSP.ToString() + " SP";
    }

    public void dmg_boost() 
    {
        if (availableSP >= 5)
        {
            PlayerPrefs.SetInt("SP", PlayerPrefs.GetInt("SP") - 5);
            StartCoroutine(spSave());
            PlayerPrefs.SetString("dmg_boost", "unlocked");
            btns[0].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[0].SetActive(true);
            StartCoroutine(SkillUnlock(0));
        }
        else StartCoroutine(ErrorPoup());
    }
    public void hp_boost() 
    {
        if (availableSP >= 5)
        {
            PlayerPrefs.SetInt("SP", PlayerPrefs.GetInt("SP") - 5);
            StartCoroutine(spSave());
            PlayerPrefs.SetString("hp_boost", "unlocked");
            btns[1].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[1].SetActive(true);
            StartCoroutine(SkillUnlock(1));
        }
        else StartCoroutine(ErrorPoup());
    }
    public void firerate_boost() 
    {
        if (availableSP >= 5)
        {
            PlayerPrefs.SetInt("SP", PlayerPrefs.GetInt("SP") - 5);
            StartCoroutine(spSave());
            PlayerPrefs.SetString("firerate_boost", "unlocked");
            btns[2].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[2].SetActive(true);
            StartCoroutine(SkillUnlock(2));
        }
        else StartCoroutine(ErrorPoup());
    }
    public void headshot_boost() 
    {
        if (availableSP >= 5)
        {
            PlayerPrefs.SetInt("SP", PlayerPrefs.GetInt("SP") - 5);
            StartCoroutine(spSave());
            PlayerPrefs.SetString("headshot_boost", "unlocked");
            btns[3].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[3].SetActive(true);
            StartCoroutine(SkillUnlock(3));
        }
        else StartCoroutine(ErrorPoup());
    }
    public void movement_boost() 
    {
        if (availableSP >= 5)
        {
            PlayerPrefs.SetInt("SP", PlayerPrefs.GetInt("SP") - 5);
            StartCoroutine(spSave());
            PlayerPrefs.SetString("movement_boost", "unlocked");
            btns[4].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[4].SetActive(true);
            StartCoroutine(SkillUnlock(4));
        }
        else StartCoroutine(ErrorPoup());
    }
    public void mag_boost() 
    {
        if (availableSP >= 5)
        {
            PlayerPrefs.SetInt("SP", PlayerPrefs.GetInt("SP") - 5);
            StartCoroutine(spSave());
            PlayerPrefs.SetString("mag_boost", "unlocked");
            btns[5].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[5].SetActive(true);
            StartCoroutine(SkillUnlock(5));
        }
        else StartCoroutine(ErrorPoup());
    }

    public void AR_unlock() 
    {
        if (availableSP >= 10)
        {
            PlayerPrefs.SetInt("SP", PlayerPrefs.GetInt("SP") - 10);
            StartCoroutine(spSave());
            PlayerPrefs.SetString("AR_weapon","unlocked");
            btns[6].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[6].SetActive(true);
            StartCoroutine(SkillUnlock(6));
        }
        else StartCoroutine(ErrorPoup());
    }

    IEnumerator ErrorPoup() 
    {
        errorPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        errorPanel.SetActive(false);
    }

    private class skillObject
    {
        public int playerId;
        public int skill;
    }

    IEnumerator SkillUnlock(int skillID)
    {
        skillObject uj = new();
        uj.skill = skillID;
        uj.playerId = PlayerPrefs.GetInt("playerID");

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost:7000/api/UnlockedSkills", JsonUtility.ToJson(uj), "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.result + " " + www.responseCode + " " + www.downloadHandler.text );
        }
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

    IEnumerator spSave() 
    {
        player buyer = new();
        buyer.playerId = PlayerPrefs.GetInt("playerID");
        buyer.username = PlayerPrefs.GetString("Username");
        buyer.passwordHash = PlayerPrefs.GetString("passwordHash");
        buyer.email = PlayerPrefs.GetString("playerEmail");
        buyer.level = PlayerPrefs.GetInt("playerLevel");
        buyer.sp = PlayerPrefs.GetInt("SP");
        buyer.permission = PlayerPrefs.GetInt("Permission");
        DateTime.TryParse(PlayerPrefs.GetString("regDate"), out DateTime regDate);
        buyer.regDate = regDate.ToString("o");

        using UnityWebRequest www = UnityWebRequest.Put($"http://localhost:7000/api/Players/{buyer.playerId}", JsonUtility.ToJson(buyer));

        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.result + " " + www.responseCode + " " + www.downloadHandler.text);
        }

    }
}
