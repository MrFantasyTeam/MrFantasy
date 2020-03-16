//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
///** Controls the spawning of the molecules. **/
//public class MoleculeController : MonoBehaviour
//{
//    #region Objects
//
//    private ObjectPooler objectPooler;
//    public GameObject molecule;
//    public Transform[] poolPositions;
//
//    #endregion
//
//    #region Settings Parameters
//
//    public float timer;
//    public float startTime;
//    public int randomNumber = 0;
//
//    #endregion
//
//    #region Boolean Values
//
//    public bool notStarted = true;
//    public bool pooling = false;
//
//    #endregion
//
//    #region Default Methods
//
//    // Start is called before the first frame update
//    void Start()
//    {
//        objectPooler = ObjectPooler.SharedIntance;
//    }
//
//    private void FixedUpdate()
//    {
//        timer += Time.deltaTime;
////        while(notStarted)
////        {
////            PoolMolecule();
////            if(molecule != null)
////                notStarted = false;
////        }
//
//        if(timer>=startTime && !pooling)
//        {
//            randomNumber = Random.Range(0, poolPositions.Length);
//            timer = 0;
//        }
//    }
//
//    #endregion
//
//    #region Custom Methods
//
//    /** Check if the molecule exit the camera collider and put it in the pool. **/
//    private void OnTriggerExit2D(Collider2D other)
//    {
//        pooling = true;
//        if(other.gameObject.CompareTag("Molecule"))
//        {
//            other.gameObject.SetActive(false);
//            PoolMolecule();
//        }
//        pooling = false;
//    }
//    
//    /** When the object holding this script collides with the LevelChargerManager collider, destroy this script and stop
//     * pooling molecules.
//     */
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Level Charger Manager"))
//        {
//            Destroy(this.gameObject);
//        }
//    }
//
//    /** Pools a molecule from the object pooler. **/ 
//    private void PoolMolecule()
//    {
////        molecule = ObjectPooler.SharedIntance.SpawnFromPool("molecule", transform.position, Quaternion.identity);
////
////        if (molecule == null) return;
////        molecule.transform.position = poolPositions[randomNumber].transform.position;
////        molecule.transform.rotation = poolPositions[randomNumber].transform.rotation;
////        molecule.SetActive(true);
//    }
//    
//    #endregion
//}
