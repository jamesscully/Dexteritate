using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterScript : MonoBehaviour
{

    public bool LoadsScene = false;
    public string SceneToLoad;

    public bool TeleportPlayer = true;
    public Vector3 TeleportTo;
    public GameObject TeleportObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject pObject = other.gameObject;
        Transform pTransform = other.gameObject.transform;
        
        if(!TeleportPlayer)
            return;
        
        if (LoadsScene && pObject == TeleportObject) {
            SceneManager.LoadScene(SceneToLoad);
        } else if (other.gameObject == TeleportObject) {
            other.gameObject.transform.SetPositionAndRotation(TeleportTo, other.gameObject.transform.rotation);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print("Collision");
        // if we don't want to tp the player, exit!

        

    }

    private void OnCollisionEnter(Collision other)
    {
        print("We're being hit!");
        
        

    }
}
