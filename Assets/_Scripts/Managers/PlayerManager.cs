using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public Player _player;

    public int currency;

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

    public void LoadData(GameData data)
    {
        currency = data.currency;
    }

    public void SaveData(ref GameData data)
    {
        data.currency = this.currency;
    }
}