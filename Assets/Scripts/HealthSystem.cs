using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    public GameObject damageIndicator;

    public Slider healthSlider;
    public TextMeshProUGUI currentHP_text;
    public TextMeshProUGUI maxHP_text;

    public GameObject deathPanel;

    void Start()
    {
        currentHealth = 100;
        maxHealth = 100;

        if (PlayerPrefs.GetString("hp_boost") == "unlocked") 
        {
            currentHealth = 150;
            maxHealth = 150;
        }
    }
    void Update()
    {
        if (currentHealth >= 0)
        {
            healthSlider.value = currentHealth / maxHealth;
            currentHP_text.text = $"{currentHealth}";
            maxHP_text.text = $"{maxHealth}";
            if (currentHealth > 75)
            {
                currentHP_text.color = Color.white;
            } 
            else if (currentHealth <= 75 && currentHealth > 25)
            {
                currentHP_text.color = Color.yellow;
            } 
            else if (currentHealth <= 25)
            {
                currentHP_text.color = Color.red;
            } 

            
        }
    }
    IEnumerator dmgIndicator() 
    {
        damageIndicator.GetComponent<Image>().color = new Color(1, 0, 0, 0.2f);
        yield return new WaitForSeconds(0.1f);
        damageIndicator.GetComponent<Image>().color = new Color(1, 0, 0, 0f);
    }

    public void TakeDmg(float dmg) 
    {
        StartCoroutine(dmgIndicator());
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameTimer>().StopTimer();
            StartCoroutine(SaveMatchlog());
            this.gameObject.SetActive(false);
            GameObject.Find("PlayerUI").SetActive(false);
            deathPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Heal(float heal) 
    {
        currentHealth += heal;
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
