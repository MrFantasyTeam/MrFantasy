using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiattaformaOnTouch : MonoBehaviour {

	public AudioSource audio;
	public bool active;

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			active = true;
			audio.Play ();
		}	
	}


}
