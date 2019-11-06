using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Destroy the player on trigger enter. **/
public class DestroyPlayer : MonoBehaviour
{
	/** Destroy the player on trigger enter with it. **/ 
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			Destroy (col.gameObject);
			Destroy (this.gameObject); 
		}
	}
}
