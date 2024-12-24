using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AiPlayerFollow : MonoBehaviour
{
    public Animator aiAnimator;
    private Transform player;
    public float shootingDistance = 10f; // Mekkora t�vols�gn�l l� az AI
    public GameObject bulletPrefab; // A l�ved�k prefabja
    public Transform firePoint; // A l�ved�k kil�v�si pontja
    public float fireRate = 3f; // L�v�si sebess�g (l�v�sek/m�sodperc)
    public float bulletSpeed = 50f; // A l�ved�k sebess�ge
    public float viewDistance = 20f; // Az AI l�t�t�vols�ga
    public float viewAngle = 90f; // Az AI l�t�sz�ge fokban

    private NavMeshAgent ai;
    private bool isShooting = false;
    private float fireCooldown = 0f;
    private bool canSeePlayer = false;

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        GameObject playerObject = GameObject.Find("player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found! Make sure the player object is named 'player'.");
        }
    }

    void Update()
    {
        if (player == null)
        {
            return; // Nem tal�lhat� j�t�kos, �gy az AI nem tud semmit tenni
        }

        CheckPlayerVisibility();

        Debug.Log("Can AI see player? " + canSeePlayer);

        if (canSeePlayer)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= shootingDistance)
            {
                if (!isShooting)
                {
                    aiAnimator.SetBool("isShooting", true);
                    StartShooting();
                }
                RotateTowardsPlayer();
                HandleShooting();
            }
            else
            {
                aiAnimator.SetBool("isShooting", false);
                StopShooting();
                ai.isStopped = false;
                ai.destination = player.position;
                Debug.Log("AI moving towards player. Destination: " + ai.destination);
            }
        }
        else
        {
            aiAnimator.SetBool("isShooting", false);
            StopShooting();
        }
    }

    void StartShooting()
    {
        isShooting = true;
        ai.isStopped = true; // AI meg�ll, nem k�veti tov�bb a j�t�kost
        Debug.Log("AI is shooting at the player!");
    }

    void StopShooting()
    {
        if (isShooting)
        {
            isShooting = false;
            ai.isStopped = false; // AI �jra k�veti a j�t�kost
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Forg�s sebess�ge
    }

    void HandleShooting()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate; // �jraind�tja a l�v�si id�z�t�t
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * bulletSpeed;
            }
            Debug.Log("Bullet fired towards player!");
        }
    }

    void CheckPlayerVisibility()
    {
        if (player == null)
        {
            canSeePlayer = false;
            return;
        }

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= viewDistance)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= viewAngle / 2)
            {
                if (!Physics.Linecast(transform.position + Vector3.up, player.position + Vector3.up, out RaycastHit hit))
                {
                    canSeePlayer = true;
                    return;
                }
                else
                {
                    Debug.Log("AI vision blocked by: " + hit.collider.name);
                }
            }
        }

        canSeePlayer = false;
    }
}
