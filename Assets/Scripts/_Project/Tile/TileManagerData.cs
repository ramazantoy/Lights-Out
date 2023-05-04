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
      public bool OverrideRandomLevel;
      public List<LevelMatrixScriptableObject> Levels;
      [Header("** Other Objects **")]
      public TileBuilder TileBuilder;
      [HideInInspector]
      public MatrixHandler MatrixHandler;
   }
}
