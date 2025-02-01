using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float maxHealth = 50;
    public float currentHealth = 50f;
    public Animator animator;
    public float dyingAnimation = 3.1f;

    CapsuleCollider[] colliders;

    private void Start()
    {
        colliders = GetComponentsInChildren<CapsuleCollider>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            StartCoroutine(enemyDead());
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator enemyDead() 
    {
        GetComponent<NavMeshAgent>().enabled = false;

        foreach (CapsuleCollider collider in colliders)
        {
            collider.enabled = false;
        }
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(dyingAnimation);
        Die();
    }
}
