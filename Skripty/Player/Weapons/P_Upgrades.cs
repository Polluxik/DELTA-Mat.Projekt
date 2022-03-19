using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_Upgrades : MonoBehaviour
{
    public Transform primaryWeapon;
    public Transform secondaryWeapon;
    public Transform meleeWeapon;

    public int primaryId;
    public int secondaryId;
    public int meleeId;
    

    private void Update()
    {
        upgradePrimaryWeapon();
        upgradeSecondaryWeapon();
        upgradeMeleeWeapon();
    }
    
    public void upgradePrimaryWeapon()
    {
        int i = 0;
        foreach (Transform weapon in primaryWeapon)
        {
            
            if (i == primaryId)
            {
                weapon.gameObject.SetActive(true);
            }
                
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public void upgradeSecondaryWeapon()
    {
        int i = 0;
        foreach (Transform weapon in secondaryWeapon)
        {
            
            if (i == secondaryId)
            {
                weapon.gameObject.SetActive(true);
            }
                
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
    public void upgradeMeleeWeapon()
    {
        int i = 0;
        foreach (Transform weapon in meleeWeapon)
        {
            
            if (i == meleeId)
            {
                weapon.gameObject.SetActive(true);
            }
                
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}