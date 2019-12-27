using System;
using System.Collections;
using System.Collections.Generic;
using Player.Gondola;
using UnityEngine;

public class Bagliore : MonoBehaviour
{
    private Animator anim;
    public GameObject player;
    public GondolaMovement gondolaMovement;
    private const string IsShotingName = "IsShooting";
    private const string PlayerName = "Player";
    private bool lit;
    public bool isShooting;
    private static readonly int IsShooting = Animator.StringToHash(IsShotingName);

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(PlayerName);
        gondolaMovement = player.GetComponent<GondolaMovement>();
        //isShooting = player.GetComponent<GondolaMovement>().isShooting;
    }

    // Update is called once per frame
    void Update()
    {
        isShooting = gondolaMovement.isShooting;
        if (isShooting)
        {
            //Debug.Log("Entered in the stete");
            anim.SetTrigger(IsShooting);
            Debug.Log("Triggered anim");
            player.GetComponent<GondolaMovement>().isShooting = false;
            Debug.Log("Setting false");
            isShooting = false;
        }
    }
}
