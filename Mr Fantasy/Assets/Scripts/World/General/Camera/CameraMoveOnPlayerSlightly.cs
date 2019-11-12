﻿using UnityEngine;
using System.Collections;
 
/** Manage the movement and behaviour of the camera. This camera move following the player movements. **/
 public class CameraMoveOnPlayerSlightly : MonoBehaviour
 {
    #region Objects

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera      

    #endregion

    #region Setting Parameters

    public float xDistance = 1;
    public float yDistance = 1;
    public float speed = 1;

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
        if(Mathf.Abs(transform.position.x - player.transform.position.x) >= xDistance
           || Mathf.Abs(transform.position.y - player.transform.position.y) >= yDistance)
        transform.position = Vector3.MoveTowards(transform.position,  player.transform.position + offset, speed * Time.deltaTime);
    }
}