using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RicochetTarget : MonoBehaviour
{
    // how many times things have been hit across all instances
    static private int hit = 0;
    
    // are we staying on?
    private bool activated = false;
    
    // when was this first hit?
    private float activation_time = 0f;
    
    // this is calculated on enable
    private float disableAtTime = 0f;
    
    // how long should we wait till disabling again?
    public float ActivationTimeDelay = 5f;
    
    // we can set these, then duplicate the targets.
    public int OpenDoorAtCount = 1337;
    
    public GameObject DoorToOpen = null;

    // Start is called before the first frame update
    void Start()
    {
        if(DoorToOpen == null)
            Debug.LogError("RicochetTarget must have door to activate!");
    }

    // Update is called once per frame
    void Update()
    {
        // only check if we are activated - it's a waste otherwise
        if (activated)
        {
            if (IsTimeToDisable())
            {
                DisableTarget();
            }
        }

    }
    
    // this should be set once, we check if it's the same bullet hitting all targets.
    // given that only one room is done at a time, this should suffice
    static private GameObject initialBullet;


    private bool IsTimeToDisable()
    {
        return disableAtTime < Time.time;
    }
    private void DisableTarget()
    {
        print("Disabling target");
        activated = false;
        activation_time = 0;
        disableAtTime = 0;
        hit--;
        
        refreshMaterial();
        
        // only when we have 0 targets activated can we disable the initial bullet.
        if (hit == 0)
        {
            initialBullet = null;
        }
    }

    private void EnableTarget()
    {
        print("Enabling target");
        activated = true;
        activation_time = Time.time;
        disableAtTime = activation_time + ActivationTimeDelay;
        hit++;
        
        refreshMaterial();

        if (hit == OpenDoorAtCount)
        {
            DoorToOpen.SendMessage("EventActivated");
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        
        print("Hit by object: " + other.gameObject.GetInstanceID());
        
        // if no targets have been hit, then we must enable + set the triggering bullet
        if (!activated && initialBullet == null)
        {
            initialBullet = other.gameObject;
            EnableTarget();
        } 
        // if we ourselves aren't activated but hit, check if it's the initial bullet
        else if (!activated && initialBullet == other.gameObject)
        {
            EnableTarget();
        }
        else if (activated)
        {
            // ignore if we're already activated
        }
        // else they are trying to cheat us!
        else 
        {
            print("Illegal shot detected!");
        }
        
        // if we're done with the puzzle, destroy the bullet
        if (hit == OpenDoorAtCount)
            Destroy(other.gameObject);
        
    }

    public MeshRenderer mesh = null;

    public Material matActivated, matError, matNormal;

    public bool error = false;
    void refreshMaterial()
    {
        if (mesh == null)
        {
            print("No mesh specified on target");
            return;
        }
            
        Material[] mats = mesh.materials;
        
        if (activated)
            mats[1] = matActivated;
        else if (error)
            mats[1] = matError;
        else if (!activated && !error)
            mats[1] = matNormal;

        mesh.materials = mats;
    }
}
