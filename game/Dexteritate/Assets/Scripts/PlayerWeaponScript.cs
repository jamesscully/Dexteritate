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

    // how forceful players throw objects
    public int PlayerPuntForce = 1000;
    
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


        if (Input.GetButtonDown("Fire1") && holding)
        {
            Rigidbody objectToPunt = heldObject.GetComponent<Rigidbody>();
            
            StopHoldingObject();
            
            Vector3 direction = Camera.main.transform.forward * PlayerPuntForce;
            
            objectToPunt.AddForce(direction, ForceMode.Force);
        }
    }


    public int ProjectileSpeed = 1000;
    public float ProjectileTimeToLive = 3f;
    public GameObject Projectile;
    void FireBullet()
    {
        // get the 'crosshair'
        Vector3 cameraPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        // move it away from us so we don't get stuck
        cameraPoint = cameraPoint + Camera.main.transform.forward * 2;
        
        GameObject bullet = Instantiate(Projectile, cameraPoint, Camera.main.transform.rotation);
        
        
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        rigidbody.AddForce(Camera.main.transform.forward * ProjectileSpeed, ForceMode.Force);
        
        Destroy(bullet, ProjectileTimeToLive);

    }
    
    void FireGrabObjectRay()
    {
        Transform cam = Camera.main.transform;
        
        RaycastHit hit;
        
        // this translates the middle of the screen to an actual world point
        Vector3 origin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        Ray ray = new Ray(origin, cam.forward);
        
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
        Rigidbody heldRigid = heldObject.GetComponent<Rigidbody>();

        if (heldRigid != null)
        {
            heldRigid.velocity = Vector3.zero;
            heldRigid.angularVelocity = Vector3.zero;
        }
        
        // "grabbed" prevents triggers from being activated (spammed)
        // before the user places the object
        heldObject.tag = "grabbed";
    }

    private void StopHoldingObject()
    {
        // make it independent of us
        heldObject.transform.parent = null;
        heldObject.tag = "grabbable";
        
        // holding the object will cause velocity gain; remove it
        Rigidbody heldRigid = heldObject.GetComponent<Rigidbody>();
        if (heldRigid != null)
        {
            heldRigid.velocity = Vector3.zero;
            heldRigid.angularVelocity = Vector3.zero;
        }

        

        holding = false;
    }
}
