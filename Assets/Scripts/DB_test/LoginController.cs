using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    private TMP_InputField nameInput, passInput;
    private Button logBTN;
    public GameObject warningText;
    private void Start()
    {
        nameInput = GameObject.Find("nameINPUT").GetComponent<TMP_InputField>();
        passInput = GameObject.Find("passINPUT").GetComponent<TMP_InputField>();
        logBTN = GameObject.Find("LoginBTN").GetComponent<Button>();
    }
    public void CallLogin() 
    {
        StartCoroutine(Upload());
        logBTN.interactable = false;
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameInput.text);
        form.AddField("pass", passInput.text);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/dungeonmaster/login.php", form);
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
                    StartCoroutine(Warning("Successful login!",true));
                    break;
                case "2: User not found":
                    StartCoroutine(Warning("Username not found!",false));
                    logBTN.interactable = true;
                    break;
                case "3: Incorrect password":
                    StartCoroutine(Warning("Incorrect password!", false));
                    logBTN.interactable = true;
                    break;
                default:
                    StartCoroutine(Warning("Something went wrong, try again later!", false));
                    logBTN.interactable = true;
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
            PlayerPrefs.SetString("Username",nameInput.text);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Lobby");
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
        logBTN.interactable = (nameInput.text.Trim() != "" && passInput.text.Trim() != "" && passInput.text.Length >= 4 && nameInput.text.Length >= 3);
    }
}
