using System;
using UnityEngine;

namespace World.Prologue.Background.Fog
{
    public class YAxisMovementRelateToCamera : MonoBehaviour
    {
        public float positionY;
        private void Start()
        {
            positionY = transform.position.y;
        }

        private void Update()
        {
            transform.position = new Vector2(transform.position.x, positionY);
        }
    }
}
