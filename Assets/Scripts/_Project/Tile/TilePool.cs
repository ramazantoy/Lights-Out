using System.Collections;
using System.Collections.Generic;
using _Project.Tile;
using UnityEngine;
using UnityEngine.Serialization;


public class TilePool : MonoBehaviour
{
    public static TilePool Instance;

    private void Awake()
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
    [SerializeField] private Tile _tilePrefab;
    
    [SerializeField] private List<Tile> _tileList;
    
    public void AddObject(Tile tile)
    {
        if (!_tileList.Contains(tile))
        {
            _tileList.Add(tile);

            tile.transform.parent = transform;
            tile.transform.localPosition = Vector3.zero;

            tile.gameObject.SetActive(false);
        }
    }

    public Tile TakeTile()
    {
        Tile tileTemp;
        if (_tileList.Count > 0)
        {
            tileTemp = _tileList[0];
            _tileList.Remove(tileTemp);
        }
        else
        {
            tileTemp = Instantiate(_tilePrefab);
        }

        //poolableGO.SetActive(true);
        tileTemp.transform.parent = null;
        return tileTemp;
    }
}