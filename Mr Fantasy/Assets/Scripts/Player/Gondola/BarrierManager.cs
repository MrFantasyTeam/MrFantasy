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

        #endregion

        #region Settings Parameters

        private const int EnemyLayer = 14;

        #endregion

        private void Awake()
        {
            gondolaMovement = player.GetComponent<GondolaMovement>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != EnemyLayer) return;
            gondolaMovement.ChangeTransparency(-other.gameObject.GetComponent<EnemiesGeneralBehaviour>().damage);
            if (gondolaMovement.health > 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
