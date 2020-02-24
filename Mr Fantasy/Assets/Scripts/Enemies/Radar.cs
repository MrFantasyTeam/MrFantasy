using System;
using UnityEngine;

namespace Enemies
{
    public class Radar : MonoBehaviour
    {
        private EnemiesGeneralBehaviour parent;
        public GameObject player;
        private Vector3 playerPosition;
        public float distance;
        private void Start()
        {
            parent = GetComponentInParent<EnemiesGeneralBehaviour>();
        }

        private void FixedUpdate()
        {
            playerPosition = player.gameObject.transform.position;
            if (Mathf.Abs((transform.position - playerPosition).sqrMagnitude) <= distance * distance)
            {
                parent.spottedPlayer = true;
            }
            else
            {
                parent.spottedPlayer = false;
            }
        }
    }
}
