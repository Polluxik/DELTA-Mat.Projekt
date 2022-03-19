using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float stamina;
    public int coins;
    public float[] position;
    public int primaryWeaponId;
    public int secondaryWeaponId;
    public int sceneIndex;


    public PlayerData(P_Controller player, P_Upgrades upgrades)
    {

        sceneIndex = player.sceneIndex;

        health = player.Health;
        stamina = player.Stamina;
        coins = player.Coins;

        primaryWeaponId = upgrades.primaryId;
        secondaryWeaponId = upgrades.secondaryId;
    
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }


}
