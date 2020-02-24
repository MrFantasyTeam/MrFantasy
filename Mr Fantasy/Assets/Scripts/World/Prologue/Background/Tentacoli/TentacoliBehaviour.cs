using System.Collections;
using MainScripts;
using Player.Gondola;
using UnityEngine;
using World.General.Camera;

namespace World.Prologue.Background.Tentacoli
{
    public class TentacoliBehaviour : MonoBehaviour
    {

        #region Objects

        private Rigidbody2D player;
        private LevelManager levelManager;
        private GameObject mainCamera;
        public GameObject tentacolo;
        public GameObject tentacoliPosition;
        private GameObject gondola;

        #endregion

        #region Settings Variables

        private Vector3 velocity;
        public float grabbingSpeed;
        public float movingSpeed;
        public float offset;
        private int num;
        private const string PlayerTag = "Player";
        private const string MainCameraTag = "MainCamera";

        #endregion

        #region Boolean

        public bool grabbed;
        public bool die;
        private bool dead;

        #endregion

        #region Default Methods

        private void Start()
        {
            mainCamera = GameObject.FindWithTag(MainCameraTag);
            player = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Rigidbody2D>();
            levelManager = mainCamera.GetComponent<LevelManager>();
            num = 0;
            velocity = new Vector3(0, 0, transform.position.z);

        }
        private void LateUpdate()
        {
            if (grabbed) return;
            Vector3 thisPosition = transform.position;
            Vector3 playerPosition = player.transform.position;
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, thisPosition.z);
            thisPosition = Vector3.SmoothDamp(thisPosition, playerPosition, ref velocity, movingSpeed);
            transform.position = thisPosition;
        }

        #endregion

        #region Custom Method

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(PlayerTag)) return;
            grabbed = true;
            GrabPlayer();
        }

        private void GrabPlayer()
        {
            gondola = player.gameObject;
            gondola.GetComponent<GondolaMovement>().enabled = false;
            if (num == 0) Instantiate(tentacolo, new Vector2(tentacoliPosition.transform.position.x, gondola.transform.position.y + 3), Quaternion.identity);
            num++;
            StartCoroutine(WaitAnim(.5f));
            mainCamera.GetComponent<CameraMoveOnPlayer>().enabled = false;
            StartCoroutine(WaitAndDie(.8f));
        }
        
        private IEnumerator WaitAndDie(float timer)
        {
            yield return new WaitForSeconds(timer);
            if (die) Death();
        }

        /** Wait for animation to complete, then set die to true */
        private IEnumerator WaitAnim(float timer)
        {
            yield return new WaitForSeconds(timer);
            var position = gondola.transform.position;
            position = new Vector2(position.x - grabbingSpeed, position.y);
            gondola.transform.position = position;
            die = true;
        }

        private void Death()
        {
            // TODO do something to show that the player is dead

            if (dead) return;
            dead = true;
            StartCoroutine(levelManager.MenuLoadLevelAsync(1));
        }

        #endregion
    }
}
