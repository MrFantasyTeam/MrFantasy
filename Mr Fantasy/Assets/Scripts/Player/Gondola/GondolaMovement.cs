using System;
using System.Collections;
using MainScripts;
using UnityEngine;
using World.General.Death;

/** Script to control the player's movement and behaviour in Gondola form, in the Prologue scene **/
namespace Player.Gondola
{
    public class GondolaMovement : MonoBehaviour
    {
        #region Objects
    
        public GameObject mainCamera;
        public DeathManager deathManager;
        public GameObject hitAnim;
        private LevelManager levelManager;
        public Transform bulletPosition;
        private Animator anim;
        public Animator smallBarrierAnim;
        private Animator mediumBarrierAnim;
        private Animator bigBarrierAnim;
        private Rigidbody2D rb;
        private Transform playerDefaultRotation;
        public GameObject[] barriers;
        public SpriteRenderer spriteRenderer;
        private AnimatorStateInfo animatorStateInfo;
        private AnimatorStateInfo barrierAnimatorStateInfo;
        private AnimatorStateInfo hitAnimatorStateInfo;

        #endregion

        #region Settings Parameters

        private Color blinkColor;
        public float speed;
        private float initialSpeed;
        public float rotationSpeed;
        public float health;
        public float barrierHealth;
        private const float CoolDownTime = 6f;
        private float coolDownTimer;
        public float shootTime;
        public int barrierIndex;
        private int tempBarrierIndex;
        private int bulletCounter;
        private float hitAnimLength;
        public float swingDegree;

        #endregion

        #region Boolean

        public bool isShooting;
        public bool barrierIsActive;
        public bool disableCheckOnPlayerPosition;
        private bool dead;
        private bool previousAnimMoving;
        private bool deactivatedBarrier;
        private bool swing;

        #endregion

