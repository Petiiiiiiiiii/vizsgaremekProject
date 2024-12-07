using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Canvas LobbyUI;
    [SerializeField] Canvas SkillsUI;

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
                LobbyUI.gameObject.SetActive(false);
                SkillsUI.gameObject.SetActive(true);
            }
        }
    }

    public void PlayButton() 
    {
        animator.SetBool("Skills", false);
        animator.SetBool("Play",true);
    }

    public void SkillsButton() 
    {
        animator.SetBool("Play", false);
        animator.SetBool("Skills", true);
        waiting = true;
        //back fuggvenyre false
    }
}
