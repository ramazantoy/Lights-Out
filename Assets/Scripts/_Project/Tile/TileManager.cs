using System;
using System.Collections.Generic;
using _Project.GameManager;
using _Project.Matrix;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Tile
{
	public class TileManager : MonoBehaviour
	{
		public static TileManager Instance;

		[SerializeField]
		private TileManagerData _properties;

		private List<Tile> _tiles; // Oyunda bulunan tile'ların listesi
		
		public MatrixInfo[,] MatrixInfo // Tileları rahat yönetmek için tile'ların oluşturduğu matrisin handlera verilmesi
		{
			set
			{
				_properties.MatrixHandler.Matrix= value;
				
				SaveManager.Instance.GameSaveState.Row = _properties.MatrixHandler.Matrix.GetLength(0);
				SaveManager.Instance.GameSaveState.Col = _properties.MatrixHandler.Matrix.GetLength(1);
				SaveManager.Instance.GameSaveState.HaveSaveGame = true;
				SaveManager.Instance.GameSaveState.MatrixValues = TwoDArrayToList(value);
			}
		}

		public MatrixHandler MatrixHandler // Tile'ların state'i değiştiğinde değişikliği handler üzerinde de yapması için
		{
			get
			{
				return _properties.MatrixHandler;
			}
		}

		private void SaveCurrentMatrix() // Oyun sırasında tilerın durumun her bir hamleden sonra liste olarak saklanması
		{
			List<int> tileValues = new List<int>();
			foreach (Tile tile in _tiles)
			{
				tileValues.Add(tile.TileValue);
			}
			
			SaveManager.Instance.GameSaveState.MatrixValues = tileValues;
			


		}
	
		void Awake()
		{
			if(Instance == null)
			{
				Instance = this;
			}
			else if(Instance != this)
			{
				Destroy(gameObject);
			}
		}

		private void Start()
		{
			BuildTiles(); // Oyunun durumuna göre tile tahtasının döşenmesi
		}

		public void BuildTiles()
		{
			if (SaveManager.Instance.GameSaveState.HaveSaveGame) // Oyun daha önce oynandıysa kayıtlı tahtanın yüklenmesi
			{

				int row = SaveManager.Instance.GameSaveState.Row; // kayıtlı row
				int col = SaveManager.Instance.GameSaveState.Col; // kayıtlı col
				List<int> matrixValues = SaveManager.Instance.GameSaveState.MatrixValues; // kayıtlı matris değerleri
				
				MatrixInfo [,]  matrixInfos= ListTo2DMatrixInfoArray(row, col, matrixValues);// listenin row ve col değerine göre 2 boyutlu matris haline getirilmesi

				_tiles=_properties.TileBuilder.BuildSavedTiles(matrixInfos);//iki boyutlu matrisin  oyuna yüklenmesi
			}
			else
			{
				if (_properties.OverrideRandomLevel) // Random level yapılması isteniliyorsa veya elle yapılan leveller bittiyse
				{
					_tiles=_properties.TileBuilder.BuildRandomTiles(); //Bfs algoritması kullanarak geçilebilir levellere oluşturulmaya çalışıılması
					return;
				}
				if (SaveManager.Instance.GameSaveState.LastLevel < _properties.Levels.Count)//elle yapılan leveller bittiyse rastgele leveller oluşturulması
				{
					_tiles = _properties.TileBuilder.BuildSavedTiles(_properties.Levels[SaveManager.Instance.GameSaveState.LastLevel].ListTo2DMatrixInfoArray());
				}
				else
				{
					_tiles = _properties.TileBuilder.BuildRandomTiles();
				}
		
			}

		}

		public void TileDown(MatrixInfo matrixInfo)//Herhangi bir tile'a basıldığında
		{
			List<MatrixInfo> matrixNeighbours = _properties.MatrixHandler.GetMyNeighbour(matrixInfo); // Tile'ın komşularının liste olarak elde edilmesi
			
			foreach (MatrixInfo matrixNeighbour in matrixNeighbours) // Elde edilen komşuların state'lerinin değiştirilmesi
			{ 
				_tiles[_properties.MatrixHandler.GetIndex(matrixNeighbour.Row,matrixNeighbour.Column)].SwitchTileState(true);
			}
			
			CheckMatrix(); //Hamle sonunda level'in bitiş kontrolü
			SaveCurrentMatrix();//Anlık oluşan tile'ın kayıt edilmesi
		}
		
		private MatrixInfo[,] ListTo2DMatrixInfoArray(int row, int col, List<int> list) //Save'den gelen listenin matrise çevirilmesi
		{
			MatrixInfo[,] result = new MatrixInfo[row, col];
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					int index = i * col + j;
					result[i, j] = new MatrixInfo(i, j, list[index]);
				}
			}
			return result;
		}
		
		private List<int> TwoDArrayToList(MatrixInfo[,] arr) // Matrisin listeye çevirilmesi
		{
			int rows = arr.GetLength(0);
			int cols = arr.GetLength(1);
			List<int> result = new List<int>(rows * cols);

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					result.Add(arr[i, j].Value);
				}
			}
			return result;
		}
	

		private void CheckMatrix() // Tahtanın durumuna göre level geçiş kontrolü
		{
			if (GameManager.GameManager.Instance.GameState == GameState.Playing && _properties.MatrixHandler.CheckMatrix())
			{
				GameManager.GameManager.Instance.UpdateGameState(GameState.End);
			}
		}


	}
}
