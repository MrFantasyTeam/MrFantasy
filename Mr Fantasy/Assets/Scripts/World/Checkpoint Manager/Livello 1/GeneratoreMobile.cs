using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratoreMobile : MonoBehaviour {

    public GameObject player;
    public GameObject pointOfInstance;
    private Rigidbody2D rb;
    
    public float shootTime;
    public float activateTime;
    public float deactivateTime;
    private int counter;
    

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
          
    }
	
	// Update is called once per frame
	void FixedUpdate () {
       
        activateTime += Time.deltaTime;
        
        if (activateTime > shootTime && counter < 1)
        {
            Instantiate(player, pointOfInstance.transform.position, pointOfInstance.transform.rotation);
            counter++;
            rb.velocity = new Vector2(-1.2f, 0);
        }
        
        if (activateTime > deactivateTime)
        {
            activateTime = 0;
            counter = 0;
            Destroy(this.gameObject);
        }

    }
}
