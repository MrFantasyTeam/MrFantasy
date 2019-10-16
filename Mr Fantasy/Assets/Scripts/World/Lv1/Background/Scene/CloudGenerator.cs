using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour {

	public GameObject Cloud;
	public float timeLapse;
	public float timeToGen;
	public int num = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		timeLapse += Time.deltaTime;
		if (timeLapse > timeToGen) {
			Instantiate (Cloud, this.transform.position, this.transform.rotation);
			timeLapse = 0;
			}
	}
}
