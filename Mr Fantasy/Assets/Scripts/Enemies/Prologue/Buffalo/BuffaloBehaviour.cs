namespace Enemies.Prologue.Buffalo
{
    public class BuffaloBehaviour : EnemiesGeneralBehaviour
    {
        private float distanceToPlayer;

        prote void FixedUpdate()
        {
            if (spottedPlayer)
            {
                myPath.enabled = false;
                MoveTowardsPlayer();
            }
            else
            {
                myPath.enabled = true;
            }
        }

        protected override void Attack()
        {
            
        }
    }
}