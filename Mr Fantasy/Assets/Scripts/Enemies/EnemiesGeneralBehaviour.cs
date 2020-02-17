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
        private Transform psTransform;
        private Collider2D physicCollider2D;

        #endregion

        #region Settings Parameters
        
        public float damage;
        public float health;
        public float tempHealth;
        public float speed;
        public float defaultSpeed = 2;
        public float attackingSpeed = 8;
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

        #endregion

        #region Default Methods

        private void Awake()
        {
            player = GameObject.FindWithTag(PlayerTag);
            gondolaMovement = player.GetComponent<GondolaMovement>();
            foreach (Collider2D enemyCollider2D in GetComponents<Collider2D>())
            {
                if (enemyCollider2D.isTrigger) continue;
                physicCollider2D = enemyCollider2D;
                break;
            }
            
        }

        protected void Start()
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
                myPath.enabled = false;
                MoveTowardsPlayer();
            }
            else
            {
                myPath.enabled = true;
            }
        }

        private void LateUpdate()
        {
            if (moveParticle) CreateParticleAndMove(bulletPos);
        }

        #endregion

        #region Custom Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerCollider(true);
//            if (other.gameObject.CompareTag(PlayerTag))
//            {
//                if (!physicCollider2D.IsTouching(other.GetComponents<Collider2D>()[0])) return;
//                TriggerCollider(true);
//            }
//            else
//            {
//                TriggerCollider(false);
//            }
        }

        protected virtual void MoveTowardsPlayer()
        {
            if (Mathf.Abs(new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y).magnitude) < distance)
            {
                Attack();
            }
            else
            {
                speed = defaultSpeed;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 2 * Time.deltaTime);
                if (transform.position.x > player.transform.position.x)
                {
                    if (!facingRight) Flip();
                }
                else
                {
                    if (facingRight) Flip();
                }
            } 
        }

        protected virtual void Attack()
        {
            if (caught) return;
            speed = attackingSpeed;
            gondolaMovement.ChangeTransparency(-damage / 100);
            anim.SetTrigger(AttackAnim);
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

        private void ReduceSize()
        {
            StartCoroutine(WaitForReductionTime(.2f));
            Vector3 enemyLocalScale = transform.localScale;
            enemyLocalScale = new Vector3(enemyLocalScale.x * LossPercentage, enemyLocalScale.y  * LossPercentage, 
                enemyLocalScale.z);
            gameObject.transform.localScale = enemyLocalScale;
            tempHealth = health;
        }
        
        private IEnumerator WaitForMovingTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            moveParticle = false;
        }
        
        private IEnumerator WaitForReductionTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime); 
            Vector3 tempLocalScale = psTransform.localScale;
            tempLocalScale = new Vector3(tempLocalScale.x / LossPercentage, tempLocalScale.y / LossPercentage,
                tempLocalScale.z );
            psTransform.localScale = tempLocalScale;
        }

        private void TriggerCollider(bool activate)
        {
            if (physicCollider2D == null) return;
            physicCollider2D.isTrigger = activate;
        }
        
        #endregion
    
    }
}
