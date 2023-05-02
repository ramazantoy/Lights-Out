using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class GameSaveState
{
    [UnityEngine.SerializeField] private int _lastLevel;

    [UnityEngine.SerializeField] private int _rowCount, _colCount;

    [SerializeField] private bool _haveSaveGame;


    [SerializeField] private List<int> _matrixValues;

    public void SetStartValues()
    {
        string saveString = PlayerPrefs.GetString("gameSave");
//        Debug.Log(saveString);
        if (saveString == "")
        {
            _haveSaveGame = false;
            _lastLevel = 1;
            _matrixValues = new List<int>();
            _rowCount = 0;
            _colCount = 0;
            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _colCount; j++)
                {
                    _matrixValues.Add(0);
                }
            }

            ;
            Save();
        }
        else
        {
            Load();
        }
    }

    private void Save()
    {
        //Debug.Log(SaveToString());
        PlayerPrefs.SetString("gameSave", SaveToString());
    }

    public void LoadFromString(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    public void Load()
    {
        string jsonString = PlayerPrefs.GetString("gameSave", "");
        LoadFromString(jsonString);
    }

    private string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public bool HaveSaveGame
    {
        set
        {
            _haveSaveGame = value;
            Save();
        }
        get { return _haveSaveGame; }
    }

    public int Row
    {
        set
        {
            _rowCount = value;
            Save();
        }
        get { return _rowCount; }
    }

    public List<int> MatrixValues
    {
        set
        {
            _matrixValues = value;
            Save();
        }
        get
        {
            return _matrixValues;
        }
    }

    public int LastLevel
    {
        set
        {
            _lastLevel = value;
            Save();
        }
        get
        {
            return _lastLevel;
        }
    }
    public int Col
    {
        set
        {
            _colCount = value;
            Save();
        }
        get { return _colCount; }
    }
}