using System.Collections;
using UnityEngine;

namespace Enemies.Grabber
{
    public class GrabberBehaviour : EnemiesGeneralBehaviour
    {
        #region Objects

        public Animator[] modulesAnim;
        private AnimatorStateInfo animatorStateInfo;

        #endregion
        #region Settings Parameter

        public float yPositionSteady;
        public float attackInterval;
        private const float MaxVerticalDistance = 10f; 
        private int animCounter;

        #endregion

        #region Boolean Values

        private bool setSteadyPos;
        public bool attached;
        private bool triggeredAnim;

        #endregion

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (transform.parent != player.transform) return;
            Vector3 tempPosition = transform.position;
            if (!setSteadyPos)
            {
                yPositionSteady = tempPosition.y;
                setSteadyPos = true;
            }
            if (Mathf.Abs(playerPosition.y - yPositionSteady) < MaxVerticalDistance)
                tempPosition = new Vector3(tempPosition.x, yPositionSteady, tempPosition.z);
            else setSteadyPos = false;
            transform.position = tempPosition;
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }

        protected override void Attack()
        {
            if (caught) return;
            speed = 0;
            if (!attached) return;
            if (DamagedPlayer) return;
            
            if (!triggeredAnim)
            {
                triggeredAnim = true;
                modulesAnim[animCounter].CrossFade(AttackAnim, 0);
            }
            
            animatorStateInfo = modulesAnim[animCounter].GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.IsName("Attack") && animatorStateInfo.normalizedTime > .2f)
            {
                animCounter++;
                triggeredAnim = false;
            }

            if (animCounter != modulesAnim.Length) return;
            DamagePlayer();
            animCounter = 0;
        }

        private void DamagePlayer()
        {
            DamagedPlayer = true;
            gondolaMovement.barrierHealth += Mathf.RoundToInt(-damage);
            if (gameObject.layer == TransparentFXLayer) return;
            gondolaMovement.PlayerTakeDamage(Mathf.RoundToInt(-damage), true);
            StartCoroutine(WaitForAttack(attackInterval));
        }
        
        private IEnumerator WaitForAttack(float time)
        {
            yield return new WaitForSeconds(time);
            speed = attackingSpeed;
            DamagedPlayer = false;
        }
    }
}
