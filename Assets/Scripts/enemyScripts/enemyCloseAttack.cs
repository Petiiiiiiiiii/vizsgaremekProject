using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCloseAttack : MonoBehaviour
{
    public float damage = 10f;
    private void OnTriggerEnter(Collider other)
    {
        HealthSystem playerHealth = other.GetComponent<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.TakeDmg(damage / 2f);
            Debug.Log("Player hit! Damage dealt: " + damage);
        }
    }
}
