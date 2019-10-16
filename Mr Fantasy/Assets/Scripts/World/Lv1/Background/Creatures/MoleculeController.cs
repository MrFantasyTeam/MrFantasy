using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeController : MonoBehaviour
{
    private ObjectPooler objectPooler;
    public GameObject molecule;
    public Transform[] poolPositions;
    public bool notStarted = true;
    public float timer;
    public float startTime;
    public int randomNumber = 0;
    public bool pooling = false;
    // Start is called before the first frame update
    void Start()
    {
       objectPooler = ObjectPooler.SharedIntance;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        while(notStarted)
        {
            PoolMolecule();
            if(molecule != null)
                notStarted = false;
        }

        if(timer>=startTime && !pooling)
        {
            randomNumber = Random.Range(0, poolPositions.Length);
            timer = 0;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        pooling = true;
        if(other.gameObject.CompareTag("Molecule"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("put molecule in the pool again");
            PoolMolecule();
        }
        pooling = false;
    }

    private void PoolMolecule()
    {
        molecule = ObjectPooler.SharedIntance.SpawnFromPool("molecule", transform.position, Quaternion.identity);

        if (molecule == null) return;
        molecule.transform.position = poolPositions[randomNumber].transform.position;
        molecule.transform.rotation = poolPositions[randomNumber].transform.rotation;
        molecule.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Level Charger Manager"))
        {
            Destroy(this.gameObject);
        }
    }
}
