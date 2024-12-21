using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        // Ellen�rizz�k, hogy a player collider�vel �tk�zik-e
        HealthSystem playerHealth = other.GetComponent<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.TakeDmg(damage);
            Debug.Log("Player hit! Damage dealt: " + damage);
            Destroy(gameObject); // L�ved�k megsemmis�t�se �tk�z�skor
        }
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
}
