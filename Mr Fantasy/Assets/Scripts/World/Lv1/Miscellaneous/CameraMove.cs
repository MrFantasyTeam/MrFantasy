using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Manage camera movement and behaviour. This camera move always in the right direction,
 * adjusting its vertical direction based on the tag of the colliders encoutered.
 **/
public class CameraMove : MonoBehaviour {

	#region Objects 

	public GameObject GeneratoreMobile;
	public GameObject GeneratoreMobilePos;
	public GameObject GeneratoreFisso;

	#endregion

	#region Settings Parameters

	public Vector3 respawnPoint;
	int GenNum = 0;
	public float speed;
	public float speed1;
	public float counter;
	public float counterGen;
	public float counterEnd;
	public float genTime = 0;
	public float genTimeOff;

	#endregion

	#region Boolean Values

	public bool start = false; 
	public bool cameraup = false;
	public bool cameradown = false;
	public bool playerDead = false;
	public bool reset = false;
	public bool firstcheckpoint = false;

	#endregion

	#region Default Methods

	void FixedUpdate() 
	{
		// camera goes right
		if(cameraup == false && cameradown == false) 
			CameraRight ();
		// camera goes up
		else if(cameraup)
			CameraUp ();
		// camera goes down
		else if(cameradown) 
			CameraDown ();
		// check if player is dead and reset camera to checkpoint position
		if((playerDead || reset) && firstcheckpoint) // first checkpoint reached
		{ 
			this.transform.position = respawnPoint;
			if (counterGen < 1)
			{
				counterGen++;
				Instantiate(GeneratoreMobile, GeneratoreMobilePos.transform.position, GeneratoreMobilePos.transform.rotation);                
			}
			genTime += Time.deltaTime;

			if (genTime > genTimeOff)
			{
				speed = speed1;
				CameraRight();
				playerDead = false;
				genTime = 0;
				counterGen = 0;
			}
		}
		else if ((playerDead || reset) && !firstcheckpoint) // first checkpoint not reached
		{
			this.transform.position = respawnPoint;
			GeneratoreFisso.SetActive(true);
			genTime += Time.deltaTime;

			if (genTime > genTimeOff)
			{
				speed = speed1;
				CameraRight();
				playerDead = false;
				genTime = 0;
			}
		}
	}

	#endregion

	#region Custom Methods

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player") 
		{
			Destroy(col.gameObject);
			playerDead = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "PointOfTrans") 
			start = true; // camera has reached the transformation point
		else if (other.gameObject.tag == "CameraUp") {
			cameraup = true;
			cameradown = false;
		}
		else if (other.gameObject.tag == "CameraDown") {
			cameradown = true;
			cameraup = false;
		}
		else if (other.gameObject.tag == "CameraRight") {
			cameradown = false;
			cameraup = false;
		}
		else if (other.gameObject.tag == "FalseCheckpoint")
			respawnPoint = new Vector3 (other.transform.position.x, other.transform.position.y, -10);
		else if (other.gameObject.tag == "Checkpoint")
		{
			firstcheckpoint = true;
			respawnPoint = new Vector3(other.transform.position.x, other.transform.position.y, -10);
		}
	}

	void CameraUp()
	{
		this.transform.Translate (new Vector2 (speed, speed/4));
	}
	
	void CameraDown()
	{
		this.transform.Translate (new Vector2 (speed, -speed/4));
	}
	
	void CameraRight()
	{
		this.transform.Translate (new Vector2 (speed, 0));
	}

	#endregion
}
