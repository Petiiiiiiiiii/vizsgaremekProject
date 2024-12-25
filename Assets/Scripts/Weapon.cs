using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float fireSpeed;
    public float damage;
    public float headshotDamage;
    public float range;

    public int currentMag;
    public int allAmmo;
    public int maxMag;

    public bool isReloading;

    public abstract void Shoot();
    public abstract IEnumerator Reload();
}
