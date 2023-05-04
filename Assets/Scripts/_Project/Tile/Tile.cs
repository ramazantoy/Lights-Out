using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Tile
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private TileData _properties; // Tile için gerekli değişkenler

        private void Awake() // Tile'a atanan state'e göre dış görünüşünün ayarlanması
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
        

        public void SwitchTileState(bool callByNeighbour=false) // Tile'ın durumunun değiştirilmesi eğer tıklanarak değiştiyse komşularınında değiştirilmesi
        {
            if (_properties.TileState == TileState.Empty)
            {
          
                return;
            }

            _properties.TileState = _properties.TileState == TileState.LightOn ? TileState.LightOff : TileState.LightOn;

            TileManager.Instance.MatrixHandler.GetMatrixInfo(_properties.MatrixInfo.Row, _properties.MatrixInfo.Column).Value = (int) _properties.TileState;

            SetTileColor();
            
            if (!callByNeighbour)
            {
                TileManager.Instance.TileDown(_properties.MatrixInfo);
            }
            
          
            
            
   
        }

        private void SetTileColor()//Tile'ın sahip olduğu duruma göre renginin ayarlanması
        {
            _properties.MeshRenderer.material.DOColor(_properties.TileState == TileState.LightOn ? _properties.LightOnColor : _properties.LightOffColor, _properties.ColorOnOffTime);
        }

        public void SetMatrixInfo(int row,int column,int value) //Tile'ın sahip olduğu MatrixInfo'nun set edilmesi
        {
            _properties.TileState = (TileState)value;
            _properties.MatrixInfo = new MatrixInfo(row, column, value);
        }

        public int TileValue // Tile'ın üzerinde taşıdığı değerin döndürülmesi -1,0,1
        {
            get
            {
                return (int)_properties.TileState;
            }
        }
    }
}
