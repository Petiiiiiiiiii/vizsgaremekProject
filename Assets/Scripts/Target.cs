using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float maxHealth = 50;
    public float currentHealth = 50f;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }

        GetComponent<AiPlayerFollow>().RotateTowardsPlayer();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
