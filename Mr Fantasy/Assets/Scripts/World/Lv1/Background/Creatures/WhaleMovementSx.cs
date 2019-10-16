using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMovementSx: MonoBehaviour {

	public float time0 = 0;
	public float time1;
	public Rigidbody2D rb;
	public float speed; 

	void Start(){
		rb = this.GetComponent<Rigidbody2D>();
		}

	void Update(){
		rb.velocity = new Vector2(-speed, 0);
		time0 += Time.deltaTime;

		if(time0 >= time1){
			Destroy(this.gameObject);
		}
	}
}
