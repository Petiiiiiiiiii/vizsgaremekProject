using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        // Ellenõrizzük, hogy a player colliderével ütközik-e
        HealthSystem playerHealth = other.GetComponent<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.TakeDmg(damage);
            Debug.Log("Player hit! Damage dealt: " + damage);
            Destroy(gameObject); // Lövedék megsemmisítése ütközéskor
        }
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
}
