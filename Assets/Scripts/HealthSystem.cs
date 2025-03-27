using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    public GameObject damageIndicator;

    public Slider healthSlider;
    public TextMeshProUGUI healthUI;

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
            healthUI.text = $"{currentHealth}";
            if (currentHealth > 75)
            {
                healthUI.color = Color.white;
            } 
            else if (currentHealth <= 75 && currentHealth > 25)
            {
                healthUI.color = Color.yellow;
            } 
            else if (currentHealth <= 25)
            {
                healthUI.color = Color.red;
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
            Debug.Log("meghalt a player, match feltoltes");
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
}
