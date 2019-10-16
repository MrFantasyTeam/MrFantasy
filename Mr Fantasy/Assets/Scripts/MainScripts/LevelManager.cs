using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public IEnumerator LoadAsync(int levelNumber)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelNumber);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void LoadImmediate(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
}
