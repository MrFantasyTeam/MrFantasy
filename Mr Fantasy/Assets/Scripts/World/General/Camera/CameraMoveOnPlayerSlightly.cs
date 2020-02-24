using MainScripts;
using UnityEngine;

 namespace World.General.Camera
 {
     /** Manage the movement and behaviour of the camera. This camera move following the player movements smoothly. */
     public class CameraMoveOnPlayerSlightly : MonoBehaviour
     {
         #region Objects

         protected Rigidbody2D player;
         protected GameObject mainCamera;

         #endregion

         #region Setting Parameters

         protected Vector3 velocity;
         public float smoothTime = 1;

         #endregion
    
         // Use this for initialization
         private void Start()
         {
             mainCamera = GameObject.FindWithTag("MainCamera");
             player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
             velocity = new Vector3(0, 0, transform.position.z);
         }

         private void LateUpdate()
         {
             Vector3 thisPosition = transform.position;
             Vector3 playerPosition = player.transform.position;
             playerPosition = new Vector3(playerPosition.x, playerPosition.y + 5, thisPosition.z);
             thisPosition = Vector3.SmoothDamp(thisPosition, playerPosition, ref velocity, smoothTime);
             transform.position = thisPosition;
         }
     }
 }