using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayer : MonoBehaviour
{


    public GameObject targetObjectA;
    public GameObject targetObjectB;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(targetObjectA) || other.gameObject.Equals(targetObjectB))
        {
            Debug.Log("We've had a hit");
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(targetObjectA) || other.gameObject.Equals(targetObjectB))
        {
            Debug.Log("We've had a exit");
            other.transform.parent = null;
        }
    }
}
