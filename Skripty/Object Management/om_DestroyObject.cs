using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class om_DestroyObject : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5; 
    void Update()
    {
        Destroy(gameObject,timeToDestroy);
    }
}
