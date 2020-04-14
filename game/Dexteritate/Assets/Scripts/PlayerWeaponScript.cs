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

        Transform cam = Camera.main.transform;
        
        Debug.Log("Player Rot: " + transform.rotation + " Player Forward: " + transform.forward);
        RaycastHit hit;
        //Ray ray = new Ray(cam.position, cam.forward);

        Vector3 lookAt = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        print(transform.position);

        Ray ray = new Ray(lookAt, cam.forward);
        Debug.DrawRay(ray.origin, cam.forward * 10, Color.magenta, 10);


        // if we have hit something
        // if (Physics.Raycast(ray, out hit))
        // if (Physics.Raycast(cam.position, cam.forward, out hit, 100f))
        // {
        //     // get information about the hit object
        //     Collider hitCollider = hit.collider;
        //     GameObject objHit = hitCollider.gameObject;
        //     string tag = hitCollider.gameObject.tag;
        //
        //     if (tag == "grabbable")
        //     {
        //         print("Grabbable object has been hit");
        //         holding = true;
        //         heldObject = objHit;
        //     }
        // }
        
        
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
