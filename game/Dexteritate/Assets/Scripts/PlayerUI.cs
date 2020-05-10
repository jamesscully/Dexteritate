using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public Text TNotification, TTime, TMishaps, TBigMessage, TBigDesc, TCrosshair;
    
    private float TimeLeft = 0f;
    
    public bool IsCountingTime = true;

    public string TimePrefix = "Time Taken: ";
    public string MishapsPrefix = "Mishaps: ";
    
    // Start is called before the first frame update
    void Start()
    {

        try
        {
            TNotification.text = "";
            TTime.text = "";
            TMishaps.text = "";
            TBigMessage.text = "";
            TBigDesc.text = "";
            TCrosshair.text = "+";
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("You forgot to specify UI references!");
            Application.Quit();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCountingTime)
        {
            TimeLeft += Time.deltaTime;
            SetText(TTime, TimePrefix + TimeLeft);
        }

    }
    
    void SetNotification(string n)
    {
        SetText(TNotification, n);
    }

    void SetBigMessage(string title, string description)
    {

        // 5 seconds for big notifications
        int defTimeToLive = 5;
        
        SetText(TBigMessage, title, defTimeToLive);
        SetText(TBigDesc, description, defTimeToLive);
    }

    void ResetTime()
    {
        TimeLeft = 0;
    }

    void SetMishaps(int m)
    {
        SetText(TMishaps, MishapsPrefix + m);
    }

    void SetText(Text t, string message, int timeout = -1)
    {
        if (timeout == -1)
            t.text = message;
        else
            StartCoroutine(TimeoutText(t, message, timeout));
    }


    IEnumerator TimeoutText(Text t, string msg, int timeToLive)
    {
        t.text = msg;
        
        yield return new WaitForSeconds(timeToLive);

        t.text = "";
    }
    
    
}
