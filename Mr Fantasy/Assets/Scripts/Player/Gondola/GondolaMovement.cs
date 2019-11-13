using System.Collections;
using Enemies;
using UnityEngine;
using World.General.HealthManager;

/** Script to control the player's movement and behaviour in Gondola form, in the Prologue scene **/
public class GondolaMovement : MonoBehaviour
{
    #region Objects
    
    public GameObject mainCamera;
    private SpriteRenderer sprite;
    public Transform bulletPosition;

    #endregion

    #region Settings Parameters

    public float speed;
    public float health;
    public float shootTime;
    public float num;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        sprite = GetComponent<SpriteRenderer>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootTime += Time.deltaTime;
        Move();
        if (health <= 0) Death();
        if (Input.GetKey(KeyCode.C)) Shoot();
    }

    private void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        transform.Translate(horizontalMovement * Time.deltaTime * speed, verticalMovement * Time.deltaTime * speed, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Enemy")) return;
        if (num != 0) return;
        num++;
        float damage = collision.GetComponent<EnemiesGeneralBehaviour>().damage;
        ChangeTransparency(-damage / (10 * 100));
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        num = 0;
    }

    public void ChangeTransparency(float variation)
    {
        if (health + (variation) > 100)
            health = 100;
        else 
            health += variation;
        Color color = sprite.color;
        color.a += variation;
        sprite.color = new Color(color.r, color.g, color.b, color.a);
        // display the increase / decrease of health
        gameObject.AddComponent<HealthVariationDisplayer>().ShowHealthVariation(variation, transform);
        StartCoroutine(Wait());
    }

    private void Death()
    {
        // TODO do something to show that the player is dead
        StartCoroutine(mainCamera.GetComponent<LevelManager>().LoadAsync(1));
    }

    private void Shoot()
    {
        if (shootTime < 1) return;
        ObjectPooler.SharedIntance.SpawnFromPool("bullet", bulletPosition.position, Quaternion.identity);
        shootTime = 0;
    }
}
