using UnityEngine;

namespace World.General.HealthManager
{
    /** Destroy the healthPopUp holding this script */
    public class HealthDestroyer : MonoBehaviour
    {
        public float destroyTime;

        // Update is called once per frame
        void Update()
        {
            Destroy(gameObject, destroyTime);
        }
    }
}