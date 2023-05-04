using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class GameSaveState //Save manager tarafından kullanılan kayıt etme sınıfı
{
    [UnityEngine.SerializeField] private int _lastLevel; // Oynanılan son level

    [UnityEngine.SerializeField] private int _rowCount, _colCount; //En son oynanılan satır ve sütün sayısı tile'ların durumunu liste olarak kayıt ettiğim için gerekliler

    [SerializeField] private bool _haveSaveGame; // oyun daha önce oynandıysa kayıtlı tahtanın yüklenmesi için


    [SerializeField] private List<int> _matrixValues; // kayıtlı oyun tahtasındaki anlık matris elemanlarının değeri

    public void SetStartValues() // Varsılan kayıtlı değişkenker
    {
        string saveString = PlayerPrefs.GetString("gameSave");
//        Debug.Log(saveString);
        if (saveString == "")
        {
            _haveSaveGame = false;
            _lastLevel = 0;
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

    private void Save() // bu sınıfın json formatına dönüştürülüp kayıt edilmesi
    {
        //Debug.Log(SaveToString());
        PlayerPrefs.SetString("gameSave", SaveToString());
    }

    public void LoadFromString(string jsonString) // kaydın json yardımıyla bu sınıfa yüklenmesi
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    public void Load() //yükleme işlemi
    {
        string jsonString = PlayerPrefs.GetString("gameSave", "");
        LoadFromString(jsonString);
    }

    private string SaveToString() //sınıf bilgilerinin json olarak döndürülmesi
    {
        return JsonUtility.ToJson(this);
    }
    
    //Değişkenlere ait get set fonksiyonları

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