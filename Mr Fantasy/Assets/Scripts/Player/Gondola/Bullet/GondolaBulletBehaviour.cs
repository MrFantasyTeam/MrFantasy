﻿using System.Collections;
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

        #endregion

        #region Setting Properties

        public float speed;
        private float recharge;
        private static readonly int Catch = Animator.StringToHash("Catch");
        private static readonly int GoBack = Animator.StringToHash("GoBack");
        private static readonly int Default = Animator.StringToHash("Default");
        private const float CatchAnimDuration = .5f;

        #endregion

        #region Boolean Values

        public bool caught; // the enemy has been caught
        public bool hit; // the enemy has been hit
        public bool catchAnimTriggered; // the catch anim has started
        
        #endregion

        #region Default Methods

        private void Awake()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindWithTag("Player").GetComponent<GondolaMovement>();
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
            other.gameObject.tag = "DeadEnemy";
            if (hit) return;
            hit = true;
            enemy = other.gameObject;
            recharge = other.GetComponent<EnemiesGeneralBehaviour>().damage;
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
            enemy.GetComponent<EnemiesGeneralBehaviour>().Decompose(transform);
            StartCoroutine(WaitForAnimEnd(GoBack, CatchAnimDuration));
        }

        private void GoBackToPlayer()
        {
            Destroy(enemy);
            anim.SetTrigger(GoBack);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed / 100);
            if (!(Mathf.Abs(transform.position.x - player.transform.position.x) < 0.05f)) return;
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
        
        private IEnumerator WaitForAnimEnd(int animName, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            caught = true;
        }

        #endregion
        
    }
}
