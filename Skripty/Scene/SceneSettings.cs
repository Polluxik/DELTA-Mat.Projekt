using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    public bool tutorial = false;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    private Transform player;
    private GameObject LoadingScreen;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        CharacterController cc = player.GetComponent<CharacterController>();
        //LoadingScreen = GameObject.Find("LoadingScreen");
        cc.enabled = false;
        player.position = spawnPosition;
        player.rotation = spawnRotation;
        cc.enabled = true;
        
        
    }

    private void Update()
    {
        // if (Input.GetButtonDown("Fire3"))
        // {
        //     if (LoadingScreen.activeSelf == true)
        //     {
        //         LoadingScreen.SetActive(false);
        //
        //     }
        //
        // }
    }
}