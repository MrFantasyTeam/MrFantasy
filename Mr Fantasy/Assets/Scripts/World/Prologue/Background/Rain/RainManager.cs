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
        
        #endregion

        #region Settings Properties

        private const float Coeff = 0.002f; 
        private const string PlayerTag = "Player";
        public float defaultDamage;
        public float actualDamage;
        public float playerHeight;

        #endregion

        #region Boolean Values

        public bool entered;
        
        #endregion

        #region Default Methods
        
        private void Update()
        {
            if (entered) DamagePlayer();
        }

        #endregion

        #region Custom Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(PlayerTag))
            {
                player = other.gameObject;
                entered = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(PlayerTag)) entered = false;
        }

        private void DamagePlayer()
        {
            playerHeight = player.transform.position.y;
            actualDamage = defaultDamage * playerHeight * Coeff;
            player.GetComponent<GondolaMovement>().ChangeTransparency(actualDamage);
        }

        #endregion
    }
}