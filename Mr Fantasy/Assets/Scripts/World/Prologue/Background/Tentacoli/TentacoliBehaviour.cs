using System;
using System.Collections;
using Player.Gondola;
using UnityEngine;
using World.General.Camera;

namespace World.Prologue.Background.Tentacoli
{
    public class TentacoliBehaviour : MonoBehaviour
    {

        #region Objects

        private Rigidbody2D player;
        private GameObject mainCamera;
        public GameObject tentacolo;
        public GameObject tentacoliPosition;
        private GameObject gondola;

        #endregion

        #region Settings Variables

        public float grabbingSpeed;
        public float movingSpeed;
        private int num = 0;
        public float offset;
        #endregion

        #region Boolean

        public bool grabbed;
        public bool die;

        #endregion
        
        private void Start()
        {
            mainCamera = GameObject.FindWithTag("MainCamera");
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }
        private void LateUpdate()
        {
            if (!grabbed)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + offset, player.transform.position.y),
                    movingSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                grabbed = true;
                GrabPlayer();
            }
        }

        private void GrabPlayer()
        {
            gondola = player.gameObject;
            gondola.GetComponent<GondolaMovement>().enabled = false;
            if (num == 0) Instantiate(tentacolo, new Vector2(tentacoliPosition.transform.position.x, gondola.transform.position.y + 3), Quaternion.identity);
            num++;
            StartCoroutine(WaitAnim(.5f));
//            mainCamera.GetComponent<CameraMoveOnPlayerSlightly>().smoothTime = 1000;
            mainCamera.GetComponent<CameraMoveOnPlayer>().enabled = false;
            StartCoroutine(WaitAndDie(.8f));
        }
        
        IEnumerator WaitAndDie(float timer)
        {
            yield return new WaitForSeconds(timer);
            if (die) Death();
        }

        IEnumerator WaitAnim(float timer)
        {
            yield return new WaitForSeconds(timer);
            gondola.transform.position = new Vector2(gondola.transform.position.x - grabbingSpeed, gondola.transform.position.y);
            die = true;
        }
        
        public void Death()
        {
            // TODO do something to show that the player is dead
            StartCoroutine(mainCamera.GetComponent<LevelManager>().LoadAsync(1));
        }
    }
}
