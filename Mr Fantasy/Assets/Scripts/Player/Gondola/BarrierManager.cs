using System;
using Enemies;
using UnityEngine;

namespace Player.Gondola
{
    public class BarrierManager : MonoBehaviour
    {
        #region Objects

        public GameObject player;
        public GondolaMovement gondolaMovement;
        private ParticleSystem particleSystem;
        private EnemiesGeneralBehaviour enemyScript;

        #endregion

        #region Settings Parameters

        private const int EnemyLayer = 14;
        private const int StaticEnemyLayer = 19;

        #endregion

        private void Awake()
        {
            gondolaMovement = player.GetComponent<GondolaMovement>();
            particleSystem = transform.parent.GetComponentInChildren<ParticleSystem>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != EnemyLayer && other.gameObject.layer != StaticEnemyLayer) return;
            enemyScript = other.GetComponent<EnemiesGeneralBehaviour>();
            enemyScript.caught = true;
            float damage = -enemyScript.damage;
            if (gondolaMovement.barrierHealth + damage >= 0) gondolaMovement.barrierHealth += damage;
            else gondolaMovement.PlayerTakeDamage(gondolaMovement.barrierHealth + damage, false);
            if (gondolaMovement.barrierHealth + damage < 0) gondolaMovement.barrierHealth = 0;
            particleSystem.transform.position = other.transform.position;
            if (gondolaMovement.health > 0) Destroy(other.gameObject);
            particleSystem.Play();
        }
    }
}
