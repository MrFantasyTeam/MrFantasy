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
        private GameObject enemy;
        private EnemiesGeneralBehaviour enemiesGeneralBehaviour;

        #endregion

        #region Setting Properties

        private static readonly int Catch = Animator.StringToHash("Catch");
        private static readonly int GoBack = Animator.StringToHash("GoBack");
        private static readonly int Default = Animator.StringToHash("Default");
        private const string PlayerTag = "Player";
        private const string DeadEnemyTag = "DeadEnemy";
        private const string MainCameraTag = "MainCamera";
        private const string EnemyTag = "Enemy";
        private const float CatchAnimDuration = .5f;
        private const float DefaultDamage = 5;
        public float speed;
        private float damage;
        private float recharge;
        private float enemyTempSpeed;
        private float enemyTempAttSpeed;

        #endregion

        #region Boolean Values

        private bool caught; // the enemy has been caught
        private bool hit; // the enemy has been hit
        private bool catchAnimTriggered; // the catch anim has started
        private bool damageEnemy = true;
        private bool killed;
        
        #endregion

        #region Default Methods

        private void Awake()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindWithTag(PlayerTag).GetComponent<GondolaMovement>();
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
            if (!other.gameObject.CompareTag(EnemyTag)) return;
            if (hit) return;
            hit = true;
            enemy = other.gameObject;
            enemiesGeneralBehaviour = other.GetComponent<EnemiesGeneralBehaviour>();
            damage = (player.barrierIndex + 1) * DefaultDamage;
            recharge = damage;
            SlowDownEnemy(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.CompareTag(MainCameraTag)) gameObject.SetActive(false);
        }

        private void CatchEnemy()
        {
            if (enemiesGeneralBehaviour == null || enemiesGeneralBehaviour.gameObject.CompareTag(DeadEnemyTag)) return;
            if (!catchAnimTriggered)
            {
                anim.SetTrigger(Catch);
                catchAnimTriggered = true;
            }

            if (damage >= enemiesGeneralBehaviour.health)
            {
                enemiesGeneralBehaviour.gameObject.tag = DeadEnemyTag;
                enemiesGeneralBehaviour.caught = true;
                enemiesGeneralBehaviour.health = 0;
            }
            enemiesGeneralBehaviour.Decompose(transform);
            StartCoroutine(WaitForAnimEnd(CatchAnimDuration));
        }

        private void GoBackToPlayer()
        {
            DamageEnemy();
            anim.SetTrigger(GoBack);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed / 100);
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 0.15f) return;
            player.ChangeTransparency(recharge);
            anim.SetTrigger(Default);
            ResetBool();
            gameObject.SetActive(false);
        }
        
        #region Secondary Methods

        private void DamageEnemy()
        {
            if (enemiesGeneralBehaviour.health <= 0) Destroy(enemy);
            else if (damageEnemy)
            {
                enemiesGeneralBehaviour.health -= damage;
                damageEnemy = false;
            }
        }
        
        private void ResetBool()
        {
            caught = false;
            hit = false;
            catchAnimTriggered = false;
            damageEnemy = true;
        }
        
        private void SlowDownEnemy(bool slow)
        {
            if (slow)
            {
                enemyTempAttSpeed = enemiesGeneralBehaviour.attackingSpeed;
                enemyTempSpeed = enemiesGeneralBehaviour.defaultSpeed;
                enemiesGeneralBehaviour.defaultSpeed = 0.3f;
                enemiesGeneralBehaviour.attackingSpeed = 0.3f;
                return;
            } 
            enemiesGeneralBehaviour.defaultSpeed = enemyTempSpeed;
            enemiesGeneralBehaviour.attackingSpeed = enemyTempAttSpeed;
        } 

        #endregion

        #region Coroutine Methods

        private IEnumerator WaitForAnimEnd(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SlowDownEnemy(false);
            caught = true;
        }

        #endregion
        
        #endregion
        
    }
}
