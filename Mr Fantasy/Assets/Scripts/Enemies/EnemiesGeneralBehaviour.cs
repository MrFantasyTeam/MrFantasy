using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGeneralBehaviour : MonoBehaviour
{
    public GameObject player;
    public FollowPath myPath;
    public float damage;
    public float speed = 2;
    public float direction;

    public bool spottedPlayer;
    public bool facingRight;


    private void Start()
    {
        myPath = GetComponent<FollowPath>();
    }

    private void FixedUpdate()
    {
        if (spottedPlayer)
        {
            Debug.Log("Spotted player", gameObject);
            myPath.enabled = false;
            MoveTowardsPlayer();
        }
        else
        {
            Debug.Log("Unspotted player", gameObject);
            myPath.enabled = true;
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 2 * Time.deltaTime);
        if (transform.position.x > player.transform.position.x)
        {
            if (!facingRight)
                Flip();
        }
        else
        {
            if (facingRight)
                Flip();
        }
    }
    
    public void Flip()
    {
        facingRight = !facingRight;
        Transform transform1 = transform;
        Vector3 theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }
}
