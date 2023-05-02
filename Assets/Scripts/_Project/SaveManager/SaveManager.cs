using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
     
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField]
    private GameSaveState _gameSaveState;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        _gameSaveState = new GameSaveState();
        _gameSaveState.SetStartValues();
    }

    public GameSaveState GameSaveState
    {
        get
        {
            return _gameSaveState;
        }
    }
}
