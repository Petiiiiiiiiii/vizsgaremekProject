using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    public GameObject warningText;
    private TMP_InputField nameInput, passInput, repassInput, emailInput;
    private Button regBTN;
    private void Start()
    {
        nameInput = GameObject.Find("nameINPUT").GetComponent<TMP_InputField>();
        passInput = GameObject.Find("passINPUT").GetComponent<TMP_InputField>();
        repassInput = GameObject.Find("repassINPUT").GetComponent<TMP_InputField>();
        emailInput = GameObject.Find("emailINPUT").GetComponent<TMP_InputField>();
        regBTN = GameObject.Find("RegBTN").GetComponent<Button>();
    }
    private class user 
    {
        public string username;
        public string passwordHash;
        public string email;
    }
    public void CallRegister()
    {
        StartCoroutine(Register());
        regBTN.interactable = false;
    }
    IEnumerator Register()
    {
        user user = new();

        user.username = nameInput.text;
        user.passwordHash = passInput.text;
        user.email = emailInput.text;

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost:7000/api/Players", JsonUtility.ToJson(user), "application/json");
        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Hálózati hiba: " + www.error + "\nResult: " + www.result);
        }
        else
        {
            switch (www.responseCode)
            {
                case 201:
                    StartCoroutine(Warning("Successful registration!",true));
                    break;
                default:
                    StartCoroutine(Warning("Something went wrong, try again later!", false));
                    Debug.Log("status code: " + www.responseCode);
                    regBTN.interactable = true;
                    break;
            }
        }
    }

    IEnumerator Warning(string warningMessage, bool success) 
    {
        if (success)
        {
            warningText.GetComponentInChildren<TextMeshProUGUI>().text = warningMessage;
            warningText.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Login");
        }
        else 
        {
            warningText.GetComponentInChildren<TextMeshProUGUI>().text = warningMessage;
            warningText.SetActive(true);
            yield return new WaitForSeconds(2f);
            warningText.SetActive(false);
        }

    }

    public void VerifyData() 
    {
        regBTN.interactable = (nameInput.text.Length >= 3 && passInput.text.Length >= 4 && passInput.text.Length >= 4 && emailInput.text.Contains("@") && emailInput.text.Contains(".") && emailInput.text.Length >= 5 && passInput.text == repassInput.text);
    }
}
