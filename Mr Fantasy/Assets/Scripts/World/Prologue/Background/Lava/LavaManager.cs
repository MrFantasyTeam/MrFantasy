using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World.Prologue.Background.Lava
{
    public class LavaManager : MonoBehaviour
    {
        #region Objects

        public GameObject[] bubbles;

        #endregion
        
        #region Settings Variables

        private float timer;
        private float bubbleTime = 2f;

        #endregion

        private void Update()
        {
            timer += Time.deltaTime;
            if (BubblesManager()) timer = 0;
        }

        private bool BubblesManager()
        {
            int index = Random.Range(0, bubbles.Length - 1);
            if (timer > 1.8f)
            {
                bubbles[index].SetActive(true);
            }
            if (!(timer > bubbleTime)) return false;
            int activeBubble = index;
            bubbles[activeBubble].SetActive(false);
            return true;

        }
    }
}
