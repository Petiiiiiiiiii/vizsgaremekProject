using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private Transform player;
    string playerGameobj;

    public float maxHealth = 50;
    public float currentHealth = 50f;
    public Animator animator;
    public float dyingAnimation = 3.1f;

    public Slider slider;
    public TextMeshProUGUI hpText;

    CapsuleCollider[] colliders;

    public GameObject ammoBoxPrefab;

    private void Start()
    {
        colliders = GetComponentsInChildren<CapsuleCollider>();
        playerGameobj = GameObject.Find("GameManager").GetComponent<GameManager>().player;
        player = GameObject.Find(playerGameobj).transform;

        slider.value = currentHealth / maxHealth;
        hpText.text = $"{currentHealth}/{maxHealth}";
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        slider.value = currentHealth / maxHealth;
        hpText.text = $"{Mathf.Round(currentHealth)}/{maxHealth}";

        if (currentHealth < 0) 
        {
            hpText.text = $"0/{maxHealth}";
        }

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
        Instantiate(ammoBoxPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),Quaternion.Euler(-90f,0f,0f));
        Destroy(gameObject);
        FindObjectOfType<ingameUI>().UpdateEnemyCount();
        GameObject.Find("GameManager").GetComponent<GameManager>().kills += 1;
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
