using System.Collections;
using Player.Gondola;
using UnityEngine;

namespace Enemies
{
    public class Zanzara : EnemiesGeneralBehaviour
    {
        #region Objects

        private GondolaMovement gondolaMovement;

        #endregion

        #region Settings Parameters
        
        private Transform playerPosition;
        private Vector2 offset;

        #endregion

        #region Boolean values

        private bool attachedToPlayer;
        private bool setOffset;

        #endregion

        #region Custom Methods

        protected override void MoveTowardsPlayer()
        {
            if (Mathf.Abs(new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y).magnitude) < distance)
            {
                Attack();
            }
            else if (!attachedToPlayer)
            {
                speed = defaultSpeed;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 2 * Time.deltaTime);
                if (transform.position.x > player.transform.position.x)
                {
                    if (!facingRight) Flip();
                }
                else
                {
                    if (facingRight) Flip();
                }
            } 
        }
        protected override void Attack()
        {
            if (caught) return;
            speed = attackingSpeed;
            playerPosition = player.transform;
            // TODO transition to animation
            if (!attachedToPlayer) SetEnemyParentToPLayer();
            StartCoroutine(WaitToExplode(.5f));
        }

        #region Secondary Methods

        private void SetEnemyParentToPLayer()
        {
            gondolaMovement = player.GetComponent<GondolaMovement>();
            Transform enemyParent = transform.parent;
            if (enemyParent.gameObject.CompareTag(PathTagName)) Destroy(enemyParent.gameObject);
            gameObject.transform.SetParent(playerPosition);
            anim.enabled = true;
            attachedToPlayer = true;
        }

        private IEnumerator WaitToExplode(float time)
        {
            yield return new WaitForSeconds(time);
            gondolaMovement.ChangeTransparency(-damage);
            Destroy(transform.gameObject);
        }

        #endregion
        #endregion
    }
}
