using System;
using UnityEditor;
using UnityEngine;

namespace _Project.Matrix
{
    [CustomEditor(typeof(LevelMatrixScriptableObject))]
    public class LevelMatrixScriptableObjectEditor : Editor
    {
        private LevelMatrixScriptableObject levelMatrix;
        
        //level matrix editör üzerinde seçilirse oluşan maze'in inspector üzerinde gösterilmesi ve kayıt edilmesi
        private int cellSize = 25; // hücre büyüklüğü
        private GUIStyle whiteStyle;//beyaz box
        private GUIStyle blackStyle;//siyah box
        private GUIStyle greenStyle;//yeşil box

        private void OnEnable()
        {
            levelMatrix = target as LevelMatrixScriptableObject; // inspectorda tıklamanın yakalanması
            
        }

        [Obsolete("Obsolete")]
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            //hedel level matrix'in verilerinin gösterilmesi
            levelMatrix.RowCount = EditorGUILayout.IntField("Row Count", levelMatrix.RowCount);
            levelMatrix.ColCount = EditorGUILayout.IntField("Column Count", levelMatrix.ColCount);
            
            levelMatrix.LoadMatrix();

            if (levelMatrix.SavedMatrix == null) // yok ise veya row col değiştiyse tekrar oluşturulup kayıt edilmesi
            {
                if (levelMatrix.Matrix == null || levelMatrix.Matrix.GetLength(0) != levelMatrix.RowCount || levelMatrix.Matrix.GetLength(1) != levelMatrix.ColCount)
                {
                    levelMatrix.Matrix = new int[levelMatrix.RowCount, levelMatrix.ColCount];
                    levelMatrix.SaveMatrix();
                    EditorUtility.SetDirty(levelMatrix);
                }
            }
     
            //bilgilerin hem temsili kutucuklar olarak gösterilmesi 
            //Hemde doldurulabilir slider olarak verilmesi
            for (int i = 0; i < levelMatrix.RowCount; i++)
            {
                for (int j = 0; j < levelMatrix.ColCount; j++)
                {
                    levelMatrix.Matrix[i, j] = EditorGUILayout.IntSlider($"[{i},{j}]", levelMatrix.Matrix[i, j], -1, 1);
                }
            }
            
            whiteStyle = new GUIStyle(GUI.skin.box);
            whiteStyle.normal.background = MakeTex(cellSize, cellSize, Color.white);

            blackStyle = new GUIStyle(GUI.skin.box);
            blackStyle.normal.background = MakeTex(cellSize, cellSize, Color.black);

            greenStyle = new GUIStyle(GUI.skin.box);
            greenStyle.normal.background = MakeTex(cellSize, cellSize, Color.green);

            EditorGUILayout.Space(15);
            
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("** Maze Info **", style);
            
    
            EditorGUILayout.Space(15);
            for (int i = 0; i < levelMatrix.RowCount; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < levelMatrix.ColCount; j++)
                {     if (levelMatrix.Matrix[i, j] == 1)
                    {
                        // 1 ise yeşil
                        EditorGUILayout.LabelField("", greenStyle, GUILayout.Width(cellSize), GUILayout.Height(cellSize));
                    }
                    else if (levelMatrix.Matrix[i, j] == -1)
                    {
                        // -1 ise kırmızı
                        EditorGUILayout.LabelField("", blackStyle, GUILayout.Width(cellSize), GUILayout.Height(cellSize));
                    }
                    else
                    {
                        // 0 ise beyaz
                        EditorGUILayout.LabelField("", whiteStyle, GUILayout.Width(cellSize), GUILayout.Height(cellSize));
                    }

                }
             
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(3);
            }
            //bilgilerin kayıt edilmesi
            levelMatrix.SaveMatrix(); 
            EditorUtility.SetDirty(levelMatrix);
            

        }
        
        //Stil ayarlanması
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
     
    }
}