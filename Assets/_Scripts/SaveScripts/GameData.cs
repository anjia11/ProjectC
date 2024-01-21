using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class GameData
{
    public int currency;

    public SerializedDictionary<string, bool> checkPoints;

    public GameData()
    {
        this.currency = 0;
        checkPoints = new SerializedDictionary<string, bool>();
    }
}
