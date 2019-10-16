using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallEasy : MonoBehaviour {

    public AudioSource suono;
    Animator anim;

    public float Time0 = 0;
    public float Time1;
    public float Time1dot2;
    public float Time2;

    public bool audioactive = false;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Time0 += Time.fixedDeltaTime;

        if (Time0 >= Time1 && Time0 < Time1dot2)
        {
            if (audioactive)
                suono.Play();
            anim.SetBool("Fall", true);
        }
        if (Time0 >= Time2)
        {
            anim.SetBool("Fall", false);
            Time0 = 0;
        }

    }

    //enable audio when in camera view
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "MainCamera")
            audioactive = true;
    }
    //disable audio when not in camera view
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "MainCamera")
            audioactive = false;
    }
}


