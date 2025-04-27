using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenMap2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCounter();
    }

    public void EnemyCounter() 
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        int enemyCount = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == enemyLayer)
            {
                enemyCount++;
            }
        }

        if (enemyCount == 0) 
        {
            this.transform.rotation = Quaternion.Euler(0,90,0);
        }
    }
}
