using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparaControllerCreatura2 : MonoBehaviour {
	private bool startGame = true;
	float time0 = 0;
	float time1 = 2.5f;
	public Rigidbody2D MrFantasybody;
	Animator anim;
	PlayerMovementCreatura2 playerMovement;
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		MrFantasybody = this.GetComponent<Rigidbody2D> ();
		playerMovement = this.GetComponent<PlayerMovementCreatura2> ();
		playerMovement.enabled = false;
	}
	void FixedUpdate(){
		if (startGame) {
			MrFantasybody.velocity = new Vector2 (15, 20);
			startGame = false;
			anim.CrossFade ("CreaturaSparato", 0.01f);
		}
	}
	// Update is called once per frame
	void Update () {
		time0 += Time.deltaTime;
		if (startGame == false && time0>time1) {
			playerMovement.enabled = true;
			this.enabled = false;
		}
	}
}
