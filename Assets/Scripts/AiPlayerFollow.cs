using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiPlayerFollow : MonoBehaviour
{
    private Transform player;
    public float shootingDistance = 10f; // Mekkora távolságnál lõ az AI
    public GameObject bulletPrefab; // A lövedék prefabja
    public Transform firePoint; // A lövedék kilövési pontja
    public float fireRate = 3f; // Lövési sebesség (lövések/másodperc)
    public float bulletSpeed = 50f; // A lövedék sebessége

    private NavMeshAgent ai;
    private bool isShooting = false;
    private float fireCooldown = 0f;

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingDistance)
        {
            if (!isShooting)
            {
                StartShooting();
            }
            RotateTowardsPlayer();
            HandleShooting();
        }
        else
        {
            StopShooting();
            ai.destination = player.position;
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
}
