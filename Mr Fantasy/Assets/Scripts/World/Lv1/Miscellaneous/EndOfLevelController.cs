using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelController : MonoBehaviour
{

    public GameObject cameraController;
    public bool isTrigger;
    public bool deactive;
    public float timer;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {      
        if(deactive)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                cameraController.SetActive(false);
                timer = 0;
                deactive = false;
                isTrigger = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            isTrigger = true;
            cameraController.SetActive(true);
            deactive = true;
        }
    }
}
