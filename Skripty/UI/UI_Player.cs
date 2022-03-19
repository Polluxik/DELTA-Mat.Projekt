using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Player : MonoBehaviour
{
    public Slider slHealth;
    public Slider slStamina;
    public Text txtCoins;
    public Text txtStamina;
    public Text txtDebug;
    public Text txtAmmo;
    public Text txtHealth;

    public void SetMaxHealth(float health)
    {
        slHealth.maxValue = health;
        slHealth.value = health;
    }

    public void SetHealth(float health, float maxHealth)
    {
        slHealth.value = health;
        txtHealth.text = Math.Round(health,0) + "/" + maxHealth;
    }

    public void SetMaxStamina(float stamina)
    {
        slStamina.maxValue = stamina;
        slStamina.value = stamina;
    }

    public void SetStamina(float stamina, float maxStamina)
    {
        slStamina.value = stamina;
        txtStamina.text = Math.Round(stamina,0) + "/" + maxStamina;
    }

    public void SetCoins(int coins)
    {
        txtCoins.text = "" + coins.ToString();
    }

    public void SetDebug(float damage, float health)
    {
        txtDebug.text = "Dealt damage: " + Math.Round(damage) + " | Enemy hp: " + health;
    }
    public void SetAmmo(int ammo, int ammoStash)
    {
        txtAmmo.text = ammo + "/" + ammoStash;
    }
}