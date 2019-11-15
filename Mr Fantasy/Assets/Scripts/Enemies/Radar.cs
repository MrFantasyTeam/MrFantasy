using UnityEngine;

namespace Enemies
{
    public class Radar : MonoBehaviour
    {
        private EnemiesGeneralBehaviour parent;
        private void Start()
        {
            parent = GetComponentInParent<EnemiesGeneralBehaviour>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            parent.spottedPlayer = true;
            parent.player = other.gameObject;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            parent.spottedPlayer = false;
            parent.player = null;
        }
    }
}
