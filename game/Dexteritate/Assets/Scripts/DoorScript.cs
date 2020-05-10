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

    public bool StayOpen = false;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void EventActivated()
    {
        opened = true;
        gameObject.SetActive(false);
    }

    void EventDeactivated()
    {
        if (StayOpen)
            return;

        opened = false;
        gameObject.SetActive(true);
    }
}
