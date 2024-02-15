using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFadeController : MonoBehaviour
{
    private FadeUI _fadeUI;

    [SerializeField] private float fadeTime;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _fadeUI = GetComponent<FadeUI>();
        _fadeUI.FadeUIOut(fadeTime);
    }

    public void CallFadeAndStartNewGame(string sceneToLoad)
    {
        StartCoroutine(FadeAndStartNewGame(sceneToLoad));
    }

    IEnumerator FadeAndStartNewGame(string sceneToLoad)
    {
        _fadeUI.FadeUIIn(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
