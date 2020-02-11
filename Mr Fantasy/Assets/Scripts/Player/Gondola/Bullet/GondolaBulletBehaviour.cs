using System.Collections;
using Enemies;
using UnityEngine;

namespace Player.Gondola.Bullet
{
    public class GondolaBulletBehaviour : MonoBehaviour
    {
        #region Objects

        private GondolaMovement player;
        private Animator anim;
        public GameObject enemy;
        private EnemiesGeneralBehaviour enemiesGeneralBehaviour;

        #endregion

        #region Setting Properties

        public float speed;
        public float damage;
        public float defaultDamage = 1;
        private float recharge;
        public float lossPercentage = 1.2f;
        private static readonly int Catch = Animator.StringToHash("Catch");
        private static readonly int GoBack = Animator.StringToHash("GoBack");
        private static readonly int Default = Animator.StringToHash("Default");
        private const float CatchAnimDuration = .5f;

        #endregion

        #region Boolean Values

        public bool caught; // the enemy has been caught
        public bool hit; // the enemy has been hit
        public bool catchAnimTriggered; // the catch anim has started
        public bool reduce = true;
        
        #endregion

        #region Default Methods

        private void Awake()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindWithTag("Player").GetComponent<GondolaMovement>();
            damage = (player.barrierIndex + 1) * defaultDamage;
        }

        private void FixedUpdate()
        {
            if (!hit) Moving();
            else
            {
                if (!caught || !catchAnimTriggered) CatchEnemy();
                else if(caught) GoBackToPlayer();
            }
        }

        #endregion

        #region Custom Methods

        private void Moving()
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Equals("Enemy")) return;
            if (hit) return;
            hit = true;
            enemy = other.gameObject;
            enemiesGeneralBehaviour = other.GetComponent<EnemiesGeneralBehaviour>();
            recharge = enemiesGeneralBehaviour.damage;
            if (enemiesGeneralBehaviour.health <= 0) other.gameObject.tag = "DeadEnemy";
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.tag.Equals("MainCamera"))
                gameObject.SetActive(false);
        }

        private void CatchEnemy()
        {
            if (!catchAnimTriggered) anim.SetTrigger(Catch);
            catchAnimTriggered = true;
            if (damage >= recharge) enemiesGeneralBehaviour.health = 0;
            enemiesGeneralBehaviour.Decompose(transform);
            StartCoroutine(WaitForAnimEnd(CatchAnimDuration));
        }

        private void GoBackToPlayer()
        {
            if (damage >= enemiesGeneralBehaviour.health)
            {
                Debug.Log("Destroying enemy");
                Destroy(enemy);
            }
            else
            {
                if (reduce)
                {
                    enemiesGeneralBehaviour.health -= damage;
                    Vector3 enemyLocalScale = enemy.transform.localScale;
                    enemyLocalScale = new Vector3(enemyLocalScale.x * lossPercentage, enemyLocalScale.y  * lossPercentage, enemyLocalScale.z);
                    enemy.transform.localScale = enemyLocalScale;
                    Debug.Log("Reduced enemy scale");
                    reduce = false;
                    WaitForReductionTime(.1f);
                }
                
            }
            anim.SetTrigger(GoBack);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed / 100);
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 0.15f) return;
            player.ChangeTransparency(recharge);
            anim.SetTrigger(Default);
            ResetBool();
            gameObject.SetActive(false);
        }

        private void ResetBool()
        {
            caught = false;
            hit = false;
            catchAnimTriggered = false;
        }
        
        private IEnumerator WaitForAnimEnd(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            caught = true;
        }
        
        private IEnumerator WaitForReductionTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            reduce = true;
        }

        #endregion
        
    }
}
