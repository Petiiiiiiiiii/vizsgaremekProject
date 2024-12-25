using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PickupAmmo : MonoBehaviour, IInteractable
{
    GameObject player;
    public void Die()
    {
        this.gameObject.SetActive(false);
    }

    public string GetDescription()
    {
        return "Ammopack";
    }

    public void Interact()
    {
        if (player != null) 
        {
            player.GetComponent<EquippedWeapon>().weapon.allAmmo += 60;
        } 
        else Debug.Log("Nincs meg a player");
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }
}
