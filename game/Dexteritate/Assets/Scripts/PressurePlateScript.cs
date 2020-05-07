using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{


    public GameObject ActivatingGameObject;
    public GameObject PlateObject;
    public GameObject Door;

    private bool activated = false;
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    
    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = PlateObject.GetComponent<MeshRenderer>();

        originalPosition = PlateObject.transform.position;
        originalRotation = PlateObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            PlateObject.transform.SetPositionAndRotation(new Vector3(originalPosition.x, originalPosition.y - 0.2f, originalPosition.z), originalRotation);
        }
        else
        {
            PlateObject.transform.SetPositionAndRotation(originalPosition, originalRotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject objOther = other.gameObject;
        
        if (objOther.Equals(ActivatingGameObject))
        {
            activate();
            Door.SendMessage("EventActivated", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject objOther = other.gameObject;

        if (objOther.Equals(ActivatingGameObject))
        {
            deactivate();
            Door.SendMessage("EventDeactivated", SendMessageOptions.DontRequireReceiver);
        }
    }
    
    public Material matActivated;
    public Material matNormal;

    private void activate()
    {
        activated = true;
        refreshMaterial();
    }

    private void deactivate()
    {
        activated = false;
        refreshMaterial();
    }
    
    void refreshMaterial()
    {
        Material[] mats = mesh.materials;
        
        
        if (activated)
            mats[0] = matActivated;
        else
            mats[0] = matNormal;

        mesh.materials = mats;
    }
}
