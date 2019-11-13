using UnityEngine;

 namespace World.General.Camera
 {
     /** Manage the movement and behaviour of the camera. This camera move following the player movements smoothly. */
     public class CameraMoveOnPlayerSlightly : MonoBehaviour
     {
         #region Objects

         private GameObject player;   

         #endregion

         #region Setting Parameters
    
         public float speed = 1;
         private float interpolation;

         #endregion
    
         // Use this for initialization
         void Start()
         {
             player = GameObject.FindGameObjectWithTag("Player");
             interpolation = speed * Time.deltaTime;
         }
    
         void LateUpdate()
         {
             Vector2 thisPosition = transform.position;
             Vector2 playerPosition = player.transform.position;
             thisPosition = new Vector2(Mathf.Lerp(thisPosition.x, playerPosition.x, interpolation),
                                        Mathf.Lerp(thisPosition.y, playerPosition.y, interpolation));
             transform.position = thisPosition;
         }
     }
 }