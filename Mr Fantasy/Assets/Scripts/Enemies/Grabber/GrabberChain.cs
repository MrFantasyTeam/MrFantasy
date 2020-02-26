using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Enemies;
using Player.Gondola;
using UnityEngine;

public class GrabberChain : MonoBehaviour
{
    public Transform target;
    private Transform parent;
    private Vector3 parentOldPosition;
    private Vector3 tempPosition;
    public Rigidbody2D rb;
    private Vector3 startingPosition;
    public float horizontalForce;
    public float timer;
    private bool firstTime = true;
    private float distance = .5f;
    private float gondolaSpeed;
    private float resetTime = 2f;
    private bool setStartPos;
    private bool parentSet;

    private void Start()
    {
        startingPosition = transform.localPosition;
        parent = GetComponent<Transform>().parent;
        target = parent.GetComponent<EnemiesGeneralBehaviour>().player.transform;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > resetTime && parentSet)
        {
            parent.SetParent(null);
            parent.GetComponent<EnemiesGeneralBehaviour>().enabled = true;
            parentSet = false;
            timer = 0;
        }
        if (parentSet)
        {
            transform.localPosition = tempPosition;
            return;
        }
        float angularVelocity = rb.angularVelocity;
        
        if (!setStartPos && timer > 1f)
        {
            setStartPos = true;
            startingPosition = transform.localPosition;
        }

        if (!setStartPos) return;
        if (Mathf.Abs((target.position - transform.position).sqrMagnitude) < 3f * 3f && !parentSet && timer > 2f)
        {
            tempPosition = transform.localPosition;
            Debug.Log("Get temp position");
            GondolaMovement gondolaMovement = target.GetComponent<GondolaMovement>();
            gondolaSpeed = gondolaMovement.speed;
            gondolaMovement.SlowDown(resetTime, 10f);
            gondolaMovement.speed = 10;
            Debug.Log("Set gondola speed");
            parent.SetParent(target);
            Debug.Log("Set parent");
            timer = 0;
            parentSet = true;
            Debug.Log("Blocked player" );
        }
        if (Mathf.Abs((target.position - transform.position).sqrMagnitude) < 30f * 30f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 2f);
            return;
        } 
        
        if (angularVelocity < .01f && angularVelocity > -.01f && Input.GetKey(KeyCode.K))
        {
            if (angularVelocity >= 0)
            {
                rb.AddForce(Vector2.right * horizontalForce);
            }
            else
            {
                rb.AddForce(-1 * horizontalForce * Vector2.right);
            }
            timer = 0;
            Debug.Log("Added force");
        }
        

    }
}
