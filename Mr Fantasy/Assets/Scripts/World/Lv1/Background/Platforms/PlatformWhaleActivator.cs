using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWhaleActivator : MonoBehaviour {

	public GameObject Gen1;
	//public GameObject Gen2;

	private void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			Gen1.SetActive (true);
			//Gen2.SetActive (true);
		}
	}

	private void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			Gen1.SetActive (false);
			//Gen2.SetActive (false);
		}
	}
}
