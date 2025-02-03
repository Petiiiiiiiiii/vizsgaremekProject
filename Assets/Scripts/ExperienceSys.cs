using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSys : MonoBehaviour
{
    public void enemyKill() 
    {
        PlayerPrefs.SetInt("playerXP", PlayerPrefs.GetInt("playerXP") + 50);
    }

    public void bossKill() 
    {
        PlayerPrefs.SetInt("playerXP", PlayerPrefs.GetInt("playerXP") + 500);
    }

}
