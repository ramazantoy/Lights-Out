using System;
using System.Collections.Generic;
using _Project.Tile;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    public static TileBuilder Instance;

    [SerializeField] private TileBuilderData _properties;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public List<Tile> BuildRandomTiles()
    {
        SetCameraState(_properties.Row,_properties.Column);
        
        MatrixInfo[,] matrixInfos = new MatrixInfo[_properties.Row, _properties.Column];
        
        Vector3 startPos = new Vector3(0, 0, 0);

        List<Tile> tiles = new List<Tile>();

        for (int i = 0; i < _properties.Row; i++)
        {
            for (int j = 0; j < _properties.Column; j++)
            {
                int rate = UnityEngine.Random.Range(0, 101);
                int value;

                if (rate < _properties.LightOnRate)
                {
                    value = 1; //LightOn
                }

                else
                {
                    rate = UnityEngine.Random.Range(0, 101);
                    if (rate < _properties.EmptyRate)
                    {
                        value = -1; //Empty
                    }
                    else
                    {
                        value = 0; //LightOff
                    }
                }

                matrixInfos[i, j] = new MatrixInfo(i, j, value);

                Vector3 spawnPos = startPos + new Vector3(j * (_properties.Spacing + 1),
                    -i * (_properties.Spacing + 1), 0); // Her bir tile'ın pozisyonu +1'in sebebi tilerın genişliği ve uzunluğu 1 birim

                Tile tileObject = TilePool.Instance.TakeTile();
                tileObject.gameObject.name = "[" + i + "," + j + "] Tile";
                tileObject.transform.position = spawnPos;
                tileObject.transform.parent = transform;
                tileObject.GetComponent<Tile>().SetMatrixInfo(i, j, value);
                tileObject.gameObject.SetActive(true);
                tiles.Add(tileObject);
            }
        }
        TileManager.Instance.MatrixInfo = matrixInfos;
        return tiles;
    }

    public List<Tile> BuildSavedTiles(MatrixInfo[,] matrixInfos)
    {
        Vector3 startPos = new Vector3(0, 0, 0);

        List<Tile> tiles = new List<Tile>();

        int row = matrixInfos.GetLength(0);
        int column = matrixInfos.GetLength(1);
        
        SetCameraState(row,column);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                int rate = UnityEngine.Random.Range(0, 101);
                int value;

                if (rate < _properties.LightOnRate)
                {
                    value = 1; //LightOn
                }

                else
                {
                    rate = UnityEngine.Random.Range(0, 101);
                    if (rate < _properties.EmptyRate)
                    {
                        value = -1; //Empty
                    }
                    else
                    {
                        value = 0; //LightOff
                    }
                }

                matrixInfos[i, j] = new MatrixInfo(i, j, value);

                Vector3 spawnPos = startPos + new Vector3(j * (_properties.Spacing + 1), -i * (_properties.Spacing + 1), 0); // Her bir tile'ın pozisyonu

                Tile tileObject = TilePool.Instance.TakeTile();
                tileObject.gameObject.name = "[" + i + "," + j + "] Tile";
                tileObject.transform.position = spawnPos;
                tileObject.transform.parent = transform;
                tileObject.GetComponent<Tile>().SetMatrixInfo(i, j, value);
                tileObject.gameObject.SetActive(true);
                tiles.Add(tileObject);
            }
        }

        TileManager.Instance.MatrixInfo = matrixInfos;
        return tiles;
    }
    private void SetCameraState(int row, int column)
    {
        float tileWidth = 1f; //Tile genişliği
        float tileHeight = 1f; //Tile Yüksekliği

        float totalWidth = _properties.Column * (tileWidth + _properties.Spacing) - _properties.Spacing + 2 * _properties.Padding; //kenar boşluğu ve tilelar arasındaki boşlukları hesaba katarak toplam genişliğin hesaplanması
        float totalHeight = _properties.Row * (tileHeight + _properties.Spacing) - _properties.Spacing + 2 * _properties.Padding; //kenar boşluğu ve tilelar arasındaki boşlukları hesaba katarak toplam yüksekliğim hesaplanması

        //Elde edilen genişlik ve yüksekliğe göre kameranın ayarlanması
        float cameraSize = Mathf.Max(totalHeight / 2f, totalWidth / 2f / Camera.main.aspect);
        Camera.main.orthographicSize = cameraSize;
        
                
        Vector3 cameraPos = new Vector3(totalWidth / 2f - tileWidth / 2f - _properties.Padding,
            -totalHeight / 2f + tileHeight / 2f + _properties.Padding, -cameraSize);
        Camera.main.transform.position = cameraPos;
        
    }
}