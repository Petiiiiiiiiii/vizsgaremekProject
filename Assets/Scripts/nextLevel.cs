using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    public string nextSceneName;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger belépés: " + other.gameObject.name);
        SceneManager.LoadScene(nextSceneName);
    }
}
