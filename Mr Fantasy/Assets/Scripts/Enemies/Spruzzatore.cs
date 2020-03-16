using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Spruzzatore : EnemiesGeneralBehaviour
    {
        public ParticleSystem attackParticleSystem;
        public AnimatorStateInfo animatorStateInfo;
        public bool triggeredAttack;

        protected override void Start()
        {
            base.Start();
            GameObject particle = transform.GetChild(0).gameObject;
            ps = particle.GetComponent<ParticleSystem>();
            psTransform = ps.transform;
            attackParticleSystem = GetComponent<ParticleSystem>();
        }

        protected override void FixedUpdate()
        {
            if (health < tempHealth)
            {
                ReduceSize();
                Debug.Log("Reduction size");
            }
            MoveTowardsPlayer();
        }

        protected override void MoveTowardsPlayer()
        {
            if (caught) return;
            playerPosition = player.transform.position;
            if (Mathf.Abs((transform.position - playerPosition).sqrMagnitude) < distance * distance) Attack();
            LookAtEnemy();
        }

        protected override void Attack()
        {
            if (caught) return;
            speed = 0;
            if (DamagedPlayer) return;
            if (!triggeredAttack)
            {
                triggeredAttack = true;
                anim.CrossFade(AttackAnim, 0f);
            }
            animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (!animatorStateInfo.IsName("Attack")) return;
            if (animatorStateInfo.normalizedTime < .1f) return;
            DamagedPlayer = true;
            attackParticleSystem.Play();
            StartCoroutine(WaitForAttack(2f));
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Player"))
            {
                float finalDamage = -damage / 50;
                if (gondolaMovement.barrierIsActive && gondolaMovement.barrierHealth + finalDamage > 0)
                    gondolaMovement.barrierHealth += finalDamage;
                else
                {
                    gondolaMovement.PlayerTakeDamage(finalDamage, false);
                    gondolaMovement.barrierHealth += finalDamage;
                }
            }
        }

        private IEnumerator WaitForAttack(float time)
        {
            yield return new WaitForSeconds(time);
            DamagedPlayer = false;
            triggeredAttack = false;
            Debug.Log("Can attack again");
        }
    }
}
