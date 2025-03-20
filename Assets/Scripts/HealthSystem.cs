using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100;
    //[SerializeField] private float maxHealth = 100;

    public GameObject damageIndicator;

    public Slider healthSlider;
    public TextMeshProUGUI healthUI;

    public GameObject deathPanel;

    void Start()
    {
        
    }
    void Update()
    {
        if (currentHealth >= 0)
        {
            healthSlider.value = currentHealth / 100;
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
            deathPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Heal(float heal) 
    {
        currentHealth += heal;
    }
}
