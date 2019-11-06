using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Manager of generator for "Platform Fall". **/
public class PlatformFallGenerator : MonoBehaviour 
{
	#region Objects

	public GameObject platform;

	#endregion

	#region Settings Parameters

	public float Time0 = 0;
	public float Time1;
	public int num = 0;

	#endregion
	
	// Update is called once per frame
	void Update () 
	{
		Time0 += Time.deltaTime;
		if (Time0 >= Time1 && num == 0) 
		{
			num++;
			Instantiate (platform, this.transform.position, this.transform.rotation);
			Time0 = 0;
			num = 0;
		} 
	}
}
