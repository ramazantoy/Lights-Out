using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
	public static SceneManager Instance;
	
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
	private bool IsSceneLoaded(string sceneName) // Zaten yüklü olan sahnenin tekrar yüklenmemesi için kontrol
	{
		for(int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
		{
			if(UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name == sceneName)
			{
				return true;
			}
		}
		return false;
	}
	public void LoadScene(string sceneName) // Aktif olan sahne üzerine yeni sahne yüklenmesi
	{
		if(IsSceneLoaded(sceneName))
		{
			return;
		}
			
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
	}
	public void UnloadScene(string sceneName) //istenilen sahnenin silinmesi
	{
		if(!IsSceneLoaded(sceneName))
		{
			return;
		}
		UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
	}

	public void RestartGame() // Lights out oyununda level geçişi veya oyun yeniden başlatılmak istenilirse
	{
		UnloadScene("LightsOutGame");
		UnityEngine.SceneManagement.SceneManager.LoadScene("LightsOutGame", LoadSceneMode.Additive);
	}
}
