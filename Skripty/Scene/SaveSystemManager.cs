using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSystemManager : MonoBehaviour
{
    private CharacterController controller;
    private P_Controller player;
    private P_Upgrades upgrades;

    
    private void Update()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        //SceneLoaded(SceneManager.GetActiveScene(),LoadSceneMode.Single);
        if (controller != null)
        {
            if (Input.GetButtonDown("Quicksave"))
            {
                SavePlayer();
            }

            if (Input.GetButtonDown("Quickload"))
            {
                LoadPlayer();
            }
        }
    }

    public void SavePlayer()
    {
        if (!P_Controller.isJumping && player != null)
        {   
            player = GameObject.FindWithTag("Player").GetComponent<P_Controller>();
            upgrades = GameObject.FindWithTag("Player").GetComponentInChildren<P_Upgrades>();
            SaveSystem.SavePlayer(player, upgrades);
        }
    }

    public void LoadPlayer()
    {
        controller = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player").GetComponent<P_Controller>();
        upgrades = GameObject.FindWithTag("Player").GetComponentInChildren<P_Upgrades>();
        PlayerData data = SaveSystem.LoadPlayer();
        
        upgrades.primaryId = data.primaryWeaponId;
        upgrades.secondaryId = data.secondaryWeaponId;
        
        player.Health = data.health;
        player.Stamina = data.stamina;
        player.Coins = data.coins;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        //print(position);
        controller.enabled = false;
        player.transform.position = position;
        controller.enabled = true;

        //Debug.Log("LOADED");
        //pauseMenu.Resume();
    }

    public void LoadPlayerScene()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        levelLoader.LoadLevel(data.sceneIndex);
    }
}
