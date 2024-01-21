using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private string scenename = "MainScene";
    [SerializeField] private GameObject continuebutton;

    private void Start()
    {
        if (!SaveManager.instance.HasSavedData())
        {
            continuebutton.SetActive(false);
        }
    }

    public void ContinueGame()
    {
        //StartCoroutine(nameof(LoadSceneFade), 1f);
        SceneManager.LoadScene(scenename);
    }

    public void NewGame()
    {
        //StartCoroutine(nameof(LoadSceneFade), 1f);
        SaveManager.instance.DeleteSavedData();
        SceneManager.LoadScene(scenename);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
    }

    IEnumerator LoadSceneFade(float delay)
    {
        UIController.instance.StartfadeToBlack();
        yield return new WaitForSeconds(delay);
        UIController.instance.StartFadeFromBlack();
    }
}
