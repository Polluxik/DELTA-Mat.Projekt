using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    private P_UpgradeShop upgradeShop;
    private bool isOptions = false;

    private GameObject _player;
    private GameObject _canvas;
    private GameObject _levelloader;
    private void Start()
    {
        _player = GameObject.Find("Player");
        _canvas = GameObject.Find("Canvas-Main");
        _levelloader = GameObject.Find("LevelLoader");

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (P_UpgradeShop.isUpgradeMenuOpen)
            {
                upgradeShop = GameObject.Find("UpgradeShop").GetComponentInChildren<P_UpgradeShop>();
                upgradeShop.Close();
            }
            else if (gameIsPaused && P_UpgradeShop.isUpgradeMenuOpen == false && isOptions != true)
            {
                Resume();
            }
            else if(isOptions != true)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MainMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Destroy(_player);
        Destroy(_canvas);
        Destroy(_levelloader);
        SceneManager.LoadScene(0);
    }

    public void OptionsMenu()
    {
        isOptions = true;
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        
    }

    public void BackToMenu()
    {
        isOptions = false;
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}