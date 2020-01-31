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

        private const string EnemyTag = "Enemy";

        #endregion

        private void Start()
        {
            gondolaMovement = player.GetComponent<GondolaMovement>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(EnemyTag))
            {
                player.GetComponent<GondolaMovement>().ChangeTransparency(-other.gameObject.GetComponent<EnemiesGeneralBehaviour>().damage);
                Debug.Log("Damage is: " + -other.gameObject.GetComponent<EnemiesGeneralBehaviour>().damage);
                if (player.GetComponent<GondolaMovement>().health > 0)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
