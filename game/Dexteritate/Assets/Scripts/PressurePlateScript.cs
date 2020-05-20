using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PressurePlateScript : MonoBehaviour
{


    // Object that will activate this pressure plate
    [FormerlySerializedAs("ObjectToActivate")] [FormerlySerializedAs("ActivatingGameObject")] public GameObject AcceptedObject;
    
    public GameObject PlateObject;
    
    // object that will have events sent to
    public GameObject ObjectToChange;

    public GameObject[] EventObjects;
    
    private bool activated = false;
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    
    private MeshRenderer mesh;

    public bool PlayerActivation = true;

    

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
        // show the player that this has been activated or not
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

        bool PlayerActivate = other.gameObject.CompareTag("Player") && PlayerActivation;
        
        if (objOther.Equals(AcceptedObject) || PlayerActivate)
        {
            activate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject objOther = other.gameObject;

        bool PlayerActivate = other.gameObject.CompareTag("Player") && PlayerActivation;
        
        if (objOther.Equals(AcceptedObject) || PlayerActivate)
        {
            ObjectToChange.SendMessage("EventStay", SendMessageOptions.DontRequireReceiver);
            
            foreach (GameObject o in EventObjects)
            {
                o.SendMessage("EventStay", SendMessageOptions.DontRequireReceiver);
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject objOther = other.gameObject;
        
        bool PlayerActivate = other.gameObject.CompareTag("Player") && PlayerActivation;

        if (objOther.Equals(AcceptedObject) || PlayerActivate)
        {
            deactivate();
        }
    }
    


    private void activate()
    {
        activated = true;
        // keep this for legacy
        ObjectToChange.SendMessage("EventActivated", SendMessageOptions.DontRequireReceiver);

        foreach (GameObject o in EventObjects)
        {
            o.SendMessage("EventActivated", SendMessageOptions.DontRequireReceiver);
        } 
        
        refreshMaterial();
    }

    private void deactivate()
    {
        activated = false;
        ObjectToChange.SendMessage("EventDeactivated", SendMessageOptions.DontRequireReceiver);
        
        foreach (GameObject o in EventObjects)
        {
            o.SendMessage("EventDeactivated", SendMessageOptions.DontRequireReceiver);
        } 
        refreshMaterial();
    }
    
    public Material matActivated;
    public Material matNormal;
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
