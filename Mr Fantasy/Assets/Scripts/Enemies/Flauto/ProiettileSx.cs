using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProiettileSx : MonoBehaviour {

	Rigidbody2D rigidbody;
	public float speed;
	public float Time0;
	public float Time1;
	// Use this for initialization
	void Start () {
		rigidbody = this.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		rigidbody.velocity = new Vector2 (speed, 0);
		Time0 += Time.deltaTime;
		if (Time0 > Time1) {
			Destroy (this.gameObject);
		}
	}
}
