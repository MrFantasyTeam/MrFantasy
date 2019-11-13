using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDestroyer : MonoBehaviour
{
    public float destroyTime;
    private float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= destroyTime)
        {
            
            Destroy(gameObject);
            Debug.LogWarning("Destroyed Before");
        }
    }
}
