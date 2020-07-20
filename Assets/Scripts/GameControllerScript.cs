using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    private GameObject player = null;
    private PlayerUI ui;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ui = player.GetComponentInChildren<PlayerUI>();

        if (player == null || ui == null)
        {
            Debug.LogError("Controller could not find the player or UI");
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int mishaps = 0;
    
    // private float startTime = 0;
    // private int failCount;
    //
    // public void setStartTime()
    // {
    //     startTime = Time.time;
    // }
    //
    
    public void setFailCount(int c)
    {
        mishaps = c;
        ui.SetMishaps(mishaps);
    }
    
    public void incFailCount()
    {
        setFailCount(mishaps + 1);
    }
    
    public void decFailCount() 
    {
        setFailCount(mishaps - 1);
    }
    
    
}
