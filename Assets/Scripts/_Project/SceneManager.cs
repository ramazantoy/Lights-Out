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
	private bool IsSceneLoaded(string sceneName)
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
	public void LoadScene(string sceneName)
	{
		if(IsSceneLoaded(sceneName))
		{
			return;
		}
			
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
	}
	public void UnloadScene(string sceneName)
	{
		if(!IsSceneLoaded(sceneName))
		{
			return;
		}
		UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
	}

	public void RestartGame()
	{
		UnloadScene("LightsOutGame");
		UnityEngine.SceneManagement.SceneManager.LoadScene("LightsOutGame", LoadSceneMode.Additive);
	}
}
