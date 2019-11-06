using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Scipt to control the movement and behaviour of a molecule. **/
public class Molecule : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Level Charger Manager")
        {
            Destroy(this.gameObject);
        }
    }
}
