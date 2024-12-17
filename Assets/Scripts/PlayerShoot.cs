using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Terrain;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject Weapon;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 20f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] bool fireMode = true;
    [SerializeField] List<GameObject> impact;
    public float fireRate = 5f; //hány ammo / másodperc (5f az egynelõ 5 ammo / másodperc)
    private float nextTimeToFire = 0f;
    private GameObject mainCamera;
    public GameObject scopeSight;

    void Start()
    {
        mainCamera = Weapon;
        impact = new List<GameObject>();
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
                MaterialType materialType = hit.transform.GetComponent<MaterialType>();

                if (materialType != null)
                {
                    GameObject impactEffect = null;
                    
                    switch (materialType.materialType)
                    {
                        case MaterialType.Material.Wood:
                            impactEffect = Instantiate(impact[0], hit.point, Quaternion.LookRotation(hit.normal));
                            break;

                        case MaterialType.Material.Concrete:
                            impactEffect = Instantiate(impact[1], hit.point, Quaternion.LookRotation(hit.normal));
                            break;

                        case MaterialType.Material.Metal:
                            impactEffect = Instantiate(impact[2], hit.point, Quaternion.LookRotation(hit.normal));
                            break;

                        case MaterialType.Material.Dirt:
                            impactEffect = Instantiate(impact[3], hit.point, Quaternion.LookRotation(hit.normal));
                            break;

                        case MaterialType.Material.Sand:
                            impactEffect = Instantiate(impact[4], hit.point, Quaternion.LookRotation(hit.normal));
                            break;

                        case MaterialType.Material.Blood:
                            impactEffect = Instantiate(impact[5], hit.point, Quaternion.LookRotation(hit.normal));
                            break;
                    }

                    if (impactEffect != null)
                    {
                        Destroy(impactEffect, 1f);
                    }
                }

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
    }
}
