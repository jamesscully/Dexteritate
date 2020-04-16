using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
    }

    // original position, so we can reference from it
    private Vector3 ogPos;

    private bool opened = false;
    // Update is called once per frame
    void Update()
    {
        if(opened)
            transform.SetPositionAndRotation(ogPos + new Vector3(0, -10, 0), transform.rotation);
        else
        {
            transform.SetPositionAndRotation(ogPos, transform.rotation);
        }
    }

    void PPlateActivated()
    {
        opened = true;
    }

    void PPlateDeactivated()
    {
        opened = false;
    }
}
