using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCreatura2 : MonoBehaviour {

	public bool facingRight;


	Animator anim;
	public LayerMask groundLayer;
	public Rigidbody2D MrFantasybody;
	public Transform GroundCheck;

	public bool grounded;
	public bool DoubleJumped;
	public bool idle;

	public float speed;
	public float JumpHeight;
	public float groundCheckRadius;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		MrFantasybody = this.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		// check if the player is grounded
		grounded = Physics2D.OverlapCircle (GroundCheck.position, groundCheckRadius, groundLayer);
	}
	
	// Update is called once per frame
	void Update () {

		float HorizontalMovement = Input.GetAxis ("Horizontal");

		//check if the player is moving
		if (HorizontalMovement == 0) {
			anim.SetBool ("Idle", true);
		} else {
			anim.SetBool ("Idle", false);
		}

		//if grounded disable double jump
		if (grounded)
			DoubleJumped = false;


		if (Input.GetAxis ("Horizontal") > 0 ) {
				if (!facingRight) {
					anim.SetTrigger ("Move");
					MrFantasybody.velocity = new Vector2 (speed, MrFantasybody.velocity.y);
				} 
				else {
					Flip ();
					anim.SetTrigger ("Move");
					MrFantasybody.velocity = new Vector2 (-speed, MrFantasybody.velocity.y);
				}
			}
		if (Input.GetAxis ("Horizontal") < 0 ) {
				if (facingRight) {
					anim.SetTrigger ("Move");
					MrFantasybody.velocity = new Vector2 (-speed, MrFantasybody.velocity.y);
				} 
				else {
					Flip ();
					anim.SetTrigger ("Move");
					MrFantasybody.velocity = new Vector2 (speed, MrFantasybody.velocity.y);
				}
			}
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded == true) {

			anim.SetBool ("Idle", false); //the player is moving, disable idle
			anim.SetBool("Grounded",grounded); //check if the player is grounded

			anim.CrossFade ("Jump",0.1f);

			MrFantasybody.velocity = new Vector2 (MrFantasybody.velocity.x, JumpHeight);

		}

		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !DoubleJumped && !grounded) {
		    
			anim.CrossFade ("DJump", 0.1f);
			MrFantasybody.velocity = new Vector2 (MrFantasybody.velocity.x, JumpHeight);

			anim.SetBool ("Idle", false); //the player is moving, disable idle
			anim.SetBool("Grounded",grounded); //check if the player is grounded
			DoubleJumped = true;

		}


	}

	void Flip(){

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
