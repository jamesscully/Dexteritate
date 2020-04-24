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
        bool moveable = other.gameObject.Equals(targetObjectA) || other.gameObject.Equals(targetObjectB);

        // moving objects that are grabbed creates weird scale-morph-effect, so we'll ignore until dropped.
        moveable = moveable && !other.gameObject.CompareTag("grabbed");
        
        // print("We've been hit!");

        if (moveable)
        {
            print("Object Grabbed");
            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool moveable = other.gameObject.Equals(targetObjectA) || other.gameObject.Equals(targetObjectB);

        // moving objects that are grabbed creates weird scale-morph-effect, so we'll ignore until dropped.
        moveable = moveable && !other.gameObject.CompareTag("grabbed");
        
        if (moveable)
        {
            // Debug.Log("We've had a exit");
            print("Object Left!");
            other.transform.parent = null;
        }
    }
}
