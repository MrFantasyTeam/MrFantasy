using System;
using System.Collections;
using Player.Gondola;
using UnityEngine;

namespace Enemies
{
    public class EnemiesGeneralBehaviour : MonoBehaviour
    {
        #region Objects
        
        public GameObject player;
        public GondolaMovement gondolaMovement;
        public FollowPath myPath;
        public Animator anim;
        public ParticleSystem ps;
        private ParticleSystem.Particle[] particles;
        private Transform bulletPos;
        protected Transform psTransform;
        #endregion

        #region Settings Parameters

        protected const int TransparentFXLayer = 1;
        protected const int EnemyLayer = 14;
        protected const int InteractWithPlayerAndEnemy = 16;
        protected Vector3 playerPosition;
        public float damage;
        public float health;
        public float tempHealth;
        public float speed;
        public float defaultSpeed = 2;
        public float attackingSpeed = 8;
        protected float tempSpeed;
        public float distance;
        private const float MoleculeSpeed = 4f;
        private const float LossPercentage = .8f;
        private int numParticleAlive;
        protected const string PathTagName = "EnemyPath";
        private const string PlayerTag = "Player";
        protected static readonly int AttackAnim = Animator.StringToHash("Attack");

        #endregion

        #region Boolean Values

        public bool spottedPlayer;
        public bool facingRight;
        public bool moveParticle;
        public bool caught;
        protected bool DamagedPlayer;
        protected bool DestroyedPath;

        #endregion

        #region Default Methods

        private void Awake()
        {
            player = GameObject.FindWithTag(PlayerTag);
            gondolaMovement = player.GetComponent<GondolaMovement>();
        }

        protected virtual void Start()
        {
            health = damage;
            tempHealth = health;
            myPath = GetComponent<FollowPath>();
            anim = GetComponent<Animator>();
            ps = GetComponentInChildren<ParticleSystem>();
            psTransform = ps.transform;
            speed = defaultSpeed; 
        }

        protected virtual void FixedUpdate()
        {
            if (health < tempHealth)
            {
                ReduceSize();
            }

            if (spottedPlayer)
            {
                if (!DestroyedPath) myPath.enabled = false;
                MoveTowardsPlayer();
            }
            else
            {
                tempSpeed = 0;
                myPath.enabled = true;
            }
        }

        private void LateUpdate()
        {
            if (moveParticle) CreateParticleAndMove(bulletPos);
        }

        #endregion

        #region Custom Methods

        protected virtual void MoveTowardsPlayer()
        {
            if (caught) return;
            playerPosition = player.transform.position;
            if (Mathf.Abs((transform.position - playerPosition).sqrMagnitude) < distance * distance)
            {
                Attack();
            }
            else
            {
                speed = defaultSpeed;
                tempSpeed += speed / 20 * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, playerPosition, Mathf.Clamp(tempSpeed, 0, speed * Time.deltaTime));
                LookAtEnemy();
            } 
        }

        protected virtual void Attack()
        {
            if (caught) return;
            speed = 0;
            if (DamagedPlayer) return;
            DamagedPlayer = true;
//            anim.SetTrigger(AttackAnim);
            StartCoroutine(WaitForAttack(1.2f));
        }

        protected virtual void LookAtEnemy()
        {
            if (transform.position.x > playerPosition.x)
            {
                if (!facingRight) Flip();
            }
            else
            {
                if (facingRight) Flip();
            }
        }
    
        public void Flip()
        {
            facingRight = !facingRight;
            Transform transform1 = transform;
            Vector3 theScale = transform1.localScale;
            theScale.x *= -1;
            transform1.localScale = theScale;
        }

        public void Decompose(Transform bulletPosition)
        {
            bulletPos = bulletPosition;
            moveParticle = true;
        }

        private void CreateParticleAndMove(Transform bulletPosition)
        {
            if (!ps.gameObject.activeInHierarchy) ps.gameObject.SetActive(true);
            if (ps.isStopped) ps.Play();
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
            numParticleAlive = ps.GetParticles(particles);
            float step = MoleculeSpeed * Time.deltaTime;
            for (int i = 0; i < numParticleAlive; i++)
            {
                particles[i].position = Vector3.SlerpUnclamped(particles[i].position, bulletPosition.position, step);
            }
            ps.SetParticles(particles, numParticleAlive);
            StartCoroutine(WaitForMovingTime(.5f));
        }

        protected void ReduceSize()
        {
            StartCoroutine(WaitForReductionTime(.2f));
            Vector3 enemyLocalScale = transform.localScale;
            enemyLocalScale = new Vector3(enemyLocalScale.x * LossPercentage, enemyLocalScale.y  * LossPercentage, 
                enemyLocalScale.z);
            gameObject.transform.localScale = enemyLocalScale;
            tempHealth = health;
        }
        
        private IEnumerator WaitForAttack(float time)
        {
            yield return new WaitForSeconds(time);
            gondolaMovement.PlayerTakeDamage(Mathf.RoundToInt(-damage), true);
            gondolaMovement.barrierHealth += Mathf.RoundToInt(-damage);
            speed = attackingSpeed;
            DamagedPlayer = false;
        }
        
        private IEnumerator WaitForMovingTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            moveParticle = false;
            ps.gameObject.SetActive(false);
        }
        
        private IEnumerator WaitForReductionTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime); 
            Vector3 tempLocalScale = psTransform.localScale;
            tempLocalScale = new Vector3(tempLocalScale.x / LossPercentage, tempLocalScale.y / LossPercentage,
                tempLocalScale.z );
            psTransform.localScale = tempLocalScale;
        }
        #endregion
    
    }
}
