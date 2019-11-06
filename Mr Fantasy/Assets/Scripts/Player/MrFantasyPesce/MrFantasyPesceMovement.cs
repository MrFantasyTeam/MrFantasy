using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Script to control the player beahviour and movements. **/
public class MrFantasyPesceMovement : MonoBehaviour {

    #region Objects

    Rigidbody2D rb;
    Animator anim;
    public GameObject projectile;
    public Transform shootPoint;
    public Transform top;
    public Transform bottom;
    public Transform transform1;
    public SpriteRenderer sprite;
    
    #endregion

    #region Settings Parameters

    private int projectile_num = 1;
    public float speed; // player speed
    public float cooldown; // time between 1st shoot and 2nd shoot
    public float cooldownTimer;
    public float rotation;
    public float timer;

    #endregion

    #region Boolean Values

    public bool facingRight;
    public bool flipVertical = false;

    #endregion

    #region Default Methods

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D> ();
        anim = this.GetComponent<Animator> ();
        transform1 = this.GetComponent<Transform>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        cooldownTimer += Time.deltaTime;

        if(Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0)
            Rotation("null", "VerticalU");
        if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0)
            Rotation("null", "VerticalD");
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") > 0)
            Rotation("HorizontalR", "null");
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") < 0)
            Rotation("HorizontalL", "null");
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0)
            Rotation("HorizontalR", "VerticalU");
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") < 0)
            Rotation("HorizontalL", "VerticalU");
        if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") > 0)
            Rotation("HorizontalR", "VerticalD");
        if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") < 0)
            Rotation("HorizontalL", "VerticalD");
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("Move", false);
            rb.velocity = new Vector2(0, 0);
        }
            
        if ((Input.GetKey(KeyCode.C) && cooldownTimer > cooldown))
        {
            cooldownTimer = 0;
            anim.CrossFade("Attack", 0.1f);
            var bullet = Instantiate(projectile, shootPoint.transform.position, new Quaternion(0, 0, 0, 0));
        }
    }

    #endregion

    #region Custom Methods

    /** Rotate the player based on the axis values received. **/
    void Rotation(string axisH, string axisV)
    {
        anim.SetBool("Move", true);

        // east
        if (axisH.Equals("HorizontalR") && axisV.Equals("null"))
        {
            if (!Input.GetKey(KeyCode.LeftControl)) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            rb.velocity = new Vector2(speed, 0);
        }
        // north east
        if (axisH.Equals("HorizontalR") && axisV.Equals("VerticalU"))
        {
            if (!Input.GetKey(KeyCode.LeftControl)) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));                          
            rb.velocity = new Vector2(speed, speed);
        }
        // north
        if (axisH.Equals("null") && axisV.Equals("VerticalU"))
        {
            if (!Input.GetKey(KeyCode.LeftControl)) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));            
            rb.velocity = new Vector2(0, speed);
        }
        // west 
        if (axisH.Equals("HorizontalL") && axisV.Equals("null"))
        {
            if (!Input.GetKey(KeyCode.LeftControl)) transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            rb.velocity = new Vector2(-speed, 0);
        }
        // north west
        if (axisH.Equals("HorizontalL") && axisV.Equals("VerticalU"))
        {
            if (!Input.GetKey(KeyCode.LeftControl)) transform.rotation = Quaternion.Euler(new Vector3(0, 180, 45));
            rb.velocity = new Vector2(-speed*.8f, speed*.8f);
        }
        // south
        if (axisH.Equals("null") && axisV.Equals("VerticalD"))
        {
            if (!Input.GetKey(KeyCode.LeftControl)) transform.rotation = Quaternion.Euler(new Vector3(0, 180, 270));
            rb.velocity = new Vector2(0, -speed);
        }
        // south east
        if (axisH.Equals("HorizontalR") && axisV.Equals("VerticalD"))
        {
            if (!Input.GetKey(KeyCode.LeftControl)) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 315));
            rb.velocity = new Vector2(speed*.8f, -speed*0.8f);
        }
        // south west
        if (axisH.Equals("HorizontalL") && axisV.Equals("VerticalD"))
        {
            if (!Input.GetKey(KeyCode.LeftControl)) transform.rotation = Quaternion.Euler(new Vector3(0, 180, -45));
            rb.velocity = new Vector2(-speed*.8f, -speed*.8f);
        }
    }

    #endregion
}


