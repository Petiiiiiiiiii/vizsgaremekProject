using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject Weapon;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 20f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impact;
    [SerializeField] bool fireMode = true;
    public float fireRate = 5f; //hány ammo / másodperc (5f az egynelõ 5 ammo / másodperc)
    private float nextTimeToFire = 0f;
    private GameObject mainCamera;
    public GameObject scopeSight;

    void Start()
    {
        mainCamera = Weapon;
    }

    
    void Update()
    {
        if (fireMode)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else 
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("IsScopeing", true);
            crosshair.SetActive(false);
            Weapon = scopeSight;
        }

        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IsScopeing", false);
            crosshair.SetActive(true);
            Weapon = mainCamera;
        }

        if (Input.GetKeyDown("b"))
        {
            fireMode = !fireMode;
        }

    }

    void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        if (Physics.Raycast(Weapon.transform.position, Weapon.transform.forward, out hit, range))
        {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactEffect = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactEffect, 1f);

        };
    }
}
