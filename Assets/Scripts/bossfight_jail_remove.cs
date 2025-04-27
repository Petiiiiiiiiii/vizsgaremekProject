using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossfight_jail_remove : MonoBehaviour
{
    public GameObject waveManager;
    public GameObject jailDoor;

    public TextMeshProUGUI firstObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            this.GetComponentInParent<Animator>().Play("bossfight_jail_remove");
            waveManager.GetComponent<waveManager>().StartWave();
            jailDoor.transform.rotation = Quaternion.Euler(0,180,0);
            firstObj.fontStyle = FontStyles.Strikethrough;
            firstObj.color = Color.black;
        }
    }
}
