using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTargetScript : MonoBehaviour
{

    public Material matActivated;
    public Material matError;
    public Material matNormal;

    private MeshRenderer mesh;

    private bool activated = false, error = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = GameObject.Find("TargetMesh").GetComponent<MeshRenderer>();
        
        if(mesh == null)
            Debug.LogError("ShootingTargetScript: Error getting TargetMesh Renderer; borken?");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("projectile"))
            return;

        activated = true;
        
        print("Activated");
        
        refreshMaterial();
    }

    void refreshMaterial()
    {
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
