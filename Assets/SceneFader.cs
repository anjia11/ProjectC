using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private float fadeTime;

    public Image _fadeOutUIImage;

    public enum FadeType
    {
        In,
        Out
    }
    
    void Awake()
    {
        _fadeOutUIImage = GetComponent<Image>();
    }
    

    public IEnumerator Fade(FadeType fadeType)
    {
        float alpha = fadeType == FadeType.Out ? 1 : 0;
        float fadeEndValue = fadeType == FadeType.Out ? 0 : 1;

        if (fadeType == FadeType.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeType);
                yield return null;
            }

            _fadeOutUIImage.enabled = false;
        }
        else
        {
            _fadeOutUIImage.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeType);
                yield return null;
            }
        }
        
    }

    public IEnumerator FadeAndLoadScene(FadeType fadeType, string sceneToLoad)
    {
        _fadeOutUIImage.enabled = true;
        yield return Fade(fadeType);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void CallFadeAndLoadScene(string sceneToLoad)
    {
        StartCoroutine(FadeAndLoadScene(FadeType.In, sceneToLoad));
    }

    void SetColorImage(ref float alpha, FadeType fadeType)
    {
        _fadeOutUIImage.color =
            new Color(_fadeOutUIImage.color.r, _fadeOutUIImage.color.g, _fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1 / fadeTime) * (fadeType == FadeType.Out ? -1 : 1);
    }
}