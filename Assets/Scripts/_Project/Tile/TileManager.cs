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

		private List<Tile> _tiles;
		
		public MatrixInfo[,] MatrixInfo
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

		public MatrixHandler MatrixHandler
		{
			get
			{
				return _properties.MatrixHandler;
			}
		}

		private void SaveCurrentMatrix()
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
			BuildTiles();
		}

		public void BuildTiles()
		{
			if (SaveManager.Instance.GameSaveState.HaveSaveGame)
			{

				int row = SaveManager.Instance.GameSaveState.Row;
				int col = SaveManager.Instance.GameSaveState.Col;
				List<int> matrixValues = SaveManager.Instance.GameSaveState.MatrixValues;
				
				MatrixInfo [,]  matrixInfos= ListTo2DMatrixInfoArray(row, col, matrixValues);

				_tiles=_properties.TileBuilder.BuildSavedTiles(matrixInfos);
			}
			else
			{
				if (_properties.OverrideRandomLevel)
				{
					_tiles=_properties.TileBuilder.BuildRandomTiles();
					return;
				}
				if (SaveManager.Instance.GameSaveState.LastLevel < _properties.Levels.Count)
				{
					_tiles = _properties.TileBuilder.BuildSavedTiles(_properties
						.Levels[SaveManager.Instance.GameSaveState.LastLevel].ListTo2DMatrixInfoArray());
				}
				else
				{
					_tiles = _properties.TileBuilder.BuildRandomTiles();
				}
		
			}

		}

		public void TileDown(MatrixInfo matrixInfo)
		{
			List<MatrixInfo> matrixNeighbours = _properties.MatrixHandler.GetMyNeighbour(matrixInfo);
			
			foreach (MatrixInfo matrixNeighbour in matrixNeighbours)
			{ 
				_tiles[_properties.MatrixHandler.GetIndex(matrixNeighbour.Row,matrixNeighbour.Column)].SwitchTileState(true);
			}
			
			CheckMatrix();
			SaveCurrentMatrix();
		}
		
		private MatrixInfo[,] ListTo2DMatrixInfoArray(int row, int col, List<int> list)
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
		
		private List<int> TwoDArrayToList(MatrixInfo[,] arr)
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
		public  int[,] ListTo2DArray(int row, int col, List<int> list)
		{

			int[,] result = new int[row, col];
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					int index = i * col + j;
					result[i, j] = list[index];
				}
			}
			return result;
		}

		private void CheckMatrix()
		{
			if (GameManager.GameManager.Instance.GameState == GameState.Playing && _properties.MatrixHandler.CheckMatrix())
			{
				GameManager.GameManager.Instance.UpdateGameState(GameState.End);
			}
		}


	}
}
