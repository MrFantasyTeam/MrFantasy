using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Manager for platform movement and behaviour. **/
public class PlatformMovement : MonoBehaviour {

	#region Objects

	Rigidbody2D rb;
	SpriteRenderer sprite;
	Vector2 moveVec;

	#endregion

	#region Settings Parameters

	private float Timer = 0;
	public float TimeToFlip;
	public float speed;

	#endregion

	#region Boolean Values

	bool facingRight;

	#endregion

	#region Default Methods

	// Use this for initialization
	void Start () 
	{
		rb = this.GetComponent<Rigidbody2D> ();
		sprite = this.GetComponent<SpriteRenderer>();
		moveVec = new Vector2 (speed, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate (moveVec * Time.deltaTime);
		//rb.velocity = moveVec;
		Timer += Time.deltaTime;
		if (Timer >= TimeToFlip) 
		{
			Flip ();
			Timer = 0;
		}
	}

	#endregion

	#region Custom Methods

	/** Flip the platform based on the movement direction and the value of facingRight. **/
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

	/** On collision enter set the player as child of platform, to avoid player from falling
	 * while the platform is moving.
	 */
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player") 
		{
			collision.transform.SetParent (transform);
		}
	}

	/** On collision exit detach and unset the player as child of this platform. **/
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player") 
		{
			collision.transform.SetParent (null);
		}
	}

	#endregion
}
