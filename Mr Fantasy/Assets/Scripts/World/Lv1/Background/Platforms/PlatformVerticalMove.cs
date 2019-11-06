using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Manage platform vertical movement and behaviour. **/ 
public class PlatformVerticalMove : MonoBehaviour 
{
	#region Objects 

	Rigidbody2D rb;
	
	#endregion

	#region Settings Parameters

	public float Time0 = 0;
	public float Time1;
	public float Time2;
	public float speed;

	#endregion

	#region Default Methods

	// Use this for initialization
	void Start () 
	{
		rb = this.GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Time0 += Time.deltaTime;
		if (Time0 >= Time1) 
		{
			rb.velocity = new Vector2 (0, speed);
		}
		if (Time0 >= Time2) 
		{
			rb.velocity = new Vector2 (0, -speed);
			Time0 = 0;
		}
	}

	#endregion

	#region Custom Methods

	/** Destroy this object on trigger enter with "Roof". **/
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Roof") 
		{
			Destroy (this.gameObject);
		}
	}
	
	#endregion
}
