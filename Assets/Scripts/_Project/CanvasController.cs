using System;
using System.Collections;
using _Project.GameManager;
using UnityEngine;

namespace _Project
{
	public class CanvasController : MonoBehaviour
	{
		public static CanvasController Instance;

		private Animator _myAnimator;

		[SerializeField]
		private GameObject _cameraMain;

		private int _currentButtonId;
	
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

			_myAnimator = transform.GetComponent<Animator>();
		}

		public void DummyButtonDown(int buttonId) // Basılan Butona ait sahnenin yüklenmesi
		{
			SetTrigger("MainMenuOut");
			StartCoroutine(LoadLevel(buttonId));
		}

		public void SetTrigger(string trigger)
		{
			_myAnimator.SetTrigger(trigger);
		}

		private IEnumerator LoadLevel(int buttonId) // Biraz bekledikten sonra game play veya dummy sahnelerinin yüklenmesi
		{
			yield return new WaitForSeconds(.35f);
			if (buttonId <= 2)
			{
				_cameraMain.SetActive(false);
				SceneManager.Instance.LoadScene("DummyScene_"+buttonId);
				_myAnimator.SetBool("BackButtonIn",true);
				_currentButtonId = buttonId;
			}
			else
			{
				_myAnimator.SetBool("BackButtonIn",false);
				SceneManager.Instance.LoadScene("LightsOutGame");
				GameManager.GameManager.Instance.UpdateGameState(GameState.Playing);
			}
		}

		public void BackButtonIn() // dummy sahnelerden çıkış yapılması
		{
			SceneManager.Instance.UnloadScene("DummyScene_"+_currentButtonId);
			SetTrigger("BackButtonOut");
			_cameraMain.SetActive(true);
		}
	}
}
