using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField] private int availableSP;
    [SerializeField] private TextMeshProUGUI skillsSP_text;
    [SerializeField] private TextMeshProUGUI lobbySP_text;

    [SerializeField] private List<Button> btns;
    [SerializeField] private List<GameObject> checkmarks;

    [SerializeField] private GameObject errorPanel;

    private void Update()
    {
        //availableSP = PlayerPrefs.GetInt("SP");
        skillsSP_text.text = availableSP.ToString() + " SP";
        lobbySP_text.text = availableSP.ToString() + " SP";
    }

    public void dmg_boost() 
    {
        if (availableSP >= 5)
        {
            availableSP -= 5;
            Debug.Log("dmg megvéve");
            btns[0].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[0].SetActive(true);
        }
        else StartCoroutine(ErrorPoup());
    }
    public void hp_boost() 
    {
        if (availableSP >= 5)
        {
            availableSP -= 5;
            Debug.Log("hp megvéve");
            btns[1].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[1].SetActive(true);
        }
        else StartCoroutine(ErrorPoup());
    }
    public void firerate_boost() 
    {
        if (availableSP >= 5)
        {
            availableSP -= 5;
            Debug.Log("firerate megvéve");
            btns[2].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[2].SetActive(true);
        }
        else StartCoroutine(ErrorPoup());
    }
    public void headshot_boost() 
    {
        if (availableSP >= 5)
        {
            availableSP -= 5;
            Debug.Log("headshot megvéve");
            btns[3].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[3].SetActive(true);
        }
        else StartCoroutine(ErrorPoup());
    }
    public void movement_boost() 
    {
        if (availableSP >= 5)
        {
            availableSP -= 5;
            Debug.Log("movement megvéve");
            btns[4].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[4].SetActive(true);
        }
        else StartCoroutine(ErrorPoup());
    }
    public void mag_boost() 
    {
        if (availableSP >= 5)
        {
            availableSP -= 5;
            Debug.Log("mag megvéve");
            btns[5].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[5].SetActive(true);
        }
        else StartCoroutine(ErrorPoup());
    }

    public void AR_unlock() 
    {
        if (availableSP >= 10)
        {
            availableSP -= 10;
            Debug.Log("AR megvéve");
            btns[6].gameObject.GetComponent<Button>().enabled = false;
            checkmarks[6].SetActive(true);
        }
        else StartCoroutine(ErrorPoup());
    }

    IEnumerator ErrorPoup() 
    {
        errorPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        errorPanel.SetActive(false);
    }
}
