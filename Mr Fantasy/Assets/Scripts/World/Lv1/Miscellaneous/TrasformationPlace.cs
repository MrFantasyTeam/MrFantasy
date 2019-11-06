using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TO BE REMOVED not USED
public class TrasformationPlace : MonoBehaviour {

	public GameObject Scene1;
	public GameObject Scene2;
	public GameObject Scene3;

	public GameObject pointOfGen;
	public GameObject MainCamera;
	private Camera camera;
	private GameObject player;
	public SpriteRenderer sprite;

	public Color color1;
	public Color color2;

	public bool StartCount;
	public float counter = 0;
	public float startScene3;
	public float ColorFade;

	// Use this for initialization
	void Start () {
		StartCount = false;
		camera = MainCamera.GetComponent<Camera> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			StartCount = true;
			player = other.gameObject;
			sprite = player.GetComponent<SpriteRenderer> ();
			//Destroy (other.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (StartCount) {
			Scene1.SetActive (false);
			Scene2.SetActive (true);
			ColorFade = Mathf.InverseLerp (0, 1, 5);
			camera.backgroundColor = Color.Lerp (color1, color2, ColorFade);
			counter += Time.deltaTime;
			if (counter >= startScene3) {
				Scene2.SetActive (false);
				Scene3.SetActive (true);
				//pointOfGen.SetActive (true);
				//Destroy (player);
				sprite.enabled = false;
                SceneManager.LoadScene(2);
            }
		}
	}


}
