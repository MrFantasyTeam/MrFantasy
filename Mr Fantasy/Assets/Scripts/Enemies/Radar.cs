using System;
using UnityEngine;

namespace Enemies
{
    public class Radar : MonoBehaviour
    {
        private EnemiesGeneralBehaviour parent;
        private Vector3 playerPosition;
        public float distance;
        private void Start()
        {
            parent = GetComponentInParent<EnemiesGeneralBehaviour>();
        }

        private void FixedUpdate()
        {
            playerPosition = parent.player.transform.position;
            parent.spottedPlayer = Mathf.Abs((transform.position - playerPosition).sqrMagnitude) <= distance * distance;
        }
    }
}
