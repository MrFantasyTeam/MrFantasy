using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Manages the whale entry and exit points. **/
public class WhaleEntry : MonoBehaviour 
{
	Animator anim;

	void Start()
	{
		anim = this.GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Whale") 
		{
			anim.SetBool ("Entry", true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Whale") 
		{
			anim.SetBool ("Entry", false);
		}
	}
}
