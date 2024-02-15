using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneWithTimer : MonoBehaviour
{
    [SerializeField] private float timer;

    [SerializeField] private string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.P))
            {
                timer = 0;
            }
            if (timer <= 0)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}