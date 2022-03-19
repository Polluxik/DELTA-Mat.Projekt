using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    
    public GameObject loadingScreen;
    public Text progressText;
    public Slider slider;

    private void Start()
    {
    
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
        SaveSystemManager saveSystem = FindObjectOfType<SaveSystemManager>();
        if(saveSystem != null) saveSystem.SavePlayer();
    }



    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        
            loadingScreen.SetActive(true);
            while (!operation.isDone)
            {
                //Debug.LogWarning(operation.progress);
                float progress=Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;
                progressText.text = progress * 100f + "%";
                yield return null;
            }
            if(operation.isDone && loadingScreen != null) loadingScreen.SetActive(false);
    }
}
