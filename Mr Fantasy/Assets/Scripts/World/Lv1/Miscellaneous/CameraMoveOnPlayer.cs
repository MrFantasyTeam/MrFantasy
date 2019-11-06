using UnityEngine;
using System.Collections;

/** Manage the movement and behaviour of the camera. This camera move following the player movements. **/
public class CameraMoveOnPlayer : MonoBehaviour
{
    #region Objects

    public GameObject player;     
    private Vector3 offset;       

    #endregion
    
    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}