        #region Default Methods
        // Start is called before the first frame update
        private void Start()
        {
            health = 100;
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            levelManager = mainCamera.GetComponent<LevelManager>();
            levelManager.dead = false;
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            blinkColor = new Color32(210, 132, 253, 255);
            initialSpeed = speed;
            smallBarrierAnim = barriers[0].GetComponent<Animator>();
            mediumBarrierAnim = barriers[1].GetComponent<Animator>();
            bigBarrierAnim = barriers[2].GetComponent<Animator>();
            hitAnimatorStateInfo = hitAnim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            hitAnimLength = hitAnimatorStateInfo.length;
            hitAnim.SetActive(false);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            shootTime += Time.deltaTime;
            coolDownTimer += Time.deltaTime;
            if (coolDownTimer >= CoolDownTime) ResetCoolDown();
            if (!swing) transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, 0, 0, 1), 
            rotationSpeed * Time.deltaTime);
            Move();
            if (health <= 0) Death();
            if (Input.GetKey(KeyCode.C)) Shoot();
            if (Input.GetKey(KeyCode.A)) Death();
            ManageBarrier();
            if (swing) SwingGondola();
        }
        
        #endregion

        #region Custom Method
        
        private void Move()
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");
            transform.Translate(horizontalMovement * Time.deltaTime * speed, verticalMovement * Time.deltaTime * speed, 0);
            animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (horizontalMovement.Equals(0) && verticalMovement.Equals(0)) ManageIdleAnim();
            else ManageMovingAnim();
        }
        
        public void PlayerTakeDamage(float variation, bool enemyAttack)
        {
            if (variation <= 0)
            {
                spriteRenderer.color = blinkColor;
//                hitAnim.SetActive(true);
//                StartCoroutine(WaitForBlinking(hitAnimLength));
                if (enemyAttack)
                {
                    swingDegree = variation / 2;
                    swing = true;
                }
                StartCoroutine(WaitForBlinking2(.1f));
            }
            ManagePlayerAndBarrierHealth(variation);
        }

        public void Death()
        {
            if (dead) return;
            dead = true;
            anim.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            GameObject player = gameObject;
            player.tag = "Untagged";
            player.layer = 1;
            deathManager.enabled = true;
            deathManager.levelNumber = 1;
            deathManager.deathAnimName = "Dead - Transition";
            deathManager.startDeathAnim = true;
            enabled = false;
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
                if (barrierIndex == 2 && barriers[barrierIndex].activeInHierarchy) return;
                barriers[barrierIndex].SetActive(false);
                tempBarrierIndex = barrierIndex;
                barrierIndex = 2;
                barriers[barrierIndex].SetActive(true);
                barrierIsActive = true;
                bigBarrierAnim.CrossFade("Transition - Medium Big", 0);
            } else if (barrierHealth >= 50 && barrierHealth < 75)
            {
                if (barrierIndex == 1 && barriers[barrierIndex].activeInHierarchy) return;
                barriers[barrierIndex].SetActive(false);
                tempBarrierIndex = barrierIndex;
                barrierIndex = 1;
                barriers[barrierIndex].SetActive(true);
                barrierIsActive = true;
                if (tempBarrierIndex > barrierIndex)
                {
                    mediumBarrierAnim.CrossFade("Transition - Big Medium", 0);
                    return;
                }
                mediumBarrierAnim.CrossFade("Transition - Small Medium", 0);
            } else if (barrierHealth >= 25 && barrierHealth < 50)
            {
                if (barrierIndex == 0 && barriers[barrierIndex].activeInHierarchy) return;
                barriers[barrierIndex].SetActive(false);
                tempBarrierIndex = barrierIndex;
                barrierIndex = 0;
                barriers[barrierIndex].SetActive(true);
                barrierIsActive = true;
                if (tempBarrierIndex > barrierIndex)
                {
                    smallBarrierAnim.CrossFade("Transition - Medium Small", 0);
                    return;
                }

                smallBarrierAnim.CrossFade("Transition - 0 Small", 0);
            }
            else
            {
                if (!smallBarrierAnim.isActiveAndEnabled) return;
                if (!deactivatedBarrier)
                {
                    smallBarrierAnim.CrossFade("Transition - Small 0", 0);
                    deactivatedBarrier = true;
                    StartCoroutine(WaitForBarrierReduction(.33f));
                }

                barrierAnimatorStateInfo = smallBarrierAnim.GetCurrentAnimatorStateInfo(0);
                if (!barrierAnimatorStateInfo.IsName("Transition - Small 0")) return;
                if (barrierAnimatorStateInfo.normalizedTime < .95f) return;
                barrierIndex = 0;
                deactivatedBarrier = false;
                barrierIsActive = false;
            }

            if (barriers[barrierIndex].activeInHierarchy)
                barriers[barrierIndex].transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    new Quaternion(0, 0, 0, 1),
                    500 * Time.deltaTime);
        }

        private void ManagePlayerAndBarrierHealth(float variation)
        {
            if (health + variation > 100) health = 100;
            else health += variation;
            if (barrierHealth < 0) barrierHealth = 0;
            if (variation < 0) return;
            if (barrierHealth + variation > 100) {barrierHealth = 100;}
            else barrierHealth += variation;
            if (barrierHealth >= 25) barrierIsActive = true;
        }

        private void ManageMovingAnim()
        {
            if (previousAnimMoving && animatorStateInfo.normalizedTime < 1f) return;
            if (health > 50) anim.CrossFade("GondolaMoving", 0);
            else if (health > 20 && health <= 50) anim.CrossFade("GondolaDamaged - Moving", 0);
            else if (health > 10 && health <= 20) anim.CrossFade("GondolaAlmostDying - Moving", 0);
            else anim.CrossFade("GondolaDying - Moving", 0);
            previousAnimMoving = true;
        }

        private void ManageIdleAnim()
        {
            if (animatorStateInfo.normalizedTime < 1f) return;
            // TODO quando hai anche le anim idle
            if (health > 50) anim.CrossFade("GondolaIdle", 0);
            else if (health > 20 && health <= 50) anim.CrossFade("GondolaDamaged - Idle", 0);
            else if (health > 10 && health <= 20) anim.CrossFade("GondolaAlmostDying - Idle", 0);
            else anim.CrossFade("GondolaDying - Idle", 0);
            previousAnimMoving = false;
        }

        private void ResetCoolDown()
        {
            bulletCounter = 0;
            coolDownTimer = 0;
        }

        public void SlowDown(float time, float changedSpeed)
        {
            speed = changedSpeed;
            StartCoroutine(WaitForSlowDownTime(time, initialSpeed));
        }
        
        private void SwingGondola()
        {
            StartCoroutine(WaitForSwing(.4f));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, swingDegree), 100 * Time.deltaTime);
            float eulerAngleZ = Mathf.Ceil(transform.eulerAngles.z);
            if (eulerAngleZ != swingDegree && eulerAngleZ != 360 + swingDegree) return;
            swingDegree = -swingDegree;
        }

        private IEnumerator WaitForSlowDownTime(float time, float previousSpeed)
        {
            yield return new WaitForSeconds(time);
            speed = previousSpeed; 
        }

        private IEnumerator WaitForBlinking(float time)
        {
            yield return new WaitForSeconds(time);
            hitAnim.SetActive(false); 
        }
        
        private IEnumerator WaitForBlinking2(float time)
        {
            yield return new WaitForSeconds(time);
            spriteRenderer.color = Color.white;
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

        private IEnumerator WaitForSwing(float time)
        {
            yield return new WaitForSeconds(time);
            swing = false;
        }

        private IEnumerator WaitForBarrierReduction(float time)
        {
            yield return new WaitForSeconds(time);
            barriers[barrierIndex].SetActive(false);
            deactivatedBarrier = false;
        }

        #endregion
        
        #endregion
    }
}
