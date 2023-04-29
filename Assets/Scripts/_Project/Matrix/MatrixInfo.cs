using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class MatrixInfo
{
   public MatrixInfo(int row, int column,int value)
   {
      this._row = row;
      this._column = column;
      this._value = value;
   }
   [SerializeField]
   private int _row; 
   [SerializeField]
   private int _column;
   [SerializeField]
   private int _value;

   public int Row
   {
      get
      {
         return _row;
      }
   }

   public int Column
   {
      get
      {
         return _column;
      }
   }

   public int Value
   {
      get
      {
         return _value;
      }
      set
      {
         _value = value;
      }
   }

}
