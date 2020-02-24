using System;
using UnityEngine;

namespace World.General.Background
{
    public class BackgroundMover : MonoBehaviour
    {

        #region Objects

        public GameObject[] backgrounds; 

        #endregion
        
        #region Settings Parameters 
        
        private float offset;
        public int backgroundsCounter = 1;
        public int lastInactive;
        public float previousXPosition;

        #endregion
        void Start()
        {
            lastInactive = backgrounds.Length - 1;
            foreach (GameObject background in backgrounds)
            {
                background.SetActive(true);
            }

            previousXPosition = transform.position.x;
            offset = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        }
        
        void Update()
        {
            if (Mathf.Abs(transform.position.x - backgrounds[backgroundsCounter].transform.position.x) < offset) return;
            if (transform.position.x > previousXPosition)
            {
                manageCounterWhileGoingRight();
            } else if (transform.position.x < previousXPosition)
            {
                manageCounterWhileGoingLeft();
            }
            
            previousXPosition = transform.position.x;
        }

        private void manageCounterWhileGoingRight()
        {
            if (backgroundsCounter == 0)
            {
                backgrounds[backgrounds.Length - 1].transform.position = new Vector3(backgrounds[lastInactive].transform.position.x + offset,
                    backgrounds[lastInactive].transform.position.y, backgrounds[lastInactive].transform.position.z);
                backgroundsCounter++;
                if (lastInactive == backgrounds.Length - 1) lastInactive = 0;
                else lastInactive++;
            } else if (backgroundsCounter == backgrounds.Length - 1)
            {
                backgrounds[backgrounds.Length - 2].transform.position = new Vector3(backgrounds[lastInactive].transform.position.x + offset,
                    backgrounds[lastInactive].transform.position.y, backgrounds[lastInactive].transform.position.z);
                backgroundsCounter = 0;
                lastInactive = backgrounds.Length - 2;
            }
            else
            {
                backgrounds[backgroundsCounter - 1].transform.position = new Vector3(backgrounds[lastInactive].transform.position.x + offset,
                    backgrounds[lastInactive].transform.position.y, backgrounds[lastInactive].transform.position.z);
                backgroundsCounter++;
                if (lastInactive == backgrounds.Length - 1) lastInactive = 0;
                else lastInactive++;
            }
        }

        private void manageCounterWhileGoingLeft()
        {
            if (backgroundsCounter == 0)
            {
                backgrounds[lastInactive].transform.position = new Vector3(backgrounds[backgrounds.Length - 1].transform.position.x - offset,
                    backgrounds[backgrounds.Length - 1].transform.position.y, backgrounds[backgrounds.Length - 1].transform.position.z);
                backgroundsCounter = backgrounds.Length - 1;
                lastInactive--;
            } else if (backgroundsCounter == backgrounds.Length - 1)
            {
                backgrounds[lastInactive].transform.position = new Vector3(backgrounds[backgrounds.Length - 2].transform.position.x - offset,
                    backgrounds[backgrounds.Length - 2].transform.position.y, backgrounds[backgrounds.Length - 2].transform.position.z);
                backgroundsCounter--;
                if (lastInactive == 0) lastInactive = backgrounds.Length - 1;
                else lastInactive--;
            }
            else
            {
                backgrounds[lastInactive].transform.position = new Vector3(backgrounds[backgroundsCounter - 1].transform.position.x - offset,
                    backgrounds[backgroundsCounter - 1].transform.position.y, backgrounds[backgroundsCounter - 1].transform.position.z);
                backgroundsCounter--;
                if (lastInactive == 0) lastInactive = backgrounds.Length - 1;
                else lastInactive--;
            }
        }
    }
}
