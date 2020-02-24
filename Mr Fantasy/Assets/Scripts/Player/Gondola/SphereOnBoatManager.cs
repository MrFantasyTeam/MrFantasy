using System;
using UnityEngine;

namespace Player.Gondola
{
    public class SphereOnBoatManager : MonoBehaviour
    {
        #region Objects

        public GondolaMovement gondolaMovement;
        public GameObject[] spheres;

        #endregion

        #region Setting Parameters

        private float health;

        #endregion

        private void FixedUpdate()
        {
            health = gondolaMovement.health;
            ManageSpheres();
        }

        private void ManageSpheres()
        {
            for (int i = 0; i < spheres.Length; i++)
            {
                spheres[i].SetActive(Mathf.FloorToInt(health / 10) >= i + 1);
            }
        }
    }
}
