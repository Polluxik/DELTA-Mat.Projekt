using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1_objectives : MonoBehaviour
{
    
    [Header("Collectibles")]
    public static bool pickedCard = false;
    public static int _cardCount = 0;
    public static bool pickedCase = false;

    [Header("Actions")]
    public static bool canOpenDoors = false;
    public static bool canOpenDoorsFake = false;


    public static bool barFirstTime;
    public static bool killedAllEnemies;
    public static bool talkedToGuy;
    public static bool pickedAllCards;
    public static bool pickedSuitcase;
    public static bool barSecondTime;

    private bool kill = true;

    public GameObject talkToGuy;
    public GameObject enemiesBar;
    public GameObject suitcase;
    public GameObject lastTrigger;


    private void Update()
    {
        if (!barFirstTime)
        {
            Debug.LogWarning("You need to see bar for the first time.");
            return;
        }
        CheckEnemies();
        if (!killedAllEnemies)
        {
            Debug.LogWarning("You need to kill all enemies.");
            return;
        }
        
        if(!talkedToGuy)
        {
            Debug.LogWarning("You need to talk with a guy");
            return;
        }
        if (!pickedAllCards)
        {
            Debug.LogWarning("You need to pick up all cards.");
            return;
        }

        if (!pickedSuitcase)
        {
            Debug.LogWarning("You need to pick up your suitcase.");
            return;
        }

        if (!barSecondTime)
        {
            Debug.LogWarning("You need to see bar for the second time.");
            return;
        }


    }

    private void CheckEnemies()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0 && enemiesBar.activeSelf == true && !talkedToGuy)
        {
            killedAllEnemies = true;
            talkToGuy.SetActive(true);
        }
    }
    public void showSuitcase()
    {
        suitcase.SetActive(true);
    }
    public void showLastTrigger()
    {
        lastTrigger.SetActive(true);
    }
}
