using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using World.General.Death;

/** Script to load scenes. **/
namespace MainScripts
{
    public class LevelManager : MonoBehaviour
    {

        #region Setting Parameters

        public float progress = .89f;

        #endregion

        #region Boolean Values

        public bool dead;
        public bool success;

        #endregion
        
        public IEnumerator LoadLevelAsync(int levelNumber)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelNumber);
            asyncOperation.allowSceneActivation = false;
        
            while (!asyncOperation.isDone)
            {
                
                if (success) asyncOperation.allowSceneActivation = true;

                yield return null;
            }
        }
    
        public IEnumerator ReloadLevelAsync(int levelNumber)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelNumber);
            asyncOperation.allowSceneActivation = false;
        
            while (!asyncOperation.isDone)
            {
                progress = asyncOperation.progress;
                if (dead) asyncOperation.allowSceneActivation = true;

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
