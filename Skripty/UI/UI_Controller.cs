using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public float timer, refresh, avgFramerate;
    private string display = "{0} FPS";
    public Text fps;

    public Image healthBar;

    public Image staminaBar;

    public Text coinsText;

    public Text ammoText;

    public Text triggerText;
    //private float currentHealth;
   // private float maxHealth;
    //private P_Controller player;

    private void Update()
    {

        if (UI_PauseMenu.gameIsPaused)
        {
            
        }
        else
        {
            float timelapse = Time.smoothDeltaTime;
            timer = timer <= 0 ? refresh : timer -= timelapse;

            if (timer <= 0) avgFramerate = (int) (1f / timelapse);
            fps.text = string.Format(display, avgFramerate.ToString());
        }

    }
    
    public void SetHealth(float health, float maxHealth)
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void SetStamina(float stamina, float maxStamina)
    {
        staminaBar.fillAmount = stamina / maxStamina;
    }

    public void SetCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void SetAmmo(int ammo, int maxAmmo)
    {
        ammoText.text = ammo + "/" + maxAmmo;
    }

    public void SetTrigger(int triggerType,string buttonChar, string itemName)
    {
        if (triggerType == 0)
        {
            triggerText.text = "Press " + buttonChar + " to pick up " + itemName;
        }

        if (triggerType == 1)
        {
            triggerText.text = "Press " + buttonChar + " open " + itemName;

        }

        if (triggerType == 2)
        {
            triggerText.text = "";
        }
    }
    
}