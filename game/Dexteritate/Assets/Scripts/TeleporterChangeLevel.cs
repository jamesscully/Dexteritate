using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterChangeLevel : MonoBehaviour
{

    public bool LoadsScene = false;
    public string SceneToLoad;

    public bool TeleportPlayer = true;
    
    // where?
    public Vector3 TeleportTo;
    
    // use an empty object to easily locate where we wanna be teleported to
    public GameObject TeleportToObject;

    // Start is called before the first frame update
    void Start()
    {
        if (TeleportToObject != null)
        {
            TeleportTo = TeleportToObject.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        print("TriggerEnter");
        GameObject pObject = other.gameObject;

        if (LoadsScene && pObject.CompareTag("Player")) {
            SceneManager.LoadScene(SceneToLoad);
        } else if (pObject.CompareTag("Player") && !TeleportPlayer)
        {
            return;
        }

        other.gameObject.transform.SetPositionAndRotation(TeleportTo, other.gameObject.transform.rotation);
    }
}
