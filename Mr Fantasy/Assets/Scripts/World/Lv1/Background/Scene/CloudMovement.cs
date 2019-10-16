using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {
	public float speed;
	public float endOfLevel;
	Rigidbody2D cloud;


	// Use this for initialization
	void Start () {
		cloud = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		cloud.velocity = new Vector2(speed, 0);
		if (this.transform.position.x > endOfLevel) {
			Destroy (this.gameObject);
		}


	}
}
