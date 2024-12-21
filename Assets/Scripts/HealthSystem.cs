using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100;
    //[SerializeField] private float maxHealth = 100;

    public Slider healthSlider;
    public TextMeshProUGUI healthUI;

    void Start()
    {
        
    }
    void Update()
    {
        if (currentHealth >= 0)
        {
            healthSlider.value = currentHealth / 100;
            healthUI.text = $"{currentHealth}";
            if (currentHealth > 75) healthUI.color = Color.white;
            else if (currentHealth <= 75 && currentHealth > 25) healthUI.color = Color.yellow;
            else if (currentHealth <= 25) healthUI.color = Color.red;
        }
        else 
        {
            Debug.Log("meghaltál");
        }

        if (Input.GetKeyDown(KeyCode.J)) TakeDmg(15);

        if (Input.GetKeyDown(KeyCode.K)) Heal(20);
    }

    public void TakeDmg(int dmg) 
    {
        if (currentHealth <= 0)
        {
            Debug.Log("meghalt");
        }
        else 
        {
            currentHealth -= dmg;
        }    
    }

    public void Heal(float heal) 
    {
        currentHealth += heal;
    }
}
