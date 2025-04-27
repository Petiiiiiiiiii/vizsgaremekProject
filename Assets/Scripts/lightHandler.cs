using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightHandler : MonoBehaviour
{
    public List<GameObject> lights;
    public GameObject jailDoor;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].SetActive(true);
            jailDoor.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
        }
        jailDoor.transform.rotation = Quaternion.Euler(0,270,0);
    }
}
