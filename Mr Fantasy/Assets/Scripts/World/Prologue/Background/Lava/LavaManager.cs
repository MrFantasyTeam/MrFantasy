using System;
using System.Collections;
using Player.Gondola;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World.Prologue.Background.Lava
{
    public class LavaManager : MonoBehaviour
    {
        #region Objects

        public GameObject[] bubbles;
        private GondolaMovement playerScript;

        #endregion
        
        #region Settings Variables

        private const float Coeff = 0.05f;
        private const string PlayerTag = "Player";
        private const float distance = 10f;
        private const float damage = -20f;
        private float damageMultiplier = 1f;
        private float timer;
        private float bubbleTime = 2f;

        #endregion

        #region Boolean Values

        private bool damagePlayer = true;

        #endregion

        private void Awake()
        {
            playerScript = GameObject.FindWithTag(PlayerTag).GetComponent<GondolaMovement>();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (BubblesManager()) timer = 0;
            float playerDistance = Mathf.Abs(transform.position.y - playerScript.transform.position.y);
            if (playerDistance > distance) return;
            if (playerScript.health <= 0) return;
            if (!damagePlayer) return;
            damagePlayer = false;
            damageMultiplier = distance / playerDistance;
            float finalDamage = damage * damageMultiplier * Coeff;
            if (playerScript.barrierIsActive && playerScript.barrierHealth + finalDamage > 0) playerScript.barrierHealth += finalDamage;
            else
            {
                playerScript.PlayerTakeDamage(finalDamage, false);
                playerScript.barrierHealth += finalDamage;
            }
            StartCoroutine(WaitToDamage(.5f));
        }

        private bool BubblesManager()
        {
            int index = Random.Range(0, bubbles.Length - 1);
            if (timer > 1.8f)
            {
                bubbles[index].SetActive(true);
                int activateLavaAttack = Random.Range(0, 2);
                if (activateLavaAttack != 0)
                {
                    ParticleSystem lavaParticleSystem = bubbles[index].GetComponent<ParticleSystem>();
                    if (lavaParticleSystem)
                        StartCoroutine(ActivateLavaAttack(.3f, lavaParticleSystem));
                }
                    
            }
            if (!(timer > bubbleTime)) return false;
            int activeBubble = index;
            bubbles[activeBubble].SetActive(false);
            return true;
        }
        
        private IEnumerator WaitToDamage(float time)
        {
            yield return new WaitForSeconds(time);
            damagePlayer = true;
        }

        private IEnumerator ActivateLavaAttack(float time, ParticleSystem lavaParticleSystem)
        {
            yield return new WaitForSeconds(time);
            lavaParticleSystem.Play();
        }
    }
}
