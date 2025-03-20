using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSys : MonoBehaviour
{
    public ParticleSystem levelUP;
    public GameObject levelUPtext;
    public void enemyKill() 
    {
        PlayerPrefs.SetInt("playerXP", PlayerPrefs.GetInt("playerXP") + 50);
        int xp = PlayerPrefs.GetInt("playerXP");
        int level = xp / 500;
        if((xp / 500) > PlayerPrefs.GetInt("playerLevel")) 
        {
            Debug.Log($"szint lépés : {level}-as szint | {xp} xp |xp/500 = {xp / 500} ");
            StartCoroutine(levelUPparticles());
        }
        PlayerPrefs.SetInt("playerLevel", level);
    }
    IEnumerator levelUPparticles() 
    {
        levelUP.Play();
        levelUPtext.SetActive(true);
        yield return new WaitForSeconds(3f);
        levelUPtext.SetActive(false);
        levelUP.Stop();
    }
    public void bossKill() 
    {
        PlayerPrefs.SetInt("playerXP", PlayerPrefs.GetInt("playerXP") + 500);
        int xp = PlayerPrefs.GetInt("playerXP");
        int level = xp / 500;
        if ((xp / 500) > PlayerPrefs.GetInt("playerLevel"))
        {
            Debug.Log($"szint lépés : {level}-as szint | {xp} xp |xp/500 = {xp / 500} ");
            StartCoroutine(levelUPparticles());
        }
        PlayerPrefs.SetInt("playerLevel", level);
    }

}
