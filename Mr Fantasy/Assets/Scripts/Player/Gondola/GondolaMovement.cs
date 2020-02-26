using System;
using System.Collections;
using MainScripts;
using UnityEngine;

/** Script to control the player's movement and behaviour in Gondola form, in the Prologue scene **/
namespace Player.Gondola
{
    public class GondolaMovement : MonoBehaviour
    {
        #region Objects
    
        public GameObject mainCamera;
        private LevelManager levelManager;
        public Transform bulletPosition;
        private Animator anim;
        public Animator spheresOnBoatAnim;
        private Transform playerDefaultRotation;
        public GameObject[] barriers;

        #endregion

        #region Settings Parameters

        private static readonly int Moving = Animator.StringToHash(MovingAnimBool);
        private const  string MovingAnimBool = "Move";
        public float speed;
        public float rotationSpeed;
        public float health;
        public float barrierHealth;
        private const float CoolDownTime = 6f;
        private float coolDownTimer;
        public float shootTime;
        public int barrierIndex;
        private int bulletCounter = 0;

        #endregion

        #region Boolean

        public bool isShooting;
        private bool dead;

        #endregion

        #region Default Methods
        // Start is called before the first frame update
        private void Start()
        {
            health = 100;
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            levelManager = mainCamera.GetComponent<LevelManager>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            shootTime += Time.deltaTime;
            coolDownTimer += Time.deltaTime;
            if (coolDownTimer >= CoolDownTime) ResetCoolDown();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, 0, 0, 1), 
            rotationSpeed * Time.deltaTime);
            Move();
            if (health <= 0) Death();
            if (Input.GetKey(KeyCode.C)) Shoot();
            if (Input.GetKey(KeyCode.A)) Death();
        }
        
        #endregion

        #region Custom Method
        
        private void Move()
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");
            transform.Translate(horizontalMovement * Time.deltaTime * speed, verticalMovement * Time.deltaTime * speed, 0);
            if (horizontalMovement.Equals(0) && verticalMovement.Equals(0))
            {
                anim.SetBool(Moving, false);
                spheresOnBoatAnim.SetBool(Moving, false);
            }
            else
            {
                anim.SetBool(Moving, true);
                spheresOnBoatAnim.SetBool(Moving, true);
            }
        }
        
        public void ChangeTransparency(float variation)
        {
            ManagePlayerAndBarrierHealth(variation);
            ManageBarrier();
        }

        private void Death()
        {
            // TODO do something to show that the player is dead
            if (dead) return;
            dead = true;
            StartCoroutine(levelManager.MenuLoadLevelAsync(1));
        }

        private void Shoot()
        {
            if (shootTime < 0.3f) return;
            if (bulletCounter == 10 && coolDownTimer < CoolDownTime) return;
            isShooting = true;
            ObjectPooler.SharedIntance.SpawnFromPool("bullet", bulletPosition.position, Quaternion.identity);
            shootTime = 0;
            bulletCounter++;
        }

        #region Secondary Methods

        private void ManageBarrier()
        {
            if (barrierHealth >= 75)
            {
                barriers[barrierIndex].SetActive(false);
                barrierIndex = 2;
                barriers[barrierIndex].SetActive(true);
            } else if (barrierHealth >= 50 && barrierHealth < 75)
            {
                barriers[barrierIndex].SetActive(false);
                barrierIndex = 1;
                barriers[barrierIndex].SetActive(true);
            } else if (barrierHealth >= 25 && barrierHealth < 50)
            {
                barriers[barrierIndex].SetActive(false);
                barrierIndex = 0;
                barriers[barrierIndex].SetActive(true);
            }
            else
            {
                barriers[barrierIndex].SetActive(false);
                barrierIndex = 0;
            }
        }

        private void ManagePlayerAndBarrierHealth(float variation)
        {
            if (health + variation > 100) health = 100;
            else health += variation;
            if (barrierHealth + variation > 100) barrierHealth = 100;
            else if (barrierHealth + variation < 0) barrierHealth = 0;
            else barrierHealth += variation;
        }

        private void ResetCoolDown()
        {
            bulletCounter = 0;
            coolDownTimer = 0;
        }

        public void SlowDown(float time, float changedSpeed)
        {
            float tempSpeed = speed;
            speed = changedSpeed;
            StartCoroutine(WaitForSlowDownTime(time, tempSpeed));
        }

        private IEnumerator WaitForSlowDownTime(float time, float previousSpeed)
        {
            yield return new WaitForSeconds(time);
            speed = previousSpeed;
        }

        #endregion
        
        #endregion
    }
}
