using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

	Rigidbody2D rb;
    SpriteRenderer sprite;
    Vector2 moveVec;

    float Timer = 0;
	public float TimeToFlip;
	public float speed;
	bool facingRight;


	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
        sprite = this.GetComponent<SpriteRenderer>();
		moveVec = new Vector2 (speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (moveVec * Time.deltaTime);
		//rb.velocity = moveVec;
		Timer += Time.deltaTime;
		if (Timer >= TimeToFlip) {
			Flip ();
			Timer = 0;
		}
	}

	void Flip(){
        facingRight = !facingRight;
		moveVec = moveVec * -1;
		rb.velocity = moveVec;
        // flip only the sprite renderer
        if (facingRight)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			collision.transform.SetParent (transform);
		}
	}

	private void OnCollisionExit2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			collision.transform.SetParent (null);
		}
	}
}
