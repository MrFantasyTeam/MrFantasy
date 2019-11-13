using UnityEngine;

namespace Enemies.Prologue.Buffalo
{
    public class BuffaloBehaviour : EnemiesGeneralBehaviour
    {
        private float distanceToPlayer;
        private void Start()
        {
            speed = 4;
        }

        private void Attack()
        {
            speed *= 2;
            transform.position = Vector2.MoveTowards(transform.position.x, )
        }
    }
}