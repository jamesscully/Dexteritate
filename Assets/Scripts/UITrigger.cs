using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{

    public string NotificationText = "";
    public string BigMessageTitle = "";
    public string BigMessageDesc = "";

    // if we've already activated, ignore 
    private bool activated = false;

    public bool startTimer = false;
    
    private PlayerUI ui;
    
    // Start is called before the first frame update
    void Start()
    {
        // get UI instance
        ui = FindObjectOfType<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // dont inform them if they've been here b4
            if (activated)
                return;

            activated = true;

            
            // send appropriate UI information
            if (BigMessageTitle != "")
            {
                ui.SetBigMessage(BigMessageTitle, BigMessageDesc);
            }

            if (NotificationText != "")
            {
                ui.SetNotification(NotificationText);
            }
        }
    }
}
