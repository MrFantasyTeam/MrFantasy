using UnityEngine;

namespace World.Lv1.Miscellaneous
{
    public class EnemiesActiverPrologo : MonoBehaviour
    {
        private GameObject player;
        public float playerPositionX;
        public float boxColliderXSize;
        private const string PLayerTag = "Player";
        private const string EnemyTag = "Enemy";
        private float offset = 50f;
        public bool playerReachedCollider;

        private void Start()
        {
            player = GameObject.FindWithTag(PLayerTag);
            boxColliderXSize = GetComponent<BoxCollider2D>().size.x / 2;
            playerPositionX = player.transform.position.x;
            if (Mathf.Abs(playerPositionX - transform.position.x) < boxColliderXSize + offset) 
                playerReachedCollider = true;
        }

        private void LateUpdate()
        {
            playerPositionX = player.transform.position.x;
            if (playerReachedCollider && Mathf.Abs(playerPositionX - transform.position.x) > boxColliderXSize + offset)
                gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(PLayerTag))
                playerReachedCollider = true;
            if (!other.gameObject.CompareTag(EnemyTag)) return;
            other.transform.SetParent(gameObject.transform);
        }
    }
}
