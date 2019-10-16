using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private EnemiesGeneralBehaviour parent;
    private void Start()
    {
        parent = GetComponentInParent<EnemiesGeneralBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Spotted player");
            parent.spottedPlayer = true;
            parent.player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("UnSpotted player");
            parent.spottedPlayer = false;
            parent.player = null;
        }
    }
}
