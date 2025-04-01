using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMainDoor : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        animator.Play("closeDoor");
        Debug.Log("Trigger belépés: " + other.gameObject.name);
    }

}
