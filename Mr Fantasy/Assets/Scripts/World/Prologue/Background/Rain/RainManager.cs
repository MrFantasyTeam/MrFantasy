using System.Collections;
using UnityEngine;

namespace World.Prologue.Background.Rain
{
    /** Controls rain behaviour */
    public class RainManager : MonoBehaviour    
    {
        #region Objects

        private Animator anim;

        #endregion

        #region Settings Properties

        private const string AnimName = "HeavyRain";
        private const float WaitTime = 30;
        private float timer;

        #endregion

        #region Boolean Values

        private bool entered;

        #endregion

        #region Default Methods

        private void Awake()
        {
            anim = GetComponent<Animator>();
            timer = WaitTime;
        }

        private void Update()
        {
            if (!entered)
                timer += Time.deltaTime;
        }

        #endregion

        #region Custom Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            // If hit game object has correct tag and timer is correct
            // then set anim to heavy rain, reset counter and damage player
            if (other.gameObject.CompareTag(AnimName) || timer >= WaitTime)
            {
                anim.SetBool(AnimName, true);
                entered = true;
                timer = 0;
                StartCoroutine(Wait());
            }
        }

        /** Set the anim to light rain and start timer. */
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(AnimName)) return;
            anim.SetBool(AnimName, true);
            entered = false;
        }

        /** Wait for x seconds and then damage player. */
        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
            GameObject.FindWithTag("Player").GetComponent<GondolaMovement>().ChangeTransparency(-10);
        }

        #endregion
    }
}