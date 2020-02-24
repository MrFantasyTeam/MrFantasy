using System.Collections;using UnityEngine;

namespace Enemies
{
    public class Virus : EnemiesGeneralBehaviour
    {

        protected override void Attack()
        {
            if (caught) return;
            speed = 0;
            if (DamagedPlayer) return;
            DamagedPlayer = true;
            anim.SetTrigger(AttackAnim);
            StartCoroutine(WaitForAttack(1.2f));

        }

        private IEnumerator WaitForAttack(float time)
        {
            yield return new WaitForSeconds(time);
            gondolaMovement.ChangeTransparency(Mathf.RoundToInt(-damage)); 
            speed = attackingSpeed;
            DamagedPlayer = false;
        }
    }
}
