using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flauto : MonoBehaviour {
	public bool facingRight;
	public bool flip;
	public float speed;
	public float AttackTimer;
	public float AttackTimeDuration;
	public bool attack;
	public float projectile;
	Vector2 moveVec;
	Rigidbody2D rb;
	Animator anim;
	public GameObject ProjectileDx;
	public GameObject ProjectileSx;
	public Transform GenPosDx;
	public Transform GenPosSx;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		anim = this.GetComponent<Animator> ();
		moveVec = new Vector2 (speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
		rb.velocity = moveVec;

		Attack ();

		if (AttackTimer >= AttackTimeDuration) {
			Shoot ();
			AttackTimer = 0;
			Flip ();
		}
			
		
}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Edge") {
			attack = true;
			}

	}
	void Flip(){
		
		attack = false;
		anim.CrossFade ("FlautoMove", 0);
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		moveVec = moveVec * -1;
		rb.velocity = moveVec;	
			

	}
	void Attack(){
		if (attack) {
			AttackTimer += Time.deltaTime;
			rb.velocity = Vector2.zero;
			anim.CrossFade("FlautoAttacca", 0);
		}
	}
	void Shoot ()
	{
		
		projectile = 1;
		if (projectile == 1) {
			if (facingRight) {
				Instantiate (ProjectileDx, GenPosDx.transform.position, GenPosDx.transform.rotation);
			} else {
				Instantiate (ProjectileSx, GenPosSx.transform.position, GenPosSx.transform.rotation);

				projectile = 0;
			}
		}
  

	}
}
