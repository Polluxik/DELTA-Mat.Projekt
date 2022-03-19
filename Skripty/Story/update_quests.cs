using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class update_quests : MonoBehaviour
{
    public string firstQuest_title;
    public string firstQuest_info;
    
    private Text questTitle;
    private Text questInfo;
    // Start is called before the first frame update
    void Awake()
    {
        questInfo = GameObject.Find("txt_quest").GetComponent<Text>();
        questTitle = GameObject.Find("txt_questTitle").GetComponent<Text>();

        questTitle.text = firstQuest_title;
        questInfo.text = firstQuest_info;
    }

}
