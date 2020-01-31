using Boo.Lang;
using UnityEngine;

namespace Enemies
{
    public class EnemiesGeneralBehaviour : MonoBehaviour
    {
        #region Objects

        public GameObject player;
        public FollowPath myPath;
        public GameObject moleculeCluster;
        public List<Collider2D> colliders;
        public SpriteRenderer spriteRenderer;

        #endregion

        #region Settings Parameters
        
        public float damage;
        private float speed;
        public float defaultSpeed = 2;
        public float attackingSpeed = 8;
        public float distance;
        private const float MoleculeSpeed = 8f;

        #endregion

        #region Boolean Values

        public bool spottedPlayer;
        public bool facingRight;
        public bool disabledColliderAndSprite;

        #endregion

        #region Default Methods

        protected void Start()
        {
            myPath = GetComponent<FollowPath>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            foreach (Collider2D col in GetComponents<Collider2D>())
            {
                colliders.Add(col);
            }
            speed = defaultSpeed;
        }

        protected virtual void FixedUpdate()
        {
            if (spottedPlayer)
            {
                myPath.enabled = false;
                MoveTowardsPlayer();
            }
            else
            {
                myPath.enabled = true;
            }
        }

        #endregion

        #region Custom Methods

        protected void MoveTowardsPlayer()
        {
            if (Mathf.Abs(new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y).magnitude) < distance)
            {
                Attack();
            }
            else
            {
                speed = defaultSpeed;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 2 * Time.deltaTime);
                if (transform.position.x > player.transform.position.x)
                {
                    if (!facingRight)
                        Flip();
                }
                else
                {
                    if (facingRight)
                        Flip();
                }
            }
        }

        protected virtual void Attack()
        {
            speed = attackingSpeed;
        }
    
        public void Flip()
        {
            facingRight = !facingRight;
            Transform transform1 = transform;
            Vector3 theScale = transform1.localScale;
            theScale.x *= -1;
            transform1.localScale = theScale;
        }

        public void Decompose(Transform bulletPosition)
        {
            if (!disabledColliderAndSprite)
            {
                DisableSpriteAndColliders();
            }
            foreach (Transform molecule in moleculeCluster.GetComponentsInChildren<Transform>())
            {
                molecule.position = Vector2.MoveTowards(molecule.position, bulletPosition.position,
                    MoleculeSpeed * Time.deltaTime);
            }
        }

        private void DisableSpriteAndColliders()
        {
            spriteRenderer.enabled = false;
            if (colliders != null)
            {
                foreach (Collider2D col in colliders)
                {
                    col.enabled = false;
                }  
            }
            foreach (Transform molecule in moleculeCluster.GetComponentInChildren<Transform>())
            {
                molecule.gameObject.SetActive(true);
            }
            disabledColliderAndSprite = true;
        }

        #endregion
    
    }
}
