using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHP : MonoBehaviour, IInteractable
{
    private GameObject player;

    public void Die()
    {
        this.gameObject.SetActive(false);
    }

    public string GetDescription()
    {
        return "Medkit";
    }

    public void Interact()
    {
        if (player != null) player.GetComponent<HealthSystem>().Heal(50);
        else Debug.Log("nincs meg a player");
    }

    private void Start()
    {
        player = GameObject.Find("player");
    }
}
