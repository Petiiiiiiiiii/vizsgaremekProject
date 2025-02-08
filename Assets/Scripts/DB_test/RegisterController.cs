using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public void CallRegister()
    {
        StartCoroutine(Upload());
        regBTN.interactable = false;
    }
    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameInput.text);
        form.AddField("pass", passInput.text);
        form.AddField("email", emailInput.text);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/dungeonmaster/register.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Hálózati hiba: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;

            switch (response)
            {
                case "0":
                    StartCoroutine(Warning("Successful registration!",true));
                    break;
                case "3: Name already exists":
                    StartCoroutine(Warning("Username already exists!",false));
                    regBTN.interactable = true;
                    break;
                case "5: Email already exists":
                    StartCoroutine(Warning("Email already exists!",false));
                    regBTN.interactable = true;
                    break;
                default:
                    StartCoroutine(Warning("Something went wrong, try again later!", false));
                    Debug.Log(response);
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
