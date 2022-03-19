using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equip : MonoBehaviour
{
    private P_WeaponSwitcher weaponSwitcher;
    public GameObject enemies;

    private void Start()
    {
        weaponSwitcher = GameObject.FindWithTag("Player").GetComponentInChildren<P_WeaponSwitcher>();
    }

    private void OnTriggerEnter(Collider other)
    {
        weaponSwitcher.tutorialEquip = true;
        enemies.SetActive(false);
    }
}
