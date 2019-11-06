using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Script to control the right movement and behaviour of whale. **/ 
public class WhaleMovementDx : MonoBehaviour 
{
	#region Objects

	public Rigidbody2D rb;

	#endregion

	#region Settings Parameters

	public float time0 = 0;
	public float time1;
	public float speed; 

	#endregion

	void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		rb.velocity = new Vector2(speed, 0);
		time0 += Time.deltaTime;
		if(time0 >= time1)
		{
			Destroy(this.gameObject);
		}
	}
}
