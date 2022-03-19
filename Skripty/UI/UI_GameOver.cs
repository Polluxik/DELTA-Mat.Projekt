using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{

    public LevelLoader levelLoader;
    public void MainMenu()
    {
            SceneManager.LoadScene(0);
    }
}