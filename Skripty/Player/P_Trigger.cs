using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class P_Trigger : MonoBehaviour
{
    private bool isTriggering = false;
    private GameObject ShowTrigger;
    private lvl1_objectives story;
    //public GameObject questUpdate;
    private UI_Controller UIController;
    public P_Controller character;

    private Text questTitle;
    private Text questInfo;

    public Animator animator;

    public Triggers triggerType;

    private bool enemyFinished = false;

    private void Start()
    {
        ShowTrigger = GameObject.Find("TriggerText");
        questInfo = GameObject.Find("txt_quest").GetComponent<Text>();
        questTitle = GameObject.Find("txt_questTitle").GetComponent<Text>();
        UIController = FindObjectOfType<UI_Controller>();
        story = GameObject.Find("StoryManager").GetComponent<lvl1_objectives>();
        character = GameObject.Find("Player").GetComponent<P_Controller>();
    }

    void Update()
    {
        if (isTriggering)
            Triggers();
        if(lvl1_objectives.killedAllEnemies && !enemyFinished)
        {
            enemyFinished = true;
            questInfo.text = "Talk to guy";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggering = true;
            if(triggerType != global::Triggers.Trigger) ShowTrigger.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggering = false;
        if (triggerType != global::Triggers.Trigger) ShowTrigger.SetActive(false);
    }


    private void Triggers()
    {
        if (triggerType == global::Triggers.PickUp)
        {
            UIController.SetTrigger(0, "E", transform.name);


            if (Input.GetButtonDown("Use"))
            {
                if (transform.name == "Card")
                {
                    lvl1_objectives._cardCount++;
                    Debug.LogWarning("Card: " + lvl1_objectives._cardCount);
                    Destroy(gameObject);
                    ShowTrigger.SetActive(false);
                    if (lvl1_objectives._cardCount <= 3)
                    {
                        questInfo.text = "Picked up " + lvl1_objectives._cardCount + "/4 cards!";

                    }
                    else
                    {
                        lvl1_objectives.pickedAllCards = true;
                        story.showSuitcase();
                        questInfo.text = "Find guy's suitcase!";
                    }
                }


                if (transform.name == "Case")
                {
                    questInfo.text = "Talk to a guy near the bar";
                    lvl1_objectives.pickedSuitcase = true;
                    ShowTrigger.SetActive(false);
                    story.showLastTrigger();
                    Destroy(gameObject);
                    
                }


            }

        }



        //if (triggerType == global::Triggers.Interact)
        //{
        //    UIController.SetTrigger(1, "E", transform.name);

        //    if (Input.GetButtonDown("Use"))
        //    {
        //        if (transform.name == "Doors")
        //        {
        //            if (lvl1_objectives.canOpenDoorsFake && lvl1_objectives.canOpenDoors == false)
        //            {
        //                questInfo.text = "Pickup your suitcase";
        //            }
        //            else if (lvl1_objectives.canOpenDoors)
        //            {
        //                questInfo.text = "GGs";
        //                animator.SetTrigger("endScreenTrigger");
        //                StartCoroutine(creditsScreen());
        //            }
        //        }
        //    }
        //}
        IEnumerator creditsScreen()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(4);
        }
        if (triggerType == global::Triggers.Trigger)
        {
            if(transform.name == "BarFirstTime")
            {
                dialogues dialog = GameObject.Find("BarFirstTimeDialogue").GetComponent<dialogues>();
                if (!lvl1_objectives.barFirstTime)
                {
                    lvl1_objectives.barFirstTime = true;
                    StartCoroutine(dialog.Type());
                    
                }
                
                if (dialog.isFinished)
                {
                    questInfo.text = "Kill all enemies";
                    lvl1_enemySpawn enemySpawn = GetComponent<lvl1_enemySpawn>();
                    enemySpawn.SpawnEnemy();
                    Destroy(gameObject);
                }
            }
            if(transform.name == "TalkToGuy")
            {
                dialogues dialog = GameObject.Find("TalkToGuyDialogue").GetComponent<dialogues>();
                if (!lvl1_objectives.talkedToGuy)
                {
                    lvl1_objectives.talkedToGuy = true;
                    StartCoroutine(dialog.Type());

                }

                if (dialog.isFinished)
                {
                    questInfo.text = "Find 4 cards";
                    Destroy(gameObject);
                }
            }
            if(transform.name == "BarSecondTime")
            {
                //dialogues dialog = GameObject.Find("BarSecondTimeDialogue").GetComponent<dialogues>();
                //if (!lvl1_objectives.barSecondTime)
                //{
                //    lvl1_objectives.barSecondTime = true;
                //    StartCoroutine(dialog.Type());

                //}

                //if (dialog.isFinished)
                //{
                    animator.SetTrigger("endScreenTrigger");
               // character.canMove = false;
                    StartCoroutine(creditsScreen());
                    
                //}
            }

            if (transform.name == "EnemySpawn_1")
            {
                UIController.SetTrigger(2, "", "");
                lvl1_enemySpawn enemySpawn = GetComponent<lvl1_enemySpawn>();
                enemySpawn.SpawnEnemy();
                Destroy(gameObject);
            }

            if (transform.name == "EnemySpawn_2")
            {
                UIController.SetTrigger(2, "", "");
                lvl1_enemySpawn enemySpawn = GetComponent<lvl1_enemySpawn>();
                enemySpawn.SpawnEnemy();
                Destroy(gameObject);
            }
        }
    }
    private void cardCounter()
    {
        lvl1_objectives._cardCount++;
    }
}

public enum Triggers
{
    Interact,
    PickUp,
    Trigger
}