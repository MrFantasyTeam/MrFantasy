using UnityEngine;

namespace World.General.Camera
{
    public class CameraMoveOnPlayer : MonoBehaviour
    {

        #region Objects

        public GameObject player;       //Public variable to store a reference to the player game object
        private Vector3 tempTransform;
        
        #endregion

        #region Settings properties

        private Vector3 offset;         //Private variable to store the offset distance between the player and camera
        public float maxHeight;
        public float minHeight;

        #endregion

        #region Boolean values

        public bool clampYAxis;

        #endregion

        // Use this for initialization
        void Start()
        {
            //Calculate and store the offset value by getting the distance between the player's position and camera's position.
            player = GameObject.FindGameObjectWithTag("Player");
            offset = transform.position - player.transform.position;
        }

        // LateUpdate is called after Update each frame
        void FixedUpdate()
        {
            tempTransform = transform.position;
            if (clampYAxis)
            {
                if (player.transform.position.y >= maxHeight || player.transform.position.y <= minHeight)
                    tempTransform = new Vector3(player.transform.position.x + offset.x, tempTransform.y, tempTransform.z);
                else tempTransform = player.transform.position + offset;
            } else tempTransform = player.transform.position + offset;

            transform.position = tempTransform;
        }
    }
}