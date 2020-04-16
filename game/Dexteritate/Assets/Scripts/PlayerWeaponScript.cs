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

        // whilst we're holding an object, move it!
        if(holding)
            MoveHeldObject();

        if (Input.GetButtonDown("Fire2"))
        {
            if (!holding)
            {
                FireGrabObjectRay();
            } else if (holding)
            {
                StopHoldingObject();
            }
        }


        if (Input.GetButtonDown("Fire1") && !holding)
        {
            FireBullet();
            
        }
    }


    public GameObject Projectile;
    void FireBullet()
    {
        // get the 'crosshair'
        Vector3 cameraPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        // move it away from us so we don't get stuck
        cameraPoint = cameraPoint + Camera.main.transform.forward * 2;
        
        GameObject bullet = Instantiate(Projectile, cameraPoint, Camera.main.transform.rotation);
        
        
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        rigidbody.AddForce(Camera.main.transform.forward * 1000, ForceMode.Force);
        
        Destroy(bullet, 3f);

    }
    
    void FireGrabObjectRay()
    {
        Transform cam = Camera.main.transform;
        
        RaycastHit hit;
        //Ray ray = new Ray(cam.position, cam.forward);

        Vector3 lookAt = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        Ray ray = new Ray(lookAt, cam.forward);
        Debug.DrawRay(ray.origin, cam.forward * 10, Color.magenta, 10);


        // if we have hit something
        if (Physics.Raycast(cam.position, cam.forward, out hit, 100f))
        {
            // get information about the hit object
            Collider hitCollider = hit.collider;
            GameObject objHit = hitCollider.gameObject;
            string tag = hitCollider.gameObject.tag;
        
            if (tag == "grabbable")
            {
                holding = true;
                heldObject = objHit;
            }
        }
    }
    
     
    private void MoveHeldObject()
    {
        heldObject.transform.parent = drop;
        heldObject.transform.position = drop.position;
        
        // grabbed prevents triggers from being activated before the user places them
        heldObject.tag = "grabbed";
    }

    private void StopHoldingObject()
    {
        // make it independent of us
        heldObject.transform.parent = null;
        heldObject.tag = "grabbable";
        holding = false;
    }
}
