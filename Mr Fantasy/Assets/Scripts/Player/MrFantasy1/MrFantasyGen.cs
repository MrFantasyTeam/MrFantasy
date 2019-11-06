using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** Script to generate the player at the beginning of the level **/
public class MrFantasyGen : MonoBehaviour {

    #region Objects

    public GameObject player;
    public GameObject pointOfInstance;

    #endregion

    #region Settings Parameters
    
    public float activateTime;
    public float deactivateTime;
    private int counter;

    #endregion
    
    void Start()
    {

    }

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
