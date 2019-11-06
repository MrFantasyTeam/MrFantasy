using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** This script activates and deactivate zones of the scene to improve perfomrance,
 * based on which zone of the scene the player is in.
 */
public class PerformanceController : MonoBehaviour {

    #region Objects 

    public GameObject parte1;
    public GameObject parte2;
    public GameObject parte3;

    #endregion

    /** On trigger enter activate the new zone and deactivate the previous. **/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Load1")
        {
            parte2.SetActive(true);
        }
        if (collision.gameObject.tag == "Load2")
        {
            parte1.SetActive(false);
        }
        if (collision.gameObject.tag == "Load3")
        {
            parte3.SetActive(true);
        }
        if (collision.gameObject.tag == "Load4")
        {
            parte2.SetActive(false);
        }
    }
}
