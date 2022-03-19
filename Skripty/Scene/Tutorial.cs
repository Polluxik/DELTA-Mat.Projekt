using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    GameObject[] enemies;
    private bool nextMap = false;
    public LevelLoader levelLoader;
    

    public void LoadNextMap(int sceneNumber)
    {
        //SceneManager.LoadScene(sceneNumber);
    }

    private void Update()
    {
        if(levelLoader == null)
        {
            levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //print(enemies.Length);
            if (enemies.Length == 0 && nextMap == false)
            {
                nextMap = true;
                levelLoader.LoadLevel(2);
            }
        }
        
        
    }
}
