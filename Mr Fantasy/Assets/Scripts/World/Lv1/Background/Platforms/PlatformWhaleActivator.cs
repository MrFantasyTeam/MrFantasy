using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Manage the activation of whale generator. **/
public class PlatformWhaleActivator : MonoBehaviour {

	public GameObject Gen1;

	/** On trigger enter with player activate the related whale generator. **/
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
			Gen1.SetActive (true);
	}

	/** On trigger exit with the player deactivate the related whale generator. **/
	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
			Gen1.SetActive (false);
	}
}
