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

    private void OnCollisionEnter(Collision other)
    {
        bool moveable = other.gameObject.Equals(targetObjectA) || other.gameObject.Equals(targetObjectB);

        // moving objects that are grabbed creates weird scale-morph-effect, so we'll ignore until dropped.
        moveable = moveable && !other.gameObject.CompareTag("grabbed");

        if (moveable)
        {
            other.transform.SetParent(transform);
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.Equals(targetObjectA) || other.gameObject.Equals(targetObjectB))
        {
            // Debug.Log("We've had a exit");
            other.transform.parent = null;
        }
    }
}
