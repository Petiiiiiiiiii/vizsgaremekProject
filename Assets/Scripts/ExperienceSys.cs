using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSys : MonoBehaviour
{
    public void enemyKill() 
    {
        PlayerPrefs.SetInt("playerXP", PlayerPrefs.GetInt("playerXP") + 50);
        int xp = PlayerPrefs.GetInt("playerXP");
        int level = xp / 500;
        PlayerPrefs.SetInt("playerLevel", level);
        Debug.Log($"szint lépés : {level}-as szint | {xp} xp |xp/500 = {xp / 500} ");

    }

    public void bossKill() 
    {
        PlayerPrefs.SetInt("playerXP", PlayerPrefs.GetInt("playerXP") + 500);
        int xp = PlayerPrefs.GetInt("playerXP");
        int level = xp / 500;
        PlayerPrefs.SetInt("playerLevel", level);
        Debug.Log($"szint lépés : {level}-as szint | {xp} xp |xp/500 = {xp / 500} ");
    }

}
