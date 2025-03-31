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

    private void Start()
    {
        if (PlayerPrefs.HasKey("AR_weapon")) 
        {
            switch (PlayerPrefs.GetString("AR_weapon"))
            {
                case "unlocked":
                    btns[6].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[6].SetActive(true);
                    break;
                case "locked":
                    btns[6].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[6].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("dmg_boost"))
        {
            switch (PlayerPrefs.GetString("dmg_boost"))
            {
                case "unlocked":
                    btns[0].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[0].SetActive(true);
                    break;
                case "locked":
                    btns[0].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[0].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("hp_boost"))
        {
            switch (PlayerPrefs.GetString("hp_boost"))
            {
                case "unlocked":
                    btns[1].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[1].SetActive(true);
                    break;
                case "locked":
                    btns[0].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[0].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("headshot_boost"))
        {
            switch (PlayerPrefs.GetString("headshot_boost"))
            {
                case "unlocked":
                    btns[3].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[3].SetActive(true);
                    break;
                case "locked":
                    btns[3].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[3].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("mag_boost"))
        {
            switch (PlayerPrefs.GetString("mag_boost"))
            {
                case "unlocked":
                    btns[5].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[5].SetActive(true);
                    break;
                case "locked":
                    btns[5].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[5].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("firerate_boost"))
        {
            switch (PlayerPrefs.GetString("firerate_boost"))
            {
                case "unlocked":
                    btns[2].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[2].SetActive(true);
                    break;
                case "locked":
                    btns[2].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[2].SetActive(false);
                    break;
                default:
                    break;
            }
        }

        if (PlayerPrefs.HasKey("movement_boost"))
        {
            switch (PlayerPrefs.GetString("movement_boost"))
            {
                case "unlocked":
                    btns[4].gameObject.GetComponent<Button>().enabled = false;
                    checkmarks[4].SetActive(true);
                    break;
                case "locked":
                    btns[4].gameObject.GetComponent<Button>().enabled = true;
                    checkmarks[4].SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

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
            PlayerPrefs.SetString("hp_boost", "unlocked");
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
            PlayerPrefs.SetString("AR_weapon","unlocked");
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
