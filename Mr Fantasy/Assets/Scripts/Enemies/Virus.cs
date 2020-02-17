using System.Collections;
using Player.Gondola;
using UnityEngine;

namespace Enemies
{
    public class Virus : EnemiesGeneralBehaviour
    {
        private bool damaged;
        
        protected override void Attack()
        {
            if (caught) return;
            speed = 0;
            anim.SetTrigger(AttackAnim);
            if (!damaged)
            {
                damaged = true;
                StartCoroutine(WaitForAttack(1.2f));
            }
            
        }

        private IEnumerator WaitForAttack(float time)
        {
            yield return new WaitForSeconds(time);
            player.GetComponent<GondolaMovement>().ChangeTransparency(Mathf.RoundToInt(-damage / 2)); 
            speed = attackingSpeed;
            damaged = false;
        }
    }
}
