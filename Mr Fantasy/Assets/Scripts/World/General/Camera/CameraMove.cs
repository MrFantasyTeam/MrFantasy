using UnityEngine;

namespace World.General.Camera
{
	public class CameraMove : MonoBehaviour {

		public GameObject GeneratoreMobile;
		public GameObject GeneratoreMobilePos;
		public GameObject GeneratoreFisso;

		int GenNum = 0;
		public float speed;
		public float speed1;
		public float counter;
		public float counterGen;
		public float counterEnd;
		public float genTime = 0;
		public float genTimeOff;

		public bool start = false; 
		public bool cameraup = false;
		public bool cameradown = false;
		public bool playerDead = false;
		public bool reset = false;
		public bool firstcheckpoint = false;

		//public Transform cpPosition;
		public Vector3 respawnPoint;
		//public GameObject Player;


		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void FixedUpdate () {
			// camera goes right
			if (cameraup == false && cameradown == false) {
				CameraRight ();
			}
			// camera goes up
			if(cameraup){
				CameraUp ();
			}
			// camera goes down
			if (cameradown) {
				CameraDown ();
			}
			// check if player is dead and reset camera to checkpoint position
			if ((playerDead || reset) && firstcheckpoint) // first checkpoint reached
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
			if ((playerDead || reset) && !firstcheckpoint) // first checkpoint not reached
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

		void OnTriggerExit2D(Collider2D col){
			if (col.gameObject.CompareTag("Player")) {
				//Player = col.gameObject;
				Destroy(col.gameObject);
				playerDead = true;
				//col.transform.position = respawnPoint; // assign player respawnpoint coordinates
			}
			
		}
		void OnTriggerEnter2D(Collider2D other){
			if (other.gameObject.CompareTag("PointOfTrans")) {
				start = true; // camera has reached the transformation point
			}
			if (other.gameObject.CompareTag("CameraUp")) {
				cameraup = true;
				cameradown = false;
			}
			if (other.gameObject.CompareTag("CameraDown")) {
				cameradown = true;
				cameraup = false;
			}
			if (other.gameObject.CompareTag("CameraRight")) {
				cameradown = false;
				cameraup = false;
			}
			if (other.gameObject.CompareTag("FalseCheckpoint"))
			{
				respawnPoint = new Vector3 (other.transform.position.x, other.transform.position.y, -10);
			}
			if (other.gameObject.CompareTag("Checkpoint"))
			{
				firstcheckpoint = true;
				respawnPoint = new Vector3(other.transform.position.x, other.transform.position.y, -10);
			}
		}
		
		void CameraUp(){
			this.transform.Translate (new Vector2 (speed, speed/4));
		}
		void CameraDown(){
			this.transform.Translate (new Vector2 (speed, -speed/4));
		}
		void CameraRight(){
			this.transform.Translate (new Vector2 (speed, 0));
		}
	}
}
