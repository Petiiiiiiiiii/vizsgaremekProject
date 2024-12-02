using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        
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

        //back fuggvenyre false
    }
}
