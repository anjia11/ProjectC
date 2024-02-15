using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Behavior_Tree.EnemyBehavior;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{

    public static GameManager Instance;
    public string transitionedFromScene;
    [SerializeField] private GameObject boss;
    public bool isInputEnable = true;
    [SerializeField] private CheckpointController[] _checkpoints;

    [SerializeField] private FadeUI pauseMenu;
    [SerializeField] private FadeUI endMenu;
    [SerializeField] private float fadeTime;
    public bool gamePaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _checkpoints = FindObjectsOfType<CheckpointController>();
        
    }

    private void Update()
    {
        InputPauseGame();
    }

    private void InputPauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused && isInputEnable)
        {
            GamePause(pauseMenu);
        }
    }
    
    public void EndPauseGame()
    {
        if (!gamePaused && isInputEnable)
        {
            GamePause(endMenu);
        }
    }

    public void GamePause(FadeUI fadeUI)
    {
        fadeUI.FadeUIIn(fadeTime);
        Time.timeScale = 0;
        gamePaused = true;
        isInputEnable = false;
        PlayerManager.instance._player.GetComponent<PlayerInput>().actions.FindActionMap("Player").Disable();
    }


    public void UnPauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        PlayerManager.instance._player.GetComponent<PlayerInput>().actions.FindActionMap("Player").Enable();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadData(GameData data)
    {
        // foreach (var pair in data.checkPoints)
        // {
        //     foreach (var checkpoint in _checkpoints)
        //     {
        //         if (checkpoint.checkPointId == pair.Key && checkpoint.activated == pair.Value)
        //         {
        //             //checkpoint.ActivateCheckPoint();
        //         }
        //     }
        // }
    }

    public void SaveData(ref GameData data)
    {
        // data.checkPoints.Clear();
        // foreach (var checkpoint in _checkpoints)
        // {
        //     data.checkPoints.Add(checkpoint.checkPointId, checkpoint.activated);
        // }
    }
}
