using System;
using System.Runtime.InteropServices;
using _Project.Tile;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.GameManager
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;

		private GameState _gameState;

		public GameState GameState
		{
			get
			{
				return _gameState;
			}
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

			_gameState = GameState.Null;
		}

		public void UpdateGameState(GameState gameState)
		{
			if(_gameState==gameState) return;

			_gameState = gameState;
			if (_gameState == GameState.End)
			{
				SaveManager.Instance.GameSaveState.LastLevel++;
				SaveManager.Instance.GameSaveState.HaveSaveGame = false;
				Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
				UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.buildIndex);
				//Load to nextLevel
			}
			else if (_gameState == GameState.Playing)
			{
				TileManager.Instance.BuildTiles();
			}
		}

		private void Update()
		{
			if(_gameState!=GameState.Playing) return;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				UpdateGameState(GameState.End);
			}
		}
	}
}
