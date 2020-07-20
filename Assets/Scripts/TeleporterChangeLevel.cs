using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterChangeLevel : MonoBehaviour
{

    public bool LoadsScene = false;
    public string SceneToLoad;

    public bool TeleportsPlayer = true;
    public bool AddsFail = true;

    public bool IgnoreProjectiles = true;
    
    // where?
    public Vector3 TeleportTo;
    
    // use an empty object to easily locate where we wanna be teleported to
    public GameObject TeleportToObject;


    private GameControllerScript controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameControllerScript>();
        
        // if we have a teleport-to object, tp there
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
        // if the player enters, and it's out-of-bounds, add fail and tp
        if (other.gameObject.CompareTag("Player") && AddsFail && TeleportsPlayer)
        {
            controller.incFailCount();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject pObject = other.gameObject;

        bool playerColliding = pObject.CompareTag("Player");

        // we'll likely only want to load a scene if the player enters it
        if (LoadsScene && playerColliding) {
            SceneManager.LoadScene(SceneToLoad);
        }
        
        // if this teleporter is used for objects only, ignore the player
        if (playerColliding && !TeleportsPlayer)
        {
            return;
        }

        if (pObject.CompareTag("projectile") && IgnoreProjectiles)
        {
            return;
        }

        if (!playerColliding)
        {
            Rigidbody r = other.gameObject.GetComponent<Rigidbody>();
            r.velocity = Vector3.zero; r.angularVelocity = Vector3.zero; r.ResetInertiaTensor();
        }
        // else, teleport them away!
        other.gameObject.transform.SetPositionAndRotation(TeleportTo, other.gameObject.transform.rotation);
    }
}
