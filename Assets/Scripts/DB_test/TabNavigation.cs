using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TabNavigation : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    private int currentIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentIndex = (currentIndex + 1) % inputFields.Length;
            EventSystem.current.SetSelectedGameObject(inputFields[currentIndex].gameObject);
            inputFields[currentIndex].ActivateInputField();
        }
    }

    public void gotoLogin() 
    {
        SceneManager.LoadScene("Login");
    }

    public void gotoReg() 
    {
        SceneManager.LoadScene("Register");
    }
}
