using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_WeaponSwitcher : MonoBehaviour
{
    public int selectedWeapon = 0;
    public string equippedWeapon = "";
    public bool reloading = false;
    public bool isTutorial = false;
    public bool tutorialEquip;
    private SceneSettings sceneSettings;

    void Start()
    {
        sceneSettings = GameObject.FindWithTag("Scene Settings").GetComponent<SceneSettings>();

        SelectWeapon();
        isTutorial = sceneSettings.tutorial;
        tutorialEquip = false;
    }

    void Update()
    {
        if (!UI_PauseMenu.gameIsPaused)
        {
            CheckTutorial();
            if (isTutorial == true && tutorialEquip == false)
            {
                foreach (Transform weapon in transform)
                {
                    weapon.gameObject.SetActive(false);
                }
            }
            else if (isTutorial == true && tutorialEquip == true)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    transform.gameObject.SetActive(true);
                    selectedWeapon = 0;
                    SelectWeapon();
                }
            }

            else if (isTutorial == false && tutorialEquip == false)
            {
                if (reloading == false)
                {
                    int previousSelectWeapon = selectedWeapon;
                    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                    {
                        if (selectedWeapon >= transform.childCount - 1) selectedWeapon = 0;
                        else selectedWeapon++;
                    }

                    if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                    {
                        if (selectedWeapon <= 0) selectedWeapon = transform.childCount - 1;
                        else selectedWeapon--;
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        selectedWeapon = 0;
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
                    {
                        selectedWeapon = 1;
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
                    {
                        selectedWeapon = 2;
                    }

                    if (previousSelectWeapon != selectedWeapon)
                    {
                        SelectWeapon();
                    }
                }
            }
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                equippedWeapon = weapon.name;
            }

            else
                weapon.gameObject.SetActive(false);

            i++;
        }
    }

    private void CheckTutorial()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;
        //print(sceneIndex);
        if (sceneIndex != 1)
        {
            isTutorial = false;
            tutorialEquip = false;
        }
    }
}