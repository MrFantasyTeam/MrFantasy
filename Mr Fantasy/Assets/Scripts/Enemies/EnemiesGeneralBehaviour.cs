using UnityEngine;

namespace Enemies
{
    public class EnemiesGeneralBehaviour : MonoBehaviour
    {
        #region Objects

        public GameObject player;
        public FollowPath myPath;

        #endregion

        #region Settings Parameters

        public float damage;
        public float speed = 2;
        public float distance;

        #endregion

        #region Boolean Values

        public bool spottedPlayer;
        public bool facingRight;

        #endregion

        #region Default Methods

        protected void Start()
        {
            myPath = GetComponent<FollowPath>();
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
            
        }
    
        protected void Flip()
        {
            facingRight = !facingRight;
            Transform transform1 = transform;
            Vector3 theScale = transform1.localScale;
            theScale.x *= -1;
            transform1.localScale = theScale;
        }

        #endregion
    
    }
}
