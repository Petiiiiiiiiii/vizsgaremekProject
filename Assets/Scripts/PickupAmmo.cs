using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    public GameObject ammoboxCanvas;
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<EquippedWeapon>() == null)
        {
        }
        else 
        {
            other.GetComponent<EquippedWeapon>().weapon.allAmmo += 10;
            Destroy(ammoboxCanvas);
        }
    }
}
