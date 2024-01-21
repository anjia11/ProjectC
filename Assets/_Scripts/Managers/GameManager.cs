using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{

    public static GameManager instance;
    [SerializeField] private CheckpointController[] _checkpoints;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _checkpoints = FindObjectsOfType<CheckpointController>();
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadData(GameData data)
    {
        foreach (var pair in data.checkPoints)
        {
            foreach (var checkpoint in _checkpoints)
            {
                if (checkpoint.checkPointId == pair.Key && checkpoint.activated == pair.Value)
                {
                    //checkpoint.ActivateCheckPoint();
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.checkPoints.Clear();
        foreach (var checkpoint in _checkpoints)
        {
            data.checkPoints.Add(checkpoint.checkPointId, checkpoint.activated);
        }
    }
}
