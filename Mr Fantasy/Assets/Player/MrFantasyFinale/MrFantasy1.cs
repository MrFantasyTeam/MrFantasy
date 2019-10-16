using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrFantasy1: MonoBehaviour {

	Animator anim;
	public LayerMask groundLayer;
	public Rigidbody2D MrFantasybody;
	public Transform GroundCheck;

	public float speed;
	public float JumpHeight;
	public float groundCheckRadius;


	public bool grounded;
	public bool DoubleJumped;
	public bool idle;

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
	void Update ()
	{   
		float HorizontalMovement = Input.GetAxis ("Horizontal");

		//check if the player is moving
		if (HorizontalMovement == 0) {
			anim.SetBool ("Idle", true);
		}

		//if grounded disable double jump
		if (grounded)
			DoubleJumped = false;

		//move input
		if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow))) {
			anim.SetBool ("Idle", false); //the player is moving, disable idle
			anim.SetBool("Grounded",grounded); //check if the player is grounded
			anim.SetTrigger ("Move dx");
			MrFantasybody.velocity = new Vector2 (speed, MrFantasybody.velocity.y);
		}

		if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow))){
			anim.SetBool ("Idle", false); //the player is moving, disable idle
			anim.SetBool("Grounded",grounded); //check if the player is grounded
			anim.SetTrigger ("Move sx");
			MrFantasybody.velocity = new Vector2 (-speed, MrFantasybody.velocity.y);
		}
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded == true) {
			anim.SetBool ("Idle", false); //the player is moving, disable idle
			anim.SetBool("Grounded",grounded); //check if the player is grounded
			if (HorizontalMovement >= 0) {
				anim.CrossFade ("Jumpdx",0.1f);
			}
			else{
				anim.CrossFade ("Jumpsx",0.1f);
			}
			MrFantasybody.velocity = new Vector2 (MrFantasybody.velocity.x, JumpHeight);

		}
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !DoubleJumped && !grounded) {

			if (HorizontalMovement >= 0) {
				anim.CrossFade ("Djumpdx", 0.1f);
			}
			else{
				anim.CrossFade ("Djumpsx", 0.1f);
			}
			MrFantasybody.velocity = new Vector2 (MrFantasybody.velocity.x, JumpHeight);

			anim.SetBool ("Idle", false); //the player is moving, disable idle
			anim.SetBool("Grounded",grounded); //check if the player is grounded
			DoubleJumped = true;

		}
	}


}
