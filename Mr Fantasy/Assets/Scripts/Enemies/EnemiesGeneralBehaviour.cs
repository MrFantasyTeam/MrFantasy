using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGeneralBehaviour : MonoBehaviour
{
    public GameObject player;
    public float damage;
    public float speed = 2;
    public float direction;

    public bool spottedPlayer;

    private void FixedUpdate()
    {
        if (spottedPlayer)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Roam();
        }
    }
    public void Roam()
    {
        transform.Translate(Time.deltaTime * speed, 0, 0);
    }

    public void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * 2);
    }
}
