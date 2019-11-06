using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Script to control the movement and behaviour of the projectile shot by Player in fish form. ***/
public class ProjectileMovement : MonoBehaviour {

    #region Objects

    private Rigidbody2D rb;
    private Transform playerRot;
    private Transform[] children;
    private GameObject projectileStep1;
    private GameObject projectileStep2;

    #endregion

    #region Setting Parameters

    public float speed;
    private float timer;
    public float projectileStep1Time;    
    public float projectileStep2Time;    
    public float destroyTime;
    private float rotationY;
    private float rotationZ;
    private int bulletStep1 = 0;

    #endregion

    #region Default Methods

    // Use this for initialization
    void Start ()
    {
        // get player rotation
        playerRot = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rotationY = playerRot.rotation.eulerAngles.y;
        rotationZ = playerRot.rotation.eulerAngles.z;

        // get children and set them to inactive
        children = gameObject.GetComponentsInChildren<Transform>();
        projectileStep1 = children[1].gameObject;
        projectileStep2 = children[2].gameObject;
        rb = this.GetComponent<Rigidbody2D>();
        projectileStep1.SetActive(false);
        projectileStep2.SetActive(false);         
    }
	
    void FixedUpdate()
    {     
        timer += Time.deltaTime;

        if (bulletStep1 == 0) Shoot();

        if (bulletStep1 == 0)
        {
            projectileStep1.SetActive(true);            
            bulletStep1++;     
        }
        if (timer >= projectileStep1Time) Destroy(projectileStep1);
        
        if (timer >= projectileStep2Time) projectileStep2.SetActive(true);

        if (timer >= destroyTime) Destroy(this.gameObject);        
    }

    #endregion

    #region Custom Methods

    /** Position and move the projectile based on the player orientation. **/
    public void Shoot()
    {        
        // east
        if (rotationY == 0 && rotationZ == 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));            
            rb.velocity = new Vector2(speed, 0);
        }
        // north
        else if (rotationY == 0 && rotationZ == 90)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            rb.velocity = new Vector2(0, speed);
        }
        // east
        else if (rotationY == 180 && rotationZ == 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            rb.velocity = new Vector2(-speed, 0);
        }
        // south
        else if (rotationY == 180 && rotationZ == 270)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 270));
            rb.velocity = new Vector2(0, -speed);
        }
        // south west
        else if (rotationY == 180 && rotationZ == 315)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, -45));
            rb.velocity = new Vector2(-speed * .75f, -speed * .75f);
        }
        // south east
        else if (rotationY == 0 && rotationZ == 315)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 315));
            rb.velocity = new Vector2(speed * .75f, -speed * .75f);
        }
        else
        {
            // north west
            if (rotationY == 180)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 45));
                rb.velocity = new Vector2(-speed * .75f, speed * .75f);
            }
            // north east
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                rb.velocity = new Vector2(speed * .75f, speed * .75f);
            }
        }
        // detach from parent
        projectileStep1.transform.parent = null;
    }

    #endregion
    
}
