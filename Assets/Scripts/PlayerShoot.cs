using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] GameObject concreteImpact;
    [SerializeField] GameObject woodImpact;
    [SerializeField] GameObject sandImpact;
    [SerializeField] GameObject metalImpact;
    [SerializeField] GameObject bloodImpact;
    [SerializeField] GameObject dirtImpact;
    public float fireRate = 5f;
    private float nextTimeToFire = 0f;
    private GameObject mainCamera;
    public GameObject scopeSight;

    public int currentMag;
    public int allAmmo;
    private int maxMag;
    public bool isReloading = false;

    public TextMeshProUGUI allAmmoUI;
    public TextMeshProUGUI currentAmmoUI;

    void Start()
    {
        mainCamera = Weapon;
        currentMag = 30;
        allAmmo = 210;
        maxMag = 30;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (fireMode)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentMag > 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentMag > 0)
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        currentMag--;
        currentAmmoUI.text = $"{currentMag}";

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
                        impactEffect = Instantiate(woodImpact, hit.point, Quaternion.LookRotation(hit.normal));
                        break;

                    case MaterialType.Material.Concrete:
                        impactEffect = Instantiate(concreteImpact.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                        break;

                    case MaterialType.Material.Metal:
                        impactEffect = Instantiate(metalImpact.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                        break;

                    case MaterialType.Material.Dirt:
                        impactEffect = Instantiate(dirtImpact.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                        break;

                    case MaterialType.Material.Sand:
                        impactEffect = Instantiate(sandImpact.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                        break;

                    case MaterialType.Material.Blood:
                        impactEffect = Instantiate(bloodImpact.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
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

    IEnumerator Reload()
    {
        if (currentMag == maxMag || allAmmo <= 0)
            yield break;

        isReloading = true;
        //animator.SetTrigger("Reload");

        // Várakozási idõ a reload animációhoz (pl. 2 másodperc)
        yield return new WaitForSeconds(2f);

        int ammoNeeded = maxMag - currentMag;
        int ammoToReload = Mathf.Min(ammoNeeded, allAmmo);

        currentMag += ammoToReload;
        allAmmo -= ammoToReload;

        allAmmoUI.text = $"{allAmmo}";
        currentAmmoUI.text = $"{currentMag}";

        isReloading = false;
    }
}
