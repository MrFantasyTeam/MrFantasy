using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{

    public GameObject col1;
    public GameObject col2;
    public AudioSource audio;
    Animator anim;

    public float Time0 = 0;
    public float Time1;
    public float Time1dot2;
    public float Time2;
    public float Time3;
    public float Time4;

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

        if(Time0 >= Time1 && Time0 < Time1dot2)
        {
            if (audioactive)
                audio.Play();
            anim.SetBool("Fall", true);
        }
        if (Time0 >= Time1dot2 && Time0 < Time2)
        {
            Col1Disactive();
        }
        if (Time0 >= Time2 && Time0 < Time3)
        {
            Col2Disactive();
        }
        if (Time0 >= Time1dot2 && Time0 < Time2)
        {
            Col1Disactive();
        }
        if (Time0 >= Time3 && Time0 < Time4)
        {
            ActiveCol2();
        }
        if (Time0 >= Time4 )
        {
            ActiveCol1();
            Time0 = 0;
        }
    }
    
    //enable collider1
    void ActiveCol1()
    {
        col1.SetActive(true);
        anim.SetBool("Fall", false);
    }
    //enable collider2
    void ActiveCol2()
    {
        col2.SetActive(true);
        
    }
    //disable collider1 
    void Col1Disactive()
    {
        
        col1.SetActive(false);
        
    }
    //disable collider2
    void Col2Disactive()
    {
        col2.SetActive(false);
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
