using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AiPlayerFollow : MonoBehaviour
{
    public Animator aiAnimator;
    private Transform player;
    public float shootingDistance = 10f; // Mekkora távolságnál lõ az AI
    public GameObject bulletPrefab; // A lövedék prefabja
    public Transform firePoint; // A lövedék kilövési pontja
    public float fireRate = 3f; // Lövési sebesség (lövések/másodperc)
    public float bulletSpeed = 50f; // A lövedék sebessége
    public float viewDistance = 20f; // Az AI látótávolsága
    public float viewAngle = 90f; // Az AI látószöge fokban

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
            return; // Nem található játékos, így az AI nem tud semmit tenni
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
        ai.isStopped = true; // AI megáll, nem követi tovább a játékost
        Debug.Log("AI is shooting at the player!");
    }

    void StopShooting()
    {
        if (isShooting)
        {
            isShooting = false;
            ai.isStopped = false; // AI újra követi a játékost
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Forgás sebessége
    }

    void HandleShooting()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate; // Újraindítja a lövési idõzítõt
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
