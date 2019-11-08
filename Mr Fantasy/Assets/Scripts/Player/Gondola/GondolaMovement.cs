using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Script to control the player's moevement and beahviour in Gondola form, in the Prologue scene **/
public class GondolaMovement : MonoBehaviour
{
    private ObjectPooler objectPooler;
    public GameObject mainCamera;
    SpriteRenderer sprite;
    public GameObject bullet;
    public Transform bulletPosition;

    public float speed;
    public float health;
    public float shootTime;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        sprite = this.GetComponent<SpriteRenderer>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        objectPooler = ObjectPooler.SharedIntance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootTime += Time.deltaTime;
        Move();
        //ChangeTransparency(-0.001f);
        if (health <= 0) Death();
        if (Input.GetKey(KeyCode.C)) Shoot();
    }

    public void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        transform.Translate(horizontalMovement * Time.deltaTime * speed, verticalMovement * Time.deltaTime * speed, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Enemy"))
        {
            float damage = collision.GetComponent<EnemiesGeneralBehaviour>().damage;
            ChangeTransparency(-damage / (10 * 10 * 100));
        }
    }

    public void ChangeTransparency(float variation)
    {
        if (health + (variation * 100) > 100)
            health = 100;
        else 
            health += variation * 100;
        Color color = sprite.color;
        color.a += variation;
        sprite.color = new Color(color.r, color.g, color.b, color.a);
    }

    public void Death()
    {
        // do something to show that the player is dead
        StartCoroutine(mainCamera.GetComponent<LevelManager>().LoadAsync(1));
    }

    public void Shoot()
    {
        if(shootTime > 1)
        {
            ObjectPooler.SharedIntance.SpawnFromPool("bullet", bulletPosition.position, Quaternion.identity);
            shootTime = 0;
        }            
    }
}
