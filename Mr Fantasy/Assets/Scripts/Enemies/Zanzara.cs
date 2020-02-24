using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Zanzara : EnemiesGeneralBehaviour
    {
        #region Objects

        private AnimatorStateInfo animatorStateInfo;
        public ParticleSystem particles;

        #endregion

        #region Settings Parameters
        
        private static readonly int AttackingTargetAnim = Animator.StringToHash("AttackTarget");
        private static readonly int Explode = Animator.StringToHash("Explode");
        private static readonly int Missed = Animator.StringToHash("Missed");
        private Vector3 tempTarget;
        private Vector2 offset;
        private float distanceToStartAttack;
        private float explosionTime;

        #endregion

        #region Boolean values

        private bool attachedToPlayer;
        private bool moveToPlayer = true;
        public bool setTarget;
        public bool attackingTarget;
        public bool missedTarget = true;
        public bool explode;
        public bool triggeredMissedAnim;
        public bool playedExplosionAnim;
        
        #endregion

        #region Default Methods

        protected override void Start()
        {
            base.Start();
            distanceToStartAttack = distance * 6f;
            particles.Stop();
        }

        #endregion

        #region Custom Methods

        protected override void MoveTowardsPlayer()
        {
            if (caught) return;
            if (explode)
            {
                Explosion();
                return;
            }
            
            if (!attachedToPlayer && moveToPlayer) GoToPlayer();

            if (!setTarget 
                && Mathf.Abs(new Vector2(transform.position.x - playerPosition.x, transform.position.y - playerPosition.y).sqrMagnitude) < distanceToStartAttack * distanceToStartAttack
                && Mathf.Abs(new Vector2(transform.position.x - playerPosition.x, transform.position.y - playerPosition.y).sqrMagnitude) > distance * distance)
            {
                SetTargetToPlayerLastPosition();
            }
            
            if (setTarget 
                && Mathf.Abs(new Vector2(transform.position.x - tempTarget.x, transform.position.y - tempTarget.y).sqrMagnitude) < distanceToStartAttack * distanceToStartAttack
                && Mathf.Abs(new Vector2(transform.position.x - tempTarget.x, transform.position.y - tempTarget.y).sqrMagnitude) > distance * distance)
            {
                AttackTarget();
            }
            else if (Mathf.Abs(new Vector2(transform.position.x - tempTarget.x, transform.position.y - tempTarget.y).sqrMagnitude) <= distance * distance)
            {
                MissedPlayer();
            }
        }
        protected override void Attack()
        { 
            if (caught) return;
            tempSpeed = 0;
            missedTarget = false;
            if (!explode)
            {
                explode = true;
                anim.SetTrigger(Explode);
            }
            if (!attachedToPlayer) SetEnemyParentToPlayer();
        }

        #region Secondary Methods
        
        protected override void LookAtEnemy()
        {
            if (transform.position.x > playerPosition.x)
            {
                if (!facingRight) Flip();
            }
            else
            {
                if (facingRight) Flip();
            }
        }

        private void GoToPlayer()
        {
            playerPosition = player.transform.position;
            speed = defaultSpeed;
            tempSpeed += speed / 15 * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, Mathf.Clamp(tempSpeed, 0, speed * .2f * Time.deltaTime));
            LookAtEnemy();
        }

        private void SetTargetToPlayerLastPosition()
        {
            tempTarget = new Vector3(playerPosition.x, playerPosition.y, playerPosition.x);
            setTarget = true;
            triggeredMissedAnim = false;
            moveToPlayer = false;
        }

        private void AttackTarget()
        {
            if (!attackingTarget)
            {
                attackingTarget = true;
                anim.SetTrigger(AttackingTargetAnim);
            }
            tempSpeed += speed / 15 * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, tempTarget, 
                Mathf.Clamp(tempSpeed, 0, speed * 1.2f * Time.deltaTime));
            if (Mathf.Abs((transform.position - player.transform.position).sqrMagnitude) < distance * distance )
            {
                Attack();
            }
        }

        private void MissedPlayer()
        {
            StartCoroutine(WaitToStartChasing(.3f));
            if (!missedTarget || triggeredMissedAnim) return;
            triggeredMissedAnim = true;
            attackingTarget = false;
            anim.SetTrigger(Missed);
        }

        private void SetEnemyParentToPlayer()
        {
            Transform enemyParent = transform.parent;
            if (enemyParent.gameObject.CompareTag(PathTagName))
            {
                Destroy(enemyParent.gameObject);
                destroyedPath = true;
            }
            gameObject.transform.SetParent(player.transform);
            anim.enabled = true;
            attachedToPlayer = true;
            enabled = true;
        }

        private void Explosion()
        {
            animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (!animatorStateInfo.IsName("Attack - Explode")) return;
            if (animatorStateInfo.normalizedTime < .9f && animatorStateInfo.normalizedTime > .8f && !playedExplosionAnim)
            {
                particles.Play();
                playedExplosionAnim = true;
            }
            if (!(animatorStateInfo.normalizedTime > .95f)) return;
            gondolaMovement.ChangeTransparency(-damage * 2);
            
            
            Destroy(gameObject);
        } 

        private IEnumerator WaitToStartChasing(float time)
        {
            yield return new WaitForSeconds(time);
            moveToPlayer = true;
            setTarget = false;
            tempSpeed = 0;
        }

        #endregion
        #endregion
    }
}
