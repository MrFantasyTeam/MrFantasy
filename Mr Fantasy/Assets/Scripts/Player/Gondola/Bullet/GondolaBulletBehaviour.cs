using System;
using System.Collections;
using Enemies;
using UnityEngine;

namespace Player.Gondola.Bullet
{
    public class GondolaBulletBehaviour : MonoBehaviour
    {
        #region Objects

        public GondolaMovement player;
        private Animator anim;

        #endregion

        #region Setting Properties

        public float speed;
        private float recharge;
        private static readonly int Catch = Animator.StringToHash("Catch");
        private static readonly int GoBack = Animator.StringToHash("GoBack");
        private static readonly int Default = Animator.StringToHash("Default");
        private const float CatchAnimDuration = .3f;

        #endregion

        #region Boolean Values

        public bool caught; // the enemy has been caught
        public bool hit; // the enemy has been hit
        
        #endregion

        #region Default Methods

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            caught = false;
            hit = false;
        }
        
        private void FixedUpdate()
        {
            if (!hit) Moving();
            else
            {
                if (!caught) CatchEnemy();
                GoBackToPlayer();
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
            recharge = other.GetComponent<EnemiesGeneralBehaviour>().damage;
            Destroy(other.transform.parent.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.tag.Equals("MainCamera"))
                gameObject.SetActive(false);
        }

        private void CatchEnemy()
        {
            anim.SetTrigger(Catch);
            StartCoroutine(WaitForAnimEnd(GoBack, CatchAnimDuration));
            caught = true;
        }

        private void GoBackToPlayer()
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed/100);
            if (!(Mathf.Abs(transform.position.x - player.transform.position.x) < 0.05f)) return;
            player.ChangeTransparency(recharge / 100);
            anim.SetTrigger(Default);
            gameObject.SetActive(false);
        }
        
        private IEnumerator WaitForAnimEnd(int animName, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            anim.SetTrigger(animName);
        }

        #endregion
        
    }
}
