using UnityEngine;


public class MenuPlayer : MonoBehaviour
{
    public LevelManager levelManager;

	void Start()
    {
        levelManager = new LevelManager();
	}

	public void Play()
    {
		StartCoroutine(levelManager.LoadAsync(0));
	}
	public void Load(){
	}
	public void Exit(){
		
	}
}
