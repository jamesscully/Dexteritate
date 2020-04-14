using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    private Transform drop;
    // Start is called before the first frame update
    void Start()
    {
        drop = DropPoint.transform;
    }

    private GameObject heldObject = null;


    private bool holding = false;

    public GameObject DropPoint;
    
    // Update is called once per frame
    void Update()
    {
        bool PrimaryFire = Input.GetButton("Fire1");
        bool SecondaryFire = Input.GetButton("Fire2");

        if(holding)
            MoveHeldObject();

        if (Input.GetButtonDown("Fire2"))
        {
            if (holding)
            {
                StopHoldingObject();
            }
            
            // fire, see if it's grabbable
            if (!holding)
            {
                FireGrabObjectRay();
            }
            

        }

        // if (SecondaryFire)
        // {
        //     MoveHeldObject();
        // }
        //
        // if (Input.GetButtonUp("Fire2"))
        // {
        //     StopHoldingObject();
        // }
    }
    
    void FireGrabObjectRay()
    {

        RaycastHit hit; 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        
        Debug.DrawRay(ray.origin, ray.direction);
        
        // if we have hit something
        if (Physics.Raycast(ray, out hit))
        {
            // get information about the hit object
            Collider hitCollider = hit.collider;
            GameObject objHit = hitCollider.gameObject;
            string tag = hitCollider.gameObject.tag;

            if (tag == "grabbable")
            {
                print("Grabbable object has been hit");
                holding = true;
                heldObject = objHit;
            }
        }
        
        
        // Debug.Log(hit.point);
    }
    
     
    private void MoveHeldObject()
    {
        // // we don't care if we arent holding an object
        // if (heldObject == null)
        //     return;
        
        heldObject.transform.parent = drop;
    }

    private void StopHoldingObject()
    {
        print("Dropping object");
        // make it independent of us
        heldObject.transform.parent = null;
        holding = false;
    }
}
