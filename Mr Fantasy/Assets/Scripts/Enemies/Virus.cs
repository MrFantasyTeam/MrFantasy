using System.Collections;using UnityEngine;

namespace Enemies
{
    public class Virus : EnemiesGeneralBehaviour
    {
        private AnimatorStateInfo animatorStateInfo;
        private bool setAttackTrigger;

        protected override void Attack()
        {
            if (caught) return;
            if (DamagedPlayer) return;
            if (!setAttackTrigger)
            {
                setAttackTrigger = true;
                speed = 0;
                anim.SetTrigger(AttackAnim);
                StartCoroutine(WaitForAttack(2f));
//                gameObject.layer = InteractWithPlayerAndEnemy;
            }

            
            animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (!animatorStateInfo.IsName("VirusAttack")) return;
            if (animatorStateInfo.normalizedTime < .70f) return;
            gondolaMovement.barrierHealth += Mathf.RoundToInt(-damage);
            if (gameObject.layer == TransparentFXLayer) return;
            gondolaMovement.PlayerTakeDamage(Mathf.RoundToInt(-damage), true);
            DamagedPlayer = true;
        }

        private IEnumerator WaitForAttack(float time)
        {
            yield return new WaitForSeconds(time);
            speed = attackingSpeed;
            setAttackTrigger = false;
            DamagedPlayer = false;
//            gameObject.layer = EnemyLayer;
        }
    }
}
