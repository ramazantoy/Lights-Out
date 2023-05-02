using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Tile
{
    [System.Serializable]
    public class TileBuilderData
    {
        public float Spacing = 0.05f;
        public float Padding = 0.5f;
        [Header("** İf Using Random Level**")]
        public int Row; // Matris boyutu - satır sayısı
        public int Column; // Matris boyutu - sütun sayısı
        [Header("**Tile Spawn Settings**")]
        [Range(0, 100)] 
        public int LightOnRate;
        [Header("**If LightOff occurs, the empty rate is checked**")]
        [Range(0, 100)] 
        public int EmptyRate;
    }
}
