using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    private GameData _gameData;
    private List<ISaveManager> _saveManagers;
    private DataHandler _dataHandler;
    [SerializeField] private string fileDataName;
    
    
    public static SaveManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        _dataHandler = new DataHandler(Application.persistentDataPath, fileDataName);
        _saveManagers = FindAllSaveManager();
        LoadGame();
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    [ContextMenu("Delete Saved Data")]
    public void DeleteSavedData()
    {
        _dataHandler = new DataHandler(Application.persistentDataPath, fileDataName);
        _dataHandler.DeleteData();
    }

    public void LoadGame()
    {

        _gameData = _dataHandler.Load();
        
        if (this._gameData == null)
        {
            Debug.Log("No Save Data");
            NewGame();
        }

        foreach (var saveManager in _saveManagers)
        {
            saveManager.LoadData(_gameData);
        }
        
        Debug.Log("Actif " + _gameData.checkPoints);
    }

    public void SaveGame()
    {
        
        
        foreach (var saveManager in _saveManagers)
        {
            saveManager.SaveData(ref _gameData);
        }
        Debug.Log("Game currency saved "+_gameData.currency);
        _dataHandler.Save(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManager()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }

    public bool HasSavedData() => _dataHandler.Load() != null;
}
