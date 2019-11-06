using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Script to control the generation of whale. **/
public class WhaleGen : MonoBehaviour {

	public float time0=0;
	public float time1;
	public GameObject Whale;
	public int num = 0;

	void Update()
	{
		time0 += Time.deltaTime;
		if(time0 >= time1 && num == 0)
		{
			num++;
			Instantiate (Whale, this.transform.position, this.transform.rotation);
			num=0;
			time0 = 0;
		}
	}
}
