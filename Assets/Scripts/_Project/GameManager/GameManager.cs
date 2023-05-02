using UnityEngine;

namespace _Project.GameManager
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;
	
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
		
	}
}
