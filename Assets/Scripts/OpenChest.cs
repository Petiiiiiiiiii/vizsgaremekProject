using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour, IInteractable
{
    GameObject player;
    public void Die(){}

    public string GetDescription()
    {
        return "Boss's Chest";
    }

    public void Interact()
    {
        if ((player != null))
        {
            this.GetComponent<Animator>().Play("map1-bossChest");
            Destroy(this.GetComponent<OpenChest>());
        }
        else Debug.Log("Nincs meg a player");
    }

    private void Start()
    {
        player = GameObject.Find(GameObject.Find("GameManager").GetComponent<GameManager>().player);
    }

}
