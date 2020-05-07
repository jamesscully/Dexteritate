using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTargetScript : MonoBehaviour
{

    public Material matActivated;
    public Material matError;
    public Material matNormal;

    public int StayActiveTime = 5;
    public int EventHitCount = 3;
    
    
    static int TargetActiveCount = 0;

    public GameObject DoorToOpen = null;

    private MeshRenderer mesh;

    private bool activated = false, error = false;

    private float hitTime;

    private GameObject collisionObj;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        // mesh = GameObject.Find("TargetMesh").GetComponent<MeshRenderer>();
        
        if(mesh == null)
            Debug.LogError("ShootingTargetScript: Error getting TargetMesh Renderer; borken?");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if(timeToDeactivate())
                deactivate();
        }
    }

    bool timeToDeactivate()
    {
        float endTime = hitTime + (StayActiveTime);
        return Time.time > endTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        // ignore if not bullet
        if (!other.gameObject.CompareTag("projectile") || activated)
            return;
        
        activate();
        
        // remove the bullet
        Destroy(other.gameObject);
    }

    void activate()
    {
        TargetActiveCount++;
        
        activated = true;
        hitTime = Time.time;
        
        refreshMaterial();
        
        // compared static count to how many to activate
        if (TargetActiveCount == EventHitCount)
        {
            DoorToOpen.SendMessage("EventActivated");
        }
    }

    void deactivate()
    {
        // decrement the active target count
        TargetActiveCount--;
        activated = false;
        
        refreshMaterial();
    }

    void refreshMaterial()
    {
        Material[] mats = mesh.materials;
        
        if (activated)
            mats[0] = matActivated;
        else if (error)
            mats[0] = matError;
        else if (!activated && !error)
            mats[0] = matNormal;

        mesh.materials = mats;
    }
}
