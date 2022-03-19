using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class live_quests : MonoBehaviour
{
    public string firstQuest_title;
    public string firstQuest_info;
    
    private Text questTitle;
    private Text questInfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questInfo = GameObject.Find("txt_quest").GetComponent<Text>();
            questTitle = GameObject.Find("txt_questTitle").GetComponent<Text>();

            questTitle.text = firstQuest_title;
            questInfo.text = firstQuest_info;
        }
    }
}
