using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Script to control the player behaviour **/
public class PlayerMovement : MonoBehaviour {

    #region Objects

    Animator anim;
    public LayerMask groundLayer;
    public Rigidbody2D MrFantasybody;
    public Transform GroundCheck;
    public Transform LevelChangerPosition;
    public AudioSource audio1;
    public SpriteRenderer sprite;
    public GameObject animStartPosition;
    public Transform originalPosition;

    #endregion

    #region Settings Parameters

    public float xTarget, yTarget;
    public float speed;
    public float JumpHeight;
    public float groundCheckRadius;
    Vector2 moveVec;

    #endregion

    #region Boolean Values

    public bool grounded;
    public bool DoubleJumped;
    public bool idle;
    public bool facingRight;
    public bool reachThePosition;
    public bool disablePlayer;
    public bool stopMoving;

    #endregion


    #region Default Methods

    void Start () {
		anim = this.GetComponent<Animator> ();
		MrFantasybody = this.GetComponent<Rigidbody2D> ();
		moveVec = new Vector2 (speed, 0);
        sprite = this.GetComponent<SpriteRenderer>();
	}
     
    void FixedUpdate()
    {
        if (reachThePosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, animStartPosition.transform.position, 30 * Time.deltaTime);
        }

        if (disablePlayer==false)
        {// check if the player is grounded
            grounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundLayer);

            float HorizontalMovement = Input.GetAxis("Horizontal");

            //if grounded disable double jump
            if (grounded)
            {
                DoubleJumped = false;
            }

            if (HorizontalMovement == 0) anim.SetBool("Move", false);

            if (Input.GetAxis("Horizontal") > 0)
            {
                anim.SetBool("Move", true);
                if (!facingRight)
                {
                    MrFantasybody.velocity = new Vector2(speed, MrFantasybody.velocity.y);
                }
                else
                {
                    Flip();
                    MrFantasybody.velocity = new Vector2(-speed, MrFantasybody.velocity.y);
                }
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                anim.SetBool("Move", true);
                if (facingRight)
                {

                    MrFantasybody.velocity = new Vector2(-speed, MrFantasybody.velocity.y);
                }
                else
                {
                    Flip();

                    MrFantasybody.velocity = new Vector2(speed, MrFantasybody.velocity.y);
                }
            }
        }
    }

    private void Update()
    {
        // check if the player movement has been disabled by external script (PlayerControlDisable.cs)
        if(disablePlayer == false)
        {
            float HorizontalMovement = Input.GetAxis("Horizontal");

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded == true)
            {
                //audio1.Play();
                //anim.SetBool("Idle", false); //the player is moving, disable idle
                //anim.SetBool("Grounded", grounded); //check if the player is grounded

                if (HorizontalMovement == 0)
                {
                    MrFantasybody.velocity = new Vector2(0, JumpHeight);
                }
                else
                {
                    if (HorizontalMovement > 0)
                    {
                        MrFantasybody.velocity = new Vector2(MrFantasybody.velocity.x, JumpHeight);
                    }
                    if (HorizontalMovement < 0)
                    {
                        MrFantasybody.velocity = new Vector2(-MrFantasybody.velocity.x, JumpHeight);
                    }
                }
                anim.CrossFade("Jump", 0.000001f);
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !DoubleJumped && !grounded)
            {
                if (HorizontalMovement == 0)
                {
                    MrFantasybody.velocity = new Vector2(0, JumpHeight);
                }
                else
                {
                    MrFantasybody.velocity = new Vector2(MrFantasybody.velocity.x, JumpHeight);
                }
                anim.CrossFade("DJump", 0.2f);
                DoubleJumped = true;
            }
        }      
    }

    #endregion

    #region Custom Methods

    /** Flip the player based on the movement direction and the facing direction. **/
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    /** Move automatically the player to a set position, called when disablePlayer is true. **/ 
    public void ReachPosition(GameObject animStartObject)
    {
        anim.SetTrigger("KeepJumping");
        MrFantasybody.velocity = new Vector2(0, 0);
        disablePlayer = true;
        reachThePosition = true;
        animStartPosition = animStartObject;
    }

    #endregion
    
}
