using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1_enemySpawn : MonoBehaviour
{
    public GameObject spawner;

    public void SpawnEnemy()
    {
        spawner.SetActive(true);
    }
}
