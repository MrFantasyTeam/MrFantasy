using System;
using System.Collections;
using Player.Gondola;
using UnityEngine;

namespace World.Prologue.Background.Rain
{
    /** Controls rain behaviour */
    public class RainManager : MonoBehaviour    
    {
        #region Objects

        public GameObject player;
        private GondolaMovement playerScript;
        
        #endregion

        #region Settings Properties

        private const float Coeff = 0.006f; 
        private const string PlayerTag = "Player";
        public float defaultDamage;
        public float actualDamage;
        public float playerHeight;
        private const float Distance = 60;

        #endregion

        #region Boolean Values

        private bool damage = true;
        
        #endregion

        #region Default Methods

        private void Start()
        {
            player = GameObject.FindWithTag(PlayerTag);
            playerScript = player.GetComponent<GondolaMovement>();
        }

        private void FixedUpdate()
        {
            if (transform.position.y - player.transform.position.y <= Distance && damage) DamagePlayer();
        }

        #endregion

        #region Custom Methods

        private void DamagePlayer()
        {
            damage = false;
            if (playerScript.health <= 0) return;
            playerHeight = player.transform.position.y;
            actualDamage = defaultDamage * playerHeight * playerHeight * Coeff;
            if (playerScript.barrierIsActive && playerScript.barrierHealth + actualDamage >= 0) playerScript.barrierHealth += actualDamage;
            else
            {
                playerScript.PlayerTakeDamage(actualDamage, false);
                playerScript.barrierHealth += actualDamage;
            }
            StartCoroutine(WaitToDamage(.5f));
        }

        private IEnumerator WaitToDamage(float time)
        {
            yield return new WaitForSeconds(time);
            damage = true;
        }

        #endregion
    }
}