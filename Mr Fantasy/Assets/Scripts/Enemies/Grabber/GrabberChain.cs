using Player.Gondola;
using UnityEngine;

namespace Enemies.Grabber
{
    public class GrabberChain : MonoBehaviour
    {
        #region Objects

        public Transform target;
        private Transform parent;
        private GrabberBehaviour grabberBehaviour;
        public Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private GondolaMovement gondolaMovement;

        #endregion
        
        #region Settings Parameters

        private const float ActDistance = 3f;
        private const float SearchingDistance = 25f;
        private const float Speed = 40f;
        private float resetTime;
        private Vector3 startingPosition;
        private Vector3 parentOldPosition;
        public float horizontalForce;
        public float timer;
        private float angularVelocity;
        private float distanceFromHead;
        private int defaultSortingOrder;
        #endregion

        #region Boolean Values

        private bool setStartPos;
        private bool parentSet;

        #endregion

        #region Default Methods

        private void Start()
        {
            parent = GetComponent<Transform>().parent;
            grabberBehaviour = parent.GetComponent<GrabberBehaviour>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultSortingOrder = spriteRenderer.sortingOrder;
            target = grabberBehaviour.player.transform;
            gondolaMovement = target.GetComponent<GondolaMovement>();
            resetTime = parent.gameObject.GetComponent<GrabberBehaviour>().attackInterval;
            distanceFromHead = Mathf.Abs((transform.position - transform.parent.position).sqrMagnitude);
        }
        
        private void FixedUpdate()
        {
            timer += Time.deltaTime;

            if (parentSet)
            {
                transform.position = target.position;
                if (timer <= resetTime) return;
                DetachFromPlayer();
            }

            if (!setStartPos)
            {
                if (timer <= 1f) return;
                setStartPos = true;
                return;
            }
            
            if (Mathf.Abs((target.position - transform.position).sqrMagnitude) < ActDistance * ActDistance 
                && !parentSet && timer > 2f)
            {
                AttachToPlayer();
                return;
            }

            if (Mathf.Abs((transform.position - transform.parent.position).sqrMagnitude) >= distanceFromHead - 1) return;
            if (Mathf.Abs((target.position - transform.position).sqrMagnitude) < SearchingDistance * SearchingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
                return;
            } 
            
            angularVelocity = rb.angularVelocity;
            if (angularVelocity < .01f && angularVelocity > -.01f)
            {
                AddForce();
            }
        }

        #endregion

        #region Custom Methods

        private void AttachToPlayer()
        {
            parent.SetParent(target);
            gondolaMovement.SlowDown(resetTime, 5f);
            rb.bodyType = RigidbodyType2D.Kinematic;
            spriteRenderer.sortingOrder = gondolaMovement.spriteRenderer.sortingOrder + 1;
            grabberBehaviour.attached = true;
            timer = 0;
            parentSet = true;
        }

        private void DetachFromPlayer()
        {
            parent.SetParent(null);
            rb.bodyType = RigidbodyType2D.Dynamic;
            grabberBehaviour.attached = false;
            spriteRenderer.sortingOrder = defaultSortingOrder;
            parentSet = false;
            timer = 0;
        }
        
        private void AddForce()
        {
            if (angularVelocity >= 0) rb.AddForce(Vector2.right * horizontalForce);
            else rb.AddForce(-1 * horizontalForce * Vector2.right);
            timer = 0;
        }

        #endregion

        
    }
}
