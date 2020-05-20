using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityTemplateProjects;

public class AddForceOnEvent : MonoBehaviour, EventInterface
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 VelocityEnter = Vector3.zero;
    public Vector3 VelocityStay = Vector3.zero;
    public Vector3 VelocityExit = Vector3.zero;

    
    public bool OnActivation = false;
    public bool OnActivationStay = false;
    public bool OnActivationLeave = false;

    public void EventActivated()
    {
        if(OnActivation)
            Apply(VelocityEnter);
    }

    public void EventStay()
    {
        if(OnActivationStay)
            Apply(VelocityStay);
    }

    public void EventDeactivated()
    {
        if(OnActivationLeave)
            Apply(VelocityExit);
    }

    public void Apply(Vector3 v)
    {
        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = v;
    }
}
