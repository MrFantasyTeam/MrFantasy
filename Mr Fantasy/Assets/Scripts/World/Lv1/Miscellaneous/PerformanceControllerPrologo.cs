using System.Collections;
using UnityEngine;

namespace World.Lv1.Miscellaneous
{
    public class PerformanceControllerPrologo : MonoBehaviour
    {
        public GameObject[] parts;
        private const string LoaderTag = "LoadNext";
        private int activeLoader;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(LoaderTag)) return;
            parts[activeLoader].gameObject.SetActive(true);
            other.gameObject.SetActive(false);
            activeLoader++;
        }
    }
}
