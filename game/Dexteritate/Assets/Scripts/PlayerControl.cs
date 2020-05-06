using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{


    // how much we have to fall at this frame
    private float verticalVelocity = 0;
    
    // to lock & hide the cursor in the center
    private bool  MOUSE_LOCKED = true;

    public bool DebugMode = false;
    
    public Vector3 SpawnPoint = Vector3.zero;
    
    // sensitivity of mouse - this should be extradited to editor variable
    public float MouseSensitivity = 4f;
    public float MovementSpeedMultiplier = 1;
    public float SprintSpeedMultiplier = 1;
    public float JumpHeight = 5;

    private float cam_hoz = 0f, cam_ver = 0f;

    private float sprMult;
    
    // Start is called before the first frame update
    void Start()
    {
        sprMult = SprintSpeedMultiplier;

        // if we are debugging, we don't want to move from where its located in the world
        if (!DebugMode)
        {
            transform.position = SpawnPoint;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
        // escape will free the mouse
        if (Input.GetKeyDown("escape"))
            MOUSE_LOCKED = !MOUSE_LOCKED;
        
        lockCursor(MOUSE_LOCKED);
        
        cam_hoz = (Input.GetAxis("Mouse X") * MouseSensitivity);
        cam_ver = (Input.GetAxis("Mouse Y") * MouseSensitivity) * -1;
        
        float forwards = Input.GetAxis("Vertical");
        float sideways = Input.GetAxis("Horizontal");
        
        
        transform.Rotate(0, cam_hoz, 0);
        Camera.main.transform.Rotate(cam_ver, 0, 0);
        
        
        CharacterController charcontroller = GetComponent<CharacterController>();
        
        if (Input.GetButton("Jump") && charcontroller.isGrounded)
            verticalVelocity = JumpHeight;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        // prevent velocity build-up from line above; falling off objects drops player
        if (verticalVelocity < -1.5f)
            verticalVelocity = -1.5f;
        
        Vector3 speed = new Vector3(sideways , verticalVelocity, forwards);
        speed = transform.rotation * speed;
        speed = speed * 10;
        
        charcontroller.Move(speed  * MovementSpeedMultiplier * Time.deltaTime);
    }
    
    private void lockCursor(bool locked)
    {
        if (locked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
        
        // only visible if not locked
        Cursor.visible = !locked;
    }

}
