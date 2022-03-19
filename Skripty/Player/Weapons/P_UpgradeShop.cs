using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_UpgradeShop : MonoBehaviour
{
    private bool isTriggering;
    private P_Upgrades ids;
    private P_Controller player;
    public GameObject upgradeMenuUI;
    public static bool isUpgradeMenuOpen;

    public int primaryLvl2,
        primaryLvl3,
        secondaryLvl2,
        secondaryLvl3,
        maxHealthLvl2,
        maxHealthLvl3,
        maxStaminaLvl2,
        maxStaminaLvl3,

        maxStaminaRegenLvl2,
        maxStaminaRegenLvl3;

    public float
        maxHealthValueLvl2,
        maxHealthValueLvl3,
        maxStaminaValueLvl2,
        maxStaminaValueLvl3,
        maxStaminaRegenValueLvl2,
        maxStaminaRegenValueLvl3;

    private void Start()
    {
        isTriggering = false;
        player = GameObject.FindWithTag("Player").GetComponent<P_Controller>();
        ids = GameObject.FindWithTag("Player").GetComponentInChildren<P_Upgrades>();
        Debug.LogWarning(player, ids);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("player Detection");
            isTriggering = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("player not detected");
            isTriggering = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Use") && UI_PauseMenu.gameIsPaused == false && isTriggering == true)
        {
            if (isUpgradeMenuOpen)
            {
                Close();
            }
            else
            {
                Show();
            }
        }
    }


    public void Close()
    {
        upgradeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        UI_PauseMenu.gameIsPaused = false;

        isUpgradeMenuOpen = false;
    }
    
    public void Show()
    {
        upgradeMenuUI.SetActive(true);
        Time.timeScale = 0f;
        UI_PauseMenu.gameIsPaused = true;

        isUpgradeMenuOpen = true;
    }


    public void upgradePrimary()
    {
        if (ids.primaryId == 0 && player.Coins >= primaryLvl2)
        {
            ids.primaryId += 1;
            player.TakeCoins(primaryLvl2);
            string levelText = GameObject.Find("level_primary").GetComponent<Text>().text = "lvl. 2";
            string priceText = GameObject.Find("price_primary").GetComponent<Text>().text = primaryLvl3 + " price";

        }
        else if (ids.primaryId == 1 && player.Coins >= primaryLvl3)
        {
            ids.primaryId += 1;
            player.TakeCoins(primaryLvl3);
            string levelText = GameObject.Find("level_primary").GetComponent<Text>().text = "MAX";
            string buttonText = GameObject.Find("price_primary").GetComponent<Text>().text = "MAX";

        }
        else
        {
            print("Not Enough Coins!");
        }
    }

    public void upgradeSecondary()
    {
        if (ids.secondaryId == 0 && player.Coins >= secondaryLvl2)
        {
            ids.secondaryId += 1;
            player.TakeCoins(secondaryLvl2);
            string levelText = GameObject.Find("level_secondary").GetComponent<Text>().text = "lvl. 2";
            string priceText = GameObject.Find("price_secondary").GetComponent<Text>().text = secondaryLvl3 + " price";
        }
        else if (ids.secondaryId == 1 && player.Coins >= secondaryLvl3)
        {
            ids.secondaryId += 1;
            player.TakeCoins(secondaryLvl3);
            string levelText = GameObject.Find("level_secondary").GetComponent<Text>().text = "MAX";
            string buttonText = GameObject.Find("price_secondary").GetComponent<Text>().text = "MAX";
        }
        else
        {
            print("Not Enough Coins!");
        }
    }

    public void upgradeMaxHealth()
    {
        if (player.maxHealthLevel == 0 && player.Coins >= maxHealthLvl2)
        {
            player.maxHealthLevel += 1;
            player.setMaxHealth(maxHealthValueLvl2);
            player.TakeCoins(maxHealthLvl2);
            string levelText = GameObject.Find("level_HP").GetComponent<Text>().text = "lvl. 2";
            string priceText = GameObject.Find("price_HP").GetComponent<Text>().text = primaryLvl3 + " price";
        }
        else if (player.maxHealthLevel == 1 && player.Coins >= maxHealthLvl3)
        {
            player.maxHealthLevel += 1;
            player.setMaxHealth(maxHealthValueLvl3);
            player.TakeCoins(maxHealthLvl3);
            string levelText = GameObject.Find("level_HP").GetComponent<Text>().text = "MAX";
            string buttonText = GameObject.Find("price_HP").GetComponent<Text>().text = "MAX";
            
        }
    }

    public void upgradeMaxStamina()
    {
        if (player.maxStaminaLevel == 0 && player.Coins >= maxStaminaLvl2)
        {
            player.maxStaminaLevel += 1;
            player.setMaxStamina(maxStaminaValueLvl2);
            player.TakeCoins(maxStaminaLvl2);
            string levelText = GameObject.Find("level_Stamina").GetComponent<Text>().text = "lvl. 2";
            string priceText = GameObject.Find("price_Stamina").GetComponent<Text>().text = primaryLvl3 + " price";
        }
        else if (player.maxStaminaLevel == 1 && player.Coins >= maxStaminaLvl3)
        {
            player.maxStaminaLevel += 1;
            player.setMaxStamina(maxStaminaValueLvl3);
            player.TakeCoins(maxStaminaLvl3);
            string levelText = GameObject.Find("level_Stamina").GetComponent<Text>().text = "MAX";
            string buttonText = GameObject.Find("price_Stamina").GetComponent<Text>().text = "MAX";
            
        }

    }

    public void upgradeStaminaRegen()
    {
        if (player.maxStaminaRegenLevel == 0 && player.Coins >= maxStaminaRegenLvl2)
        {
            player.maxStaminaRegenLevel += 1;
            player.setStaminaRegen(maxStaminaRegenValueLvl2);
            player.TakeCoins(maxStaminaRegenLvl2);
            string levelText = GameObject.Find("level_StaminaRegen").GetComponent<Text>().text = "lvl. 2";
            string priceText = GameObject.Find("price_StaminaRegen").GetComponent<Text>().text = primaryLvl3 + " price";
        }
        else if (player.maxStaminaRegenLevel == 1 && player.Coins >= maxStaminaRegenLvl3)
        {
            player.maxStaminaRegenLevel += 1;
            player.setStaminaRegen(maxStaminaRegenValueLvl3);
            player.TakeCoins(maxStaminaRegenLvl3);
            string levelText = GameObject.Find("level_StaminaRegen").GetComponent<Text>().text = "MAX";
            string buttonText = GameObject.Find("price_StaminaRegen").GetComponent<Text>().text = "MAX";
            
        }
    }
}