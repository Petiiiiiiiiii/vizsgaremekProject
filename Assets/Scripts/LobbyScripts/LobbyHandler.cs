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

    public Animation playAnim;
    public Animation playReverseAnim;
    public AnimationClip skillsAnim;
    public Animation skillsReverseAnim;

    public void PlayButton() 
    {
        StartCoroutine(WaitForAnimPlay(true));  
    }

    public void PlayBackButton() 
    {
        StartCoroutine(WaitForAnimPlay(false));
    }

    public void SkillsButton() 
    {
        StartCoroutine(WaitForAnimSkills(true));
    }

    public void SkillsBackButton() 
    {
        StartCoroutine(WaitForAnimSkills(false));
    }

    IEnumerator WaitForAnimSkills(bool value) 
    {
        if (value)
        {
            animator.SetBool("Skills",value);
            LobbyUI.gameObject.SetActive(!value);
            yield return new WaitForSeconds(2.6f);
            SkillsUI.gameObject.SetActive(value);
        }
        else 
        {
            animator.SetBool("Skills", value);
            SkillsUI.gameObject.SetActive(value);
            yield return new WaitForSeconds(2.6f);
            LobbyUI.gameObject.SetActive(!value);
        }
    }
    IEnumerator WaitForAnimPlay(bool value)
    {
        if (value)
        {
            animator.SetBool("Play", value);
            LobbyUI.gameObject.SetActive(!value);
            yield return new WaitForSeconds(1f);
            PlayUI.gameObject.SetActive(value);
        }
        else 
        {
            animator.SetBool("Play", value);
            PlayUI.gameObject.SetActive(value);
            yield return new WaitForSeconds(1f);
            LobbyUI.gameObject.SetActive(!value);
        }

    }
}
