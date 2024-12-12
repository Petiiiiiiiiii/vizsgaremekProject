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
    private bool waiting = false;

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
                Debug.Log("mukodik");
                SkillsUI.gameObject.SetActive(true);
                Debug.Log("lefutott");
                timer = 0f;
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
        LobbyUI.gameObject.SetActive(false);
        waiting = true;
        //itt nemjo meg
    }

    public void SkillsBackButton() 
    {
        animator.SetBool("Skills",false);
        SkillsUI.gameObject.SetActive(false);
        LobbyUI.gameObject.SetActive(true);
    }
}
