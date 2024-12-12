using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject Weapon;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 20f;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(Weapon.transform.position, Weapon.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
            };

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
