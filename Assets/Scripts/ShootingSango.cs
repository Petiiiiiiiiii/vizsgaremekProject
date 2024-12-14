using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSango : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float bulletSpeed = 20f;

    public Transform firePoint;

    public float fireRate = 5f; //h�ny ammo / m�sodperc (5f az egynel� 5 ammo / m�sodperc)

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }
}
