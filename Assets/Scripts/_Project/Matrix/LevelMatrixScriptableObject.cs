using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Matrix
{
    [CreateAssetMenu(fileName = "LevelMatrix", menuName = "ScriptableObjects/LevelMatrix")]
    [System.Serializable]
    public class LevelMatrixScriptableObject : ScriptableObject
    {
        public List<int> SavedMatrix;
        public int RowCount, ColCount;
        public int[,] Matrix;
        


        [Obsolete("Obsolete")]
        public void SaveMatrix()
        {
            SavedMatrix = TwoDArrayToList(Matrix);
            SetDirty();
        }

        public void LoadMatrix()
        {
            Matrix = ListTo2DMatrix(SavedMatrix);
        }
        private List<int> TwoDArrayToList(int[,] arr)
        {
            int rows = RowCount;
            int cols = ColCount;
            List<int> result = new List<int>(rows * cols);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result.Add(arr[i, j]);
                }
            }
            return result;
        }
        
        private MatrixInfo[,] ListTo2DMatrixInfoArray(List<int> list)
        {
            MatrixInfo[,] result = new MatrixInfo[RowCount, ColCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j <ColCount; j++)
                {
                    int index = i * ColCount + j;
                    result[i, j] = new MatrixInfo(i, j, list[index]);
                }
            }
            return result;
        }

        private int[,] ListTo2DMatrix(List<int> list)
        {
            int[,] result = new int[RowCount, ColCount];
            if (list == null)
            {
                list = new List<int>();
            }
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j <ColCount; j++)
                {
                    int index = i * ColCount + j;
                    if (index >= list.Count)
                    {
                        result[i, j] = 0;
                    }
                    else
                    {
                        result[i, j] = list[index];
                    }
                    
                }
            }
            return result;
        }
     



    }
}
