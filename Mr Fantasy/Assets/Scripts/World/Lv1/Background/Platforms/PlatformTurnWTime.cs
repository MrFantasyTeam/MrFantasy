using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Manages the "Platform Turn" within a defined time. **/
public class PlatformTurnWTime : MonoBehaviour {

	#region Settings Parameters

	public float Time0;
	public float Time1;
	public float Time2;
	public float Time3;
	public float Time4;
	public float speed1;
	public float speed2;
	public float PlatformRot = 0;

	#endregion
	

	void Start()
	{
		PlatformRot = this.transform.rotation.z  ;
	}
	
	void FixedUpdate ()
	{
		Time0 += Time.deltaTime;
		if (Time0 < Time1) 
			transform.Rotate (0, 0, speed1, Space.World);
		if (Time0 >= Time1 && Time0 < Time2 && Time0 < Time3 && Time0 < Time4) 
		{
			this.transform.eulerAngles = new Vector3 (0, 0, 180);
			transform.Rotate (0, 0, 0, Space.Self);
		}
		if (Time0 >= Time2 && Time0 < Time3 && Time0 < Time4) 
			transform.Rotate (0, 0, speed2, Space.World);
		if (Time0 >= Time3 && Time0 < Time4) 
		{ 
			transform.Rotate (0, 0, 0, Space.Self);
			this.transform.eulerAngles = new Vector3 (0, 0, 0);
		}
		if (Time0 >= Time4)
			Time0 = 0;
	}
}
