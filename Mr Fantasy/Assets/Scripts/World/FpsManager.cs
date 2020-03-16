using System;
using UnityEngine;

namespace World
{
    public class FpsManager : MonoBehaviour
    {
        public int target = 60;
        private int actualFrameRate;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = target;
            actualFrameRate = Application.targetFrameRate;
            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            actualFrameRate = target;
        }
    }
}
