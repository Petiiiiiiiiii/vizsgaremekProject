using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SMG : Weapon
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject crosshair;
    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] GameObject concreteImpact;
    [SerializeField] GameObject woodImpact;
    [SerializeField] GameObject sandImpact;
    [SerializeField] GameObject metalImpact;
    [SerializeField] GameObject bloodImpact;
    [SerializeField] GameObject dirtImpact;

    [SerializeField] TextMeshProUGUI allAmmoUI;
    [SerializeField] TextMeshProUGUI currentAmmoUI;

    [SerializeField] AudioSource oneShot;
    [SerializeField] AudioSource emptyMag;
    [SerializeField] AudioSource reloadAudio;
    [SerializeField] AudioSource firemodeSwitch;


    [SerializeField] GameObject hitMarker;
    [SerializeField] GameObject hitMarkerHead;

    public GameObject WeaponPOV;
    public GameObject scopeSight;
    public GameObject mainCamera;

    public bool waiting = false;
    private float timer = 0f;
    private float nextTimeToFire = 0f;
    public bool canShoot;

    public bool fireMode = true;
    public TextMeshProUGUI fireModeText;

    private void Start()
    {
        fireSpeed = 10f;
        damage = 17f;
        headshotDamage = 40f;
        range = 100f;

        maxMag = 40;
        currentMag = 40;
        allAmmo = 210;

        canShoot = true;

        if (PlayerPrefs.GetString("dmg_boost") == "unlocked") damage *= 1.2f;
        if (PlayerPrefs.GetString("headshot_boost") == "unlocked") headshotDamage *= 1.2f;
        if (PlayerPrefs.GetString("mag_boost") == "unlocked") maxMag = 45;
        if (PlayerPrefs.GetString("firerate_boost") == "unlocked") fireSpeed *= 1.2f;
    }

    private void Update()
    {
        if (isReloading)
            return;

        if (waiting)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                waiting = false;
                timer = 0f;
            }
        }

        if (fireMode)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentMag > 0 && canShoot)
            {
                nextTimeToFire = Time.time + 1f / fireSpeed;
                Shoot();
                oneShot.Play();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentMag > 0 && canShoot)
            {
                nextTimeToFire = Time.time + 1f / fireSpeed;
                Shoot();
                oneShot.Play();
            }
        }

        if (fireMode) fireModeText.text = "[B] Full Auto";
        else fireModeText.text = "[B] Semi Auto";

        if (currentMag == 0 && Input.GetButtonDown("Fire1")) emptyMag.Play();

        if (currentMag <= 5) currentAmmoUI.color = Color.red;
        else currentAmmoUI.color = Color.white;

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("IsScopeing", true);
            crosshair.SetActive(false);
            scopeSight.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IsScopeing", false);
            waiting = true;
            crosshair.SetActive(true);
            scopeSight.SetActive(false);
        }

        if (Input.GetKeyDown("b"))
        {
            fireMode = !fireMode;
            firemodeSwitch.Play();
        }

        if (Input.GetKeyDown(KeyCode.R) && !waiting)
        {
            if (animator.GetBool("IsScopeing")) Debug.Log("Scopeol�s k�zbeni reload probalkozas");
            else StartCoroutine(Reload());
        }

        UpdateUI();
    }

    public override void Shoot()
    {
        currentMag--;
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(WeaponPOV.transform.position, WeaponPOV.transform.forward, out hit, range))
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
                        impactEffect = Instantiate(concreteImpact, hit.point, Quaternion.LookRotation(hit.normal));
                        break;
                    case MaterialType.Material.Metal:
                        impactEffect = Instantiate(metalImpact, hit.point, Quaternion.LookRotation(hit.normal));
                        break;
                    case MaterialType.Material.Dirt:
                        impactEffect = Instantiate(dirtImpact, hit.point, Quaternion.LookRotation(hit.normal));
                        break;
                    case MaterialType.Material.Sand:
                        impactEffect = Instantiate(sandImpact, hit.point, Quaternion.LookRotation(hit.normal));
                        break;
                    case MaterialType.Material.Blood:
                        impactEffect = Instantiate(bloodImpact, hit.point, Quaternion.LookRotation(hit.normal));
                        break;
                }

                if (impactEffect != null)
                {
                    Destroy(impactEffect, 1f);
                }
            }
            else Debug.Log("valami nemjo");

            Target target = hit.transform.GetComponentInParent<Target>();
            if (target != null)
            {
                float appliedDamage = (hit.transform.CompareTag("enemyHead")) ? headshotDamage : damage;

                if (hit.transform.CompareTag("enemyHead"))
                {
                    StartCoroutine(hitmarkerController(true));
                    Debug.Log($"Fejl�v�s | {target.currentHealth} HP maradt, damage: {appliedDamage}");
                }
                else
                {
                    StartCoroutine(hitmarkerController(false));
                    Debug.Log($"Testl�v�s | {target.currentHealth} HP maradt, damage: {appliedDamage}");
                }

                target.TakeDamage(appliedDamage);
                enemyController enemy = hit.transform.GetComponentInParent<enemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }

            }
        }
    }

    public override IEnumerator Reload()
    {
        if (currentMag == maxMag || allAmmo <= 0)
            yield break;

        isReloading = true;
        animator.SetTrigger("Reload");
        reloadAudio.Play();

        yield return new WaitForSeconds(2f);

        int ammoNeeded = maxMag - currentMag;
        int ammoToReload = Mathf.Min(ammoNeeded, allAmmo);

        currentMag += ammoToReload;
        allAmmo -= ammoToReload;

        isReloading = false;

        UpdateUI();
    }

    private void UpdateUI()
    {
        allAmmoUI.text = $"{allAmmo}";
        currentAmmoUI.text = $"{currentMag}";
    }

    IEnumerator hitmarkerController(bool hitPoint) 
    {
        if (!hitPoint)
        {
            hitMarker.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            hitMarker.SetActive(false);
        }
        else 
        {
            hitMarkerHead.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            hitMarkerHead.SetActive(false);
        }

    }
}
