using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {
	
	Rigidbody2D player;
	Vector2 moveForce;

	public float speed;
	public bool isTrigger;

	//check if the player enter the MoveTerrain
	private void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			player = col.GetComponent<Rigidbody2D> ();
			isTrigger = true;
		}
	}

	//check if the player exit the MoveTerrain
	private void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			isTrigger = false;
		}
	}

	//if the player is on the terrain, apply movement
	void Update(){
		if (isTrigger) {
			
			moveForce = new Vector2 (speed, 0);
			player.AddForce (moveForce);
		}
	}

}
