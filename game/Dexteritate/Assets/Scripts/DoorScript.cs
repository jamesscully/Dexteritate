using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class DoorScript : MonoBehaviour, EventInterface
{

    private Vector3 OriginalPos;
    private Quaternion OriginalRot;

    public bool Closed = true;
    
    // Start is called before the first frame update
    void Start()
    {
        OriginalPos = transform.position;
        OriginalRot = transform.rotation;
        
        SetActive(Closed);
    }

    // we may want the door to not stay open - puzzles!
    public bool StayOpen = false;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void EventActivated()
    {
        SetActive(false);
    }

    public void EventStay()
    {
        
    }

    public void EventDeactivated()
    {
        if (StayOpen)
            return;
        
        SetActive(true);
    }

    public void SetActive(bool b)
    {
        Vector3 HidePos = new Vector3(1337, 1337, 1337);

        if (b)
        {
            gameObject.transform.SetPositionAndRotation(OriginalPos, OriginalRot);
        }
        else
        {
            gameObject.transform.SetPositionAndRotation(HidePos, OriginalRot);
        }
        
    }
}
