using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoNextLevel : MonoBehaviour, IInteractable
{
    public void Die() { }

    public string GetDescription()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().hasKey)
        {
            return "Open";
        }
        else 
        {
            return "You have to find the key of the Floor 2";
        }
    }

    public void Interact()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().hasKey)
        {
            this.GetComponent<Animator>().Play("openDoor");
        }
        else { }
    }

}
