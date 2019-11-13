using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World.General.HealthManager;

public class GondolaBulletBehaviour : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float speed;
    public float recharge;
    public int num = 0;

    public bool catched;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

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
        if(collider.gameObject.tag.Equals("Enemy"))
        {
            if (!catched)
            {
                catched = true;
                recharge = collider.GetComponent<EnemiesGeneralBehaviour>().damage;
                player = GameObject.FindGameObjectWithTag("Player");
                Destroy(collider.transform.parent.gameObject);
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
        anim.SetBool("Catch", true);
//        Debug.Log("GoBackToPlayer()");
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed/100);
    //    Debug.Log("Distance: " + Mathf.Abs(transform.position.x - player.transform.position.x));
        if(Mathf.Abs(transform.position.x-player.transform.position.x) < 0.05f)
        {
            player.GetComponent<GondolaMovement>().ChangeTransparency(recharge / 100);
            if (num == 0)
            {
                num++;
                gameObject.AddComponent<HealthVariationDisplayer>().ShowHealthVariation(recharge / 100, transform);
                StartCoroutine(Wait());
            }
            
            anim.SetBool("Catch", false);
            this.gameObject.SetActive(false);
        }
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.3f);
        num = 0;
        catched = false;
    }
}
