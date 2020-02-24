using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/** Script to load scenes. **/
namespace MainScripts
{
    public class LevelManager : MonoBehaviour
    {
        public bool dead;
        public bool success;
        public IEnumerator LoadLevelAsync(int levelNumber)
        {
            yield return null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelNumber);
            asyncOperation.allowSceneActivation = false;
        
            while (!asyncOperation.isDone)
            {
                
                if (success)
                {
                    asyncOperation.allowSceneActivation = true;
                    Debug.Log("Set allowScene to true");
                }
                else
                {
                    Debug.Log("Not success yet");
                }
                    

                yield return null;
            }
        }
    
        public IEnumerator ReloadLevelAsync(int levelNumber)
        {
            yield return null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelNumber);
            asyncOperation.allowSceneActivation = false;
        
            while (!asyncOperation.isDone)
            {
                if (dead)
                    asyncOperation.allowSceneActivation = true;

                yield return null;
            }
        }

        public IEnumerator MenuLoadLevelAsync(int levelNumber)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelNumber);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}
