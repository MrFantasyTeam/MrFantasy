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
        public int backgroundsCounter = 0;
        public int lastInactive;

        #endregion
        void Start()
        {
            lastInactive = backgrounds.Length - 1;
            foreach (GameObject background in backgrounds)
            {
                background.SetActive(true);
            }
            
            offset = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        }
        
        void Update()
        {
            if (Mathf.Abs(transform.position.x - backgrounds[backgroundsCounter].transform.position.x) < offset) return;
            backgrounds[backgroundsCounter].transform.position = new Vector3(backgrounds[lastInactive].transform.position.x + offset, 
                backgrounds[lastInactive].transform.position.y, backgrounds[lastInactive].transform.position.z);
            backgrounds[backgroundsCounter].gameObject.GetComponent<SpriteRenderer>().flipX =
                !backgrounds[backgroundsCounter].gameObject.GetComponent<SpriteRenderer>().flipX;
            // TODO make it possibile in other levels to go backward 
            manageCounterWhileGoingRight();
        }

        private void manageCounterWhileGoingRight()
        {
            if (backgroundsCounter < backgrounds.Length - 1)
            {
                backgroundsCounter++;
                if (lastInactive == backgrounds.Length - 1) lastInactive = 0;
                else lastInactive++;
            }
            else
            {
                backgroundsCounter = 0;
                lastInactive = backgrounds.Length - 1;
            }
        }
    }
}
