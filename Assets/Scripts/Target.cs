using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    private Transform player;
    string playerGameobj;

    public float maxHealth = 50;
    public float currentHealth = 50f;
    public Animator animator;
    public float dyingAnimation = 3.1f;

    CapsuleCollider[] colliders;

    private void Start()
    {
        colliders = GetComponentsInChildren<CapsuleCollider>();
        playerGameobj = GameObject.Find("GameManager").GetComponent<GameManager>().player;
        player = GameObject.Find(playerGameobj).transform;
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
        if (transform.tag == "enemyBOSS")
        {
            player.GetComponent<ExperienceSys>().bossKill();
        }
        else if (transform.tag == "enemyNormal")
        {
            player.GetComponent<ExperienceSys>().enemyKill();
        }

        Debug.Log("xp: " + PlayerPrefs.GetInt("playerXP") + " xp");
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
