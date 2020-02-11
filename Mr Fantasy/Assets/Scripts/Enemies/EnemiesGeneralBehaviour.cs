using System.Collections;
using Boo.Lang;
using Player.Gondola;
using UnityEngine;

namespace Enemies
{
    public class EnemiesGeneralBehaviour : MonoBehaviour
    {
        #region Objects

        public GameObject player;
        public FollowPath myPath;
        public GameObject moleculeCluster;
        public List<Collider2D> colliders;
        public SpriteRenderer spriteRenderer;
        public Animator anim;
        public Transform[] moleculeClusterTransforms;

        #endregion

        #region Settings Parameters
        
        public float damage;
        public float health;
        private float speed;
        public float defaultSpeed = 2;
        public float attackingSpeed = 8;
        public float distance;
        private const float MoleculeSpeed = 8f;
        private static readonly int AttackAnim = Animator.StringToHash("Attack");

        #endregion

        #region Boolean Values

        public bool spottedPlayer;
        public bool facingRight;
        public bool disabledColliderAndSprite;
        public bool deactivateMolecule = false;

        #endregion

        #region Default Methods

        protected void Start()
        {
            health = damage;
            myPath = GetComponent<FollowPath>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            foreach (Collider2D col in GetComponents<Collider2D>())
            {
                colliders.Add(col);
            }
            speed = defaultSpeed;
        }

        protected virtual void FixedUpdate()
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

        #endregion

        #region Custom Methods

        protected void MoveTowardsPlayer()
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
                    if (!facingRight)
                        Flip();
                }
                else
                {
                    if (facingRight)
                        Flip();
                }
            } 
        }

        protected virtual void Attack()
        {
            speed = attackingSpeed;
            player.GetComponent<GondolaMovement>().ChangeTransparency(-damage / 100);
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
//            moleculeClusterTransforms = moleculeCluster.GetComponentsInChildren<Transform>();
            if (health <= 0)
            {
                
                if (!disabledColliderAndSprite)
                {
                    DisableSpriteAndColliders();
                }

//                int transformINdex = 0;

//                foreach (Transform molecule in moleculeClusterTransforms)
//                {
//                    transformINdex++;
//                }
//                Debug.Log("Transfrom INdex = " + transformINdex);
                foreach (Transform molecule in moleculeClusterTransforms)
                {
                    molecule.position = Vector2.MoveTowards(molecule.position, bulletPosition.position,
                        MoleculeSpeed * Time.deltaTime);
                }
            }
//            else
//            {
//                int transformIndex = 0;
//                
//                foreach (Transform molecule in moleculeCluster.GetComponentInChildren<Transform>())
//                {
//                    molecule.gameObject.SetActive(true);
////                    Debug.Log("Activated molecule");
//                    transformIndex++;
//                }
//                
//                Debug.Log("MOlecule cluster size step 2: " + moleculeClusterTransforms.Length);
//                Debug.Log("Index is = " + transformIndex);
//                Transform[] transforms = new Transform[transformIndex];
//                transformIndex = 0;
//                foreach (Transform molecule in moleculeCluster.GetComponentInChildren<Transform>())
//                {
//                    transforms[transformIndex] = molecule.transform;
//                    transformIndex++;
////                    Debug.Log("Saved previous position");
//                }
//                
//                foreach (Transform molecule in moleculeCluster.GetComponentInChildren<Transform>())
//                {
//                    molecule.position = Vector2.MoveTowards(molecule.position, bulletPosition.position,
//                        MoleculeSpeed * Time.deltaTime);
//                    WaitForMovingTime(.01f, molecule);
////                    Debug.Log("Moved towards player");
//                }
//
////                WaitForMovingTime(.1f);
//                transformIndex = 0;
//
//                
//            }
        }

        private void DisableSpriteAndColliders()
        {
            spriteRenderer.enabled = false;
            if (colliders != null && health <= 0)
            {
                foreach (Collider2D col in colliders)
                {
                    col.enabled = false;
                }  
            }
            foreach (Transform molecule in moleculeCluster.GetComponentInChildren<Transform>())
            {
                molecule.gameObject.SetActive(true);
            }

            if (health <= 0)
            {
                disabledColliderAndSprite = true;
            }
            
        }
        
        private IEnumerator WaitForMovingTime(float waitTime, Transform molecule)
        {
            yield return new WaitForSeconds(waitTime);
            molecule.gameObject.SetActive(false);
        }

        #endregion
    
    }
}
