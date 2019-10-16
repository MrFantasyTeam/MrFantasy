using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceController : MonoBehaviour {

    public GameObject parte1;
    public GameObject parte2;
    public GameObject parte3;
 

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
