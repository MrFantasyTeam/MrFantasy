using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {
	public float time0 = 0;
	public float Time1;
	public Camera MainCamera;

	// Use this for initialization
	void Start () {
		MainCamera.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		time0 += Time.deltaTime;
		if (time0 > Time1) {
			MainCamera.enabled = true;
		}
	}
}
