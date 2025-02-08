using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Canvas LobbyUI;
    [SerializeField] Canvas SkillsUI;
    [SerializeField] Canvas PlayUI;

    private float timer = 0f;
    private float timer2 = 0f;
    private bool waiting = false;
    private bool waiting2 = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (waiting)
        {
            timer += Time.deltaTime;
            if (timer >= 2.55f)
            {
                waiting = false;
                SkillsUI.gameObject.SetActive(true);
                timer = 0f;
            }
        }

        if (waiting2)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= 2.55f)
            {
                waiting2 = false;
                LobbyUI.gameObject.SetActive(true);
                timer2 = 0f;
            }
        }
    }

    public void PlayButton() 
    {
        animator.SetBool("Play",true);
        PlayUI.gameObject.SetActive(true);
        LobbyUI.gameObject.SetActive(false);
    }

    public void PlayBackButton() 
    {
        animator.SetBool("Play", false);
        PlayUI.gameObject.SetActive(false);
        LobbyUI.gameObject.SetActive(true);
    }

    public void SkillsButton() 
    {
        animator.SetBool("Skills", true);
        waiting = true;
        LobbyUI.gameObject.SetActive(false);
    }

    public void SkillsBackButton() 
    {
        animator.SetBool("Skills",false);
        waiting2 = true;
        SkillsUI.gameObject.SetActive(false);
    }
}
