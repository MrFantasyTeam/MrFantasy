using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Manage the generation of the clouds in the background. **/
public class CloudGenerator : MonoBehaviour {

	#region Objects 

	public GameObject Cloud;

	#endregion

	#region Settings Parameters

	public float timeLapse;
	public float timeToGen;
	public int num = 0;

	#endregion

	// Update is called once per frame
	void FixedUpdate()
	{
		timeLapse += Time.deltaTime;
		if (timeLapse > timeToGen) 
		{
			Instantiate (Cloud, this.transform.position, this.transform.rotation);
			timeLapse = 0;
		}
	}
}
