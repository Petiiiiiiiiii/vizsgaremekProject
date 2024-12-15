using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject Weapon;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 20f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impact;
    [SerializeField] bool fireMode = true;
    [SerializeField] double fireRate = 0.2f;
    private float timer = 0f;
    void Start()
    {
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (fireMode)
            {

                if (Input.GetButton("Fire1"))
                {
                    Shoot();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }
            }
            
        }

        if (Input.GetKeyDown("b"))
        {
            fireMode = !fireMode;
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
            timer = 0f;
        }
    }
}
