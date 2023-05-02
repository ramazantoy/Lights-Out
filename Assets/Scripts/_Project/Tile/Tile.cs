using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Tile
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private TileData _properties;

        private void Awake()
        {
            if (_properties.TileState == TileState.LightOn)
            {
                _properties.MeshRenderer.material.color = _properties.LightOnColor;
            }
            else if(_properties.TileState==TileState.LightOff)
            {
                _properties.MeshRenderer.material.color = _properties.LightOffColor;
            }
            else
            {
                _properties.MeshRenderer.material.color=_properties.EmptyColor;
            }
        }
        

        public void SwitchTileState(bool callByNeighbour=false)
        {
            if (_properties.TileState == TileState.Empty)
            {
          
                return;
            }

            _properties.TileState = _properties.TileState == TileState.LightOn ? TileState.LightOff : TileState.LightOn;

            SetTileColor();
            
            if (!callByNeighbour)
            {
                TileManager.Instance.TileDown(_properties.MatrixInfo);
            }
   
        }

        private void SetTileColor()
        {
            _properties.MeshRenderer.material.DOColor(_properties.TileState == TileState.LightOn ? _properties.LightOnColor : _properties.LightOffColor, _properties.ColorOnOffTime);
        }

        public void SetMatrixInfo(int row,int column,int value)
        {
            _properties.TileState = (TileState)value;
            _properties.MatrixInfo = new MatrixInfo(row, column, value);
        }

        public int TileValue
        {
            get
            {
                return (int)_properties.TileState;
            }
        }
    }
}
