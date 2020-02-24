using UnityEngine;

/** Script to manage the menu scene. **/
namespace MainScripts
{
	public class MenuPlayer : MonoBehaviour
	{
		public LevelManager levelManager;

		void Start()
		{
			levelManager = gameObject.AddComponent<LevelManager>();
		}

		public void Play()
		{
			StartCoroutine(levelManager.MenuLoadLevelAsync(1));
		}
		public void Load(){
		}
		public void Exit(){
		
		}
	}
}
