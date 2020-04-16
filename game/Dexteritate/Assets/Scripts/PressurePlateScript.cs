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


    // when we start off, we won't be activated
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    
    // Start is called before the first frame update
    void Start()
    {
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
            activated = true;
            Door.SendMessage("PPlateActivated", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject objOther = other.gameObject;

        if (objOther.Equals(ActivatingGameObject))
        {
            activated = false;
            Door.SendMessage("PPlateDeactivated", SendMessageOptions.DontRequireReceiver);
        }
    }
}
