using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            // print("   Time now  : " + Time.time);
            // print("Disable time : " + activation_time + (ActivationTimeDelay));
            // print("Active  time : " + activation_time);
            // print("Delay time : " + ActivationTimeDelay);
            // print("DisablingTime : "  + );
            bool disableTime = activation_time + (ActivationTimeDelay) < Time.time;

            if (disableTime)
            {
                print("Disabling");
                activated = false;
            }
        }

    }

    // this counts how many time's things have been hit across all instances
    static private int hit = 0;

    // are we staying on?
    private bool activated = false;

    // when was this first hit?
    private float activation_time = 0f;

    private float disableAtTime = 0f;

    // how long should we wait till disabling again?
    public float ActivationTimeDelay = 5f;


    // this should be set once, we check if it's the same bullet hitting all targets
    static private GameObject initialBullet;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!activated)
        {
            hit++;
            print("We've been hit sir! Hits: " + hit);
            activated = true;
            activation_time = Time.time;

            disableAtTime = activation_time + ActivationTimeDelay;
            
            print("Activating at time: " + activation_time);
        }
    }
}
