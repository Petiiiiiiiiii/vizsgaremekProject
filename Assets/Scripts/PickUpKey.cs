using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PickUPKey : MonoBehaviour, IInteractable
{
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
        GameObject.Find("GameManager").GetComponent<GameManager>().hasKey = true;
    }
}
