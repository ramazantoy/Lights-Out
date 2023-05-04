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

			_gameState = GameState.Null; // oyun ana menude açıldığı için boş state'de başlatılması
		}

		public void UpdateGameState(GameState gameState) // oyunun state'inin değiştirilmesi
		{
			if(_gameState==gameState) return;

			_gameState = gameState;
			if (_gameState == GameState.End) // Oyun bittiyse lights out'un yeni bir maze ile başlatılması
			{
				SaveManager.Instance.GameSaveState.LastLevel++;
				SaveManager.Instance.GameSaveState.HaveSaveGame = false;
				_gameState = GameState.Playing;
				SceneManager.Instance.RestartGame();
			}
		
		}

		private void Update()
		{
			if(_gameState!=GameState.Playing) return;
			if (Input.GetKeyDown(KeyCode.Space)) //Bazen Bfs algoritması geçilemeyen leveller oluşturuyor. Space'e basılırsa oynanılan level'in geçildi sayılması
			{
				UpdateGameState(GameState.End);
			}
		}
	}
}
