using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrFantasyGen : MonoBehaviour {


    public GameObject player;
    public GameObject pointOfInstance;

    public float activateTime;
    public float deactivateTime;
    private int counter;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        activateTime += Time.deltaTime;
        if (counter < 1)
        {
            Instantiate(player, pointOfInstance.transform.position, pointOfInstance.transform.rotation);
            counter++;   
        }
        
        if (activateTime > deactivateTime)
        {
            activateTime = 0;
            counter = 0;
            this.gameObject.SetActive(false);
        }


    }
}
