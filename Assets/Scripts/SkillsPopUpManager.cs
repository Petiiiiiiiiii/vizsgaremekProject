using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillsPopUpManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup;
    private bool isPopupVisible = false;
    public string title, desc, cost;
    private TextMeshProUGUI popup_t, popup_d, popup_c;

    private void Start()
    {
        popup_t = GameObject.Find("pop_up_title").GetComponent<TextMeshProUGUI>();
        popup_d = GameObject.Find("pop_up_desc").GetComponent<TextMeshProUGUI>();
        popup_c = GameObject.Find("pop_up_cost").GetComponent<TextMeshProUGUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        popup_t.text = title;
        popup_d.text = desc;
        popup_c.text = cost;
        popup.SetActive(true);
        isPopupVisible = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popup.SetActive(false);
        isPopupVisible = false;
    }

    private void Update()
    {
        if (isPopupVisible)
        {
            popup.transform.position = Input.mousePosition + new Vector3(210, 150, 0);
        }
    }
}
