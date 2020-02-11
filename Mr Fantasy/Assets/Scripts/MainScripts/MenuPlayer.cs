using UnityEngine;

/** Script to manage the menu scene. **/
public class MenuPlayer : MonoBehaviour
{
    public LevelManager levelManager;

	void Start()
    {
        levelManager = gameObject.AddComponent<LevelManager>();
	}

	public void Play()
    {
		StartCoroutine(levelManager.LoadAsync(1));
	}
	public void Load(){
	}
	public void Exit(){
		
	}
}
