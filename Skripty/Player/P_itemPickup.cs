using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_itemPickup : MonoBehaviour
{
    public string modelName;
    private bool isTriggering = false;
    public GameObject ShowPickUp;
    private UI_Controller UIController;

    private void Start()
    {
        
        UIController = FindObjectOfType<UI_Controller>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggering = true;
            
            ShowPickUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggering = false;
        ShowPickUp.SetActive(false);

    }

    private void Update()
    {
        if(isTriggering)
            Triggers();
    }
    private void Triggers()
    {
        if (Input.GetButtonDown("Use"))
        {
            if (transform.name == modelName)
            {
                //lvl1_objectives.pickedCard = true;
                Destroy(gameObject);
                ShowPickUp.SetActive(false);
            }
        }
    }
}
