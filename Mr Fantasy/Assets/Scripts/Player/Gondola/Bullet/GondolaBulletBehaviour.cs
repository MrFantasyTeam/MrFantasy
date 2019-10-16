using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GondolaBulletBehaviour : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float recharge;

    public bool catched;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!catched) Moving();
        else GoBackToPlayer();
    }

    public void Moving()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided");
        if(collider.gameObject.tag.Equals("Enemy"))
        {
            if (!catched)
            {
                catched = true;
                recharge = collider.GetComponent<EnemiesGeneralBehaviour>().damage;
                player = GameObject.FindGameObjectWithTag("Player");
                Destroy(collider.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("MainCamera"))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void GoBackToPlayer()
    {
        // It should now activate the sphere that goes back to the player
        Debug.Log("GoBackToPlayer()");
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed/100);
        Debug.Log("Distance: " + Mathf.Abs(transform.position.x - player.transform.position.x));
        if(Mathf.Abs(transform.position.x-player.transform.position.x) < 0.05f)
        {
            player.GetComponent<GondolaMovement>().ChangeTransparency(recharge / 100);
            catched = false;
            this.gameObject.SetActive(false);
        }
    }
}
