using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class showHidePassword : MonoBehaviour
{
    private TMP_InputField pass, repass;
    public GameObject passShow, passHide, repassShow, repassHide;

    private void Start()
    {
        pass = GameObject.Find("passINPUT").GetComponent<TMP_InputField>();
        if (GameObject.Find("repassINPUT")) repass = GameObject.Find("repassINPUT").GetComponent<TMP_InputField>();
        else Debug.Log("Nem található a repassINPUT (Login sceneben nem lényeg!)");

    }

    public void ShowPass()
    {
        pass.contentType = TMP_InputField.ContentType.Standard;
        pass.textComponent.fontSize = 14;
        pass.ForceLabelUpdate();

        if (passShow != null) passShow.SetActive(false);
        if (passHide != null) passHide.SetActive(true);
    }

    public void HidePass()
    {
        pass.contentType = TMP_InputField.ContentType.Password;
        pass.textComponent.fontSize = 18;
        pass.ForceLabelUpdate();

        if (passHide != null) passHide.SetActive(false);
        if (passShow != null) passShow.SetActive(true);
    }

    public void ShowRepass()
    {
        repass.contentType = TMP_InputField.ContentType.Standard;
        repass.textComponent.fontSize = 14;
        repass.ForceLabelUpdate();

        if (repassShow != null) repassShow.SetActive(false);
        if (repassHide != null) repassHide.SetActive(true);
    }

    public void HideRepass()
    {
        repass.contentType = TMP_InputField.ContentType.Password;
        repass.textComponent.fontSize = 18;
        repass.ForceLabelUpdate();

        if (repassHide != null) repassHide.SetActive(false);
        if (repassShow != null) repassShow.SetActive(true);
    }
}
