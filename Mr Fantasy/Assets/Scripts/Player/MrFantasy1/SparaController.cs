using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Script to control player shot movement at the beginning of the first level. **/
public class SparaController : MonoBehaviour {

	#region Objects

	public Rigidbody2D MrFantasybody;
	Animator anim;
	PlayerMovement playerMovement;

	#endregion

	#region Settings Parameters

	public float time0 = 0;
	public float time1 = 2.5f;

	#endregion

	#region Boolean Values

	private bool startGame = true;

	#endregion
	
	void Start () {
		anim = this.GetComponent<Animator> ();
		MrFantasybody = this.GetComponent<Rigidbody2D> ();
		playerMovement = this.GetComponent<PlayerMovement> ();
		playerMovement.enabled = false;
	}
	
	void FixedUpdate()
	{
		if (startGame) 
		{
			MrFantasybody.velocity = new Vector2 (15, 20);
			startGame = false;
			anim.CrossFade ("CreaturaSparato", 0.0f);
		}
	}

	void Update () 
	{
		time0 += Time.deltaTime;
		if (startGame == false && time0>time1) {
			playerMovement.enabled = true;
            anim.SetBool("StartGame", true);
			this.enabled = false;
		}
	}
}
