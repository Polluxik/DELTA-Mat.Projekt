using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [HideInInspector] public string objectID;
    public bool Destroy = true;
    private void Awake()
    {
        objectID = name + transform.position;
    }

     void Start()
     {
         if (Destroy)
         {
             for (int i = 0; i < FindObjectsOfType<DontDestroy>().Length; i++)
             {
                 if (FindObjectsOfType<DontDestroy>()[i] != this)
                 {
                     if (FindObjectsOfType<DontDestroy>()[i].objectID == objectID)
                     {
                         Destroy(gameObject);
                     }
                 }
             }
             DontDestroyOnLoad(gameObject);
         }
         else
         {
             
         }
     }
}
