using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegisterHandler : MonoBehaviour
{
    public TMP_InputField[] inputFields;

    [SerializeField] Canvas loginCanvas;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            for (int i = 0; i < inputFields.Length; i++)
            {
                if (inputFields[i].isFocused)
                {
                    int nextIndex = (i + 1) % inputFields.Length;
                    inputFields[nextIndex].Select();
                    break;
                }
            }
        }
    }

    public void AlreadyHaveAnAccount() 
    {
        Debug.Log("asd");
        loginCanvas.gameObject.SetActive(true);
    }
}
