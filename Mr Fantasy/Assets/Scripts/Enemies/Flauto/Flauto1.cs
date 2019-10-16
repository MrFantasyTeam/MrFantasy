using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flauto1 : MonoBehaviour {

	Rigidbody2D rigidbody;
	Animator anim;
	public Transform GenDx;
	public Transform GenSx;
	public GameObject projectiledx;
	public GameObject projectilesx;
	public float projNum;
	public float speed;
	public float Time0;
	public float Time1;
	public float Time2;
	public float AttackTime1;
	public float AttackTime2;
	public bool AttaccaDx;
	public bool AttaccaSx;


	// Use this for initialization
	void Start () {
		rigidbody = this.GetComponent<Rigidbody2D> ();
		anim = this.GetComponent<Animator> ();
		MoveDx ();
	}
	
	// Update is called once per frame
	void Update () {

		Time0 += Time.deltaTime;
		if (Time0 >= Time1) {
			AttackDx ();
			AttackTime1 += Time.deltaTime;
			Time0 = 0;

			if (AttackTime1 >= AttackTime2) {
				MoveSx ();
				AttackTime1 = 0;
				Time2 += Time.deltaTime;
				if (Time2 >= Time1) {
					AttackSx ();
					Time2 = 0;
					AttackTime1 += Time.deltaTime;
					if (AttackTime1 >= AttackTime2) {
						MoveDx ();
						AttackTime1 = 0;
					}
				}
			}
		}
	}

		/*Time0 += Time.deltaTime;
		if (Time0 >= Time1) {
			AttackDx ();
			//ShootDx ();
			AttackTime1 += Time.deltaTime;
			if (AttackTime1 >= AttackTime2) {
				MoveSx ();
				Time0 = 0;
				AttackTime1 = 0;
				Time2 += Time.deltaTime;
			}
		}

		if (Time2 >= Time1) {
			AttackSx ();
			anim.SetBool ("AttaccaSx", false);
			anim.SetBool ("AttaccaDx", false);
			//ShootSx ();
			AttackTime1 += Time.deltaTime;
			if (AttackTime1 >= AttackTime2) {
				MoveDx ();
				Time2 = 0;
				AttackTime1 = 0;
			}
		}*/
		//MoveDx ();
		/*anim.SetTrigger ("MoveDx");
		if (Time0 < Time1) {
			anim.SetBool ("AttaccaDx", true);
			//projNum = 1;
			if (projNum >= 1) {
				
				Instantiate (projectilesx, GenDx.transform.position, GenDx.transform.rotation);

			}
			projNum=0;
			rigidbody.velocity = new Vector2 (speed, 0);
			AttackTime1 += Time.deltaTime;
			if (AttackTime1 >= AttackTime2) {
				AttackTime1 = 0;
				anim.SetBool ("AttaccaDx", false);
				anim.SetTrigger ("MoveDx");
				//rigidbody.velocity = new Vector2 (speed, 0);
			}



		}
		Time0 += Time.deltaTime;
		if (Time0 >= Time1) {
			anim.SetBool ("AttaccaSx", true);
			if (projNum < 1) {
				Instantiate (projectiledx, GenSx.transform.position, GenSx.transform.rotation);

			}
			projNum = 1;
			rigidbody.velocity = new Vector2 (-speed, 0);
			AttackTime1 += Time.deltaTime;
			if (AttackTime1 >= AttackTime2) {
				AttackTime1 = 0;
				anim.SetBool ("AttaccaSx", false);
				anim.SetTrigger ("MoveSx");
				//rigidbody.velocity = new Vector2 (-speed, 0);
			}

			Time2 += Time.deltaTime;
			if (Time2 >= Time1) {
				Time0 = 0;
				Time2 = 0;
			}
		}*/
	
		void MoveDx(){
		anim.SetTrigger ("MoveDx");
		rigidbody.velocity = new Vector2 (speed, 0);
		//AttaccaDx = false;
		//AttaccaSx = false;
		anim.SetBool ("AttaccaSx", false);
		anim.SetBool ("AttaccaDx", false);
		}
		void MoveSx(){
		anim.SetTrigger ("MoveSx");
		rigidbody.velocity = new Vector2 (-speed, 0);
		//AttaccaDx = false;
		//AttaccaSx = false;
		anim.SetBool ("AttaccaSx", false);
		anim.SetBool ("AttaccaDx", false);
		}
		void AttackDx(){
		//AttaccaDx = true;
		//AttaccaSx = false;
		anim.SetBool ("AttaccaDx", true);
		anim.SetBool ("AttaccaSx", false);
		rigidbody.velocity = new Vector2 (0, 0);
	}
		void AttackSx(){
		//AttaccaSx = true;
		//AttaccaDx = false;
		anim.SetBool ("AttaccaSx", true);
		anim.SetBool ("AttaccaDx", false);
		rigidbody.velocity = new Vector2 (0, 0);
	}
	void ShootDx(){
	}
	void ShootSx(){
	}
}
