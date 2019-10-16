using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour {

	public MovieTexture movie;
	private float x; 
	RawImage ThisVideo;

	// Use this for initialization
	void Start () {
		ThisVideo = this.GetComponent<RawImage> ();
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		movie.Play ();
		x = 0;
		
	}
	void Update(){
		x += Time.deltaTime;
		if (x > 10) {
			ThisVideo.enabled = false;
		}
		Debug.Log ("disabilitato");
	}
}
