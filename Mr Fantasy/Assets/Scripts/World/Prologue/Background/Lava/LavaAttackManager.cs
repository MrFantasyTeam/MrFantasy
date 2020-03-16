using Player.Gondola;
using UnityEngine;

namespace World.Prologue.Background.Lava
{
   public class LavaAttackManager : MonoBehaviour
   {

      private GondolaMovement playerScript;
      private const string PlayerTag = "Player";
      private const int PlayerLayer = 13;
      private const int BulletLayer = 15;
      private const float damage = -.5f;

      private void Awake()
      {
         playerScript = GameObject.FindWithTag(PlayerTag).GetComponent<GondolaMovement>();
      }

      private void OnParticleCollision(GameObject other)
      {
         if (other.layer != PlayerLayer && other.layer != BulletLayer) return;
         if (playerScript.barrierIsActive && playerScript.barrierHealth + damage > 0) playerScript.barrierHealth += damage;
         else
         {
            playerScript.PlayerTakeDamage(damage, false);
            playerScript.barrierHealth += damage;
         }
      }
   }
}
