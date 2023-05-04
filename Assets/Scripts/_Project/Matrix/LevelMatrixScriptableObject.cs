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
        //el ile level dizilmek istenilirse dizilen level'in arka planda liste olarak saklanması gerektiğinde matris olarak geri döndürülmesi
        public List<int> SavedMatrix;
        public int RowCount, ColCount;
        public int[,] Matrix;
        


        [Obsolete("Obsolete")]
        public void SaveMatrix() // elle dizilen matrisin kayıt edilmesi editör üzerinde yapılan değişikliklerin korunması
        {
            SavedMatrix = TwoDArrayToList(Matrix);
            SetDirty();
        }

        public void LoadMatrix()// listenin matrise dönüştürülmesi
        {
            Matrix = ListTo2DMatrix(SavedMatrix);
        }
        private List<int> TwoDArrayToList(int[,] arr) // matrisin listeye dönüştürülmesi
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
        

        private int[,] ListTo2DMatrix(List<int> list) // listenin matrise dönüştürülmesi
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
        public MatrixInfo[,] ListTo2DMatrixInfoArray() // int tipinde olan matris'in matrixInfo olarak geri döndürülmesi
        {
            MatrixInfo[,] result = new MatrixInfo[RowCount, ColCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j <ColCount; j++)
                {
                    int index = i * ColCount + j;
                    result[i, j] = new MatrixInfo(i, j, SavedMatrix[index]);
                }
            }
            return result;
        }
     



    }
}
