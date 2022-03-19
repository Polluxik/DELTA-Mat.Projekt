using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogues : MonoBehaviour
{
    public Text textDisplay;
    [TextArea(3, 10)]
    public string[] sentences;

    public static bool isDialogue;
    private int index;
    public GameObject continueButton;
    private P_Controller controller;
    public bool freezePlayer;
    public bool isFinished = false;

    private void Start()
    {
        controller = GameObject.Find("Player").GetComponent<P_Controller>();
    }

    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);

        }
    }

    public IEnumerator Type()
    {
        if(freezePlayer) isDialogue = true;
        foreach (var letter in sentences[index].ToCharArray())
        {

            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            if(freezePlayer) isDialogue = false;

            isFinished = true;
            textDisplay.text = "";
            controller.setCursor(false);
            continueButton.SetActive(false);
            Destroy(gameObject);
        }
    }

}
