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
    private const string IsShotingName = "Bagliore";
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
    void FixedUpdate()
    {
        isShooting = gondolaMovement.isShooting;
        if (isShooting)
        {
            anim.CrossFade(IsShotingName, 0.1f);
            gondolaMovement.isShooting = false;
            isShooting = false;
        }
    }
}
