using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PickUPKey : MonoBehaviour, IInteractable
{
    GameObject player;
    public void Die()
    {
        this.gameObject.SetActive(false);
    }

    public string GetDescription()
    {
        return "Key (Floor 2)";
    }

    public void Interact()
    {
        if (player != null) 
        {
            player.GetComponent<GotoNextLevel>().hasKey = true;
        } 
        else Debug.Log("Nincs meg a player");
    }

    void Start()
    {
        player = GameObject.Find(GameObject.Find("GameManager").GetComponent<GameManager>().player);
    }
}
