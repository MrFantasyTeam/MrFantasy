using System.Collections.Generic;
using UnityEngine;

public class MoleculePooler : MonoBehaviour
{
    public static MoleculePooler SharedInstance;

    public List<GameObject> molecules;
    public GameObject[] objectsList;
    public GameObject objectToPool;
    private GameObject obj;
    public int randomNumber = 0;
    public int amountToPool;
    public float timer;
    public float startTime;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        molecules = new List<GameObject>();
        for (var i=0; i < amountToPool; i++)
        {
            foreach (var molecule in objectsList)
            {
                obj = Instantiate(molecule);
                obj.SetActive(false);
                molecules.Add(obj);
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= startTime)
        {
            while(molecules[randomNumber].activeInHierarchy)
            {
                randomNumber = Random.Range(0, objectsList.Length);
                objectToPool = molecules[randomNumber];
            }
            timer = 0;
        }
    }

    public GameObject GetPooledObject()
    {
        return molecules[randomNumber];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Level Charger Manager"))
        {
            Destroy(this.gameObject.GetComponent<MoleculePooler>());
        }
    }
}
