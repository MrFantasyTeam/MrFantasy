using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Manage the cloud movement and behaviour. **/
public class CloudMovement : MonoBehaviour {

	#region Objects

	Rigidbody2D cloud;

	#endregion

	#region Settings Parameters

	public float speed;
	public float endOfLevel;

	#endregion
	
	// Use this for initialization
	void Start () 
	{
		cloud = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		cloud.velocity = new Vector2(speed, 0);
		if (this.transform.position.x > endOfLevel) 
		{
			Destroy (this.gameObject); 
		}
	}
}
