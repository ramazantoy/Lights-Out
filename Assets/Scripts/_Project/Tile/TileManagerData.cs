using System.Collections.Generic;
using _Project.Matrix;
using UnityEngine;

namespace _Project.Tile
{
   [System.Serializable]
   public class TileManagerData
   {
      [Header("If assigned as True, it generates random levels regardless of the manually arranged levels.")]
      [Header(" If it is False, it generates random levels after the manually arranged levels are finished")]
      public bool OverrideRandomLevel; //Elle dizilen leveller oynanmak istenmezse
      public List<LevelMatrixScriptableObject> Levels; // Elle dizilen levellerin listesi
      [Header("** Other Objects **")]
      public TileBuilder TileBuilder; // Tileları oluşturan mono class
      [HideInInspector]
      public MatrixHandler MatrixHandler;// Tile'ların matrsinin arka planda yönetilmesini sağlayan class
   }
}
