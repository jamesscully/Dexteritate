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
    }
    
    // Update is called once per frame
    void Update()
    {
        // escape will free the mouse
        if (Input.GetKeyDown("escape"))
            MOUSE_LOCKED = !MOUSE_LOCKED;
        
        lockCamera(MOUSE_LOCKED);
        
        cam_hoz = (Input.GetAxis("Mouse X") * MouseSensitivity);
        cam_ver = (Input.GetAxis("Mouse Y") * MouseSensitivity) * -1;
        
        float forwards = Input.GetAxis("Vertical");
        float sideways = Input.GetAxis("Horizontal");

        // we don't want to go over 180 deg range - we'll end up spinning around -- fix this
        // cam_ver = Mathf.Clamp(cam_ver, -90f, 90f);
        
        transform.Rotate(0, cam_hoz, 0);
        Camera.main.transform.Rotate(cam_ver, 0, 0);
        

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        CharacterController charcontroller = GetComponent<CharacterController>();

        if (Input.GetButton("Jump") && charcontroller.isGrounded)
            verticalVelocity = JumpHeight;

        Vector3 speed = new Vector3(sideways , verticalVelocity, forwards);
        speed = transform.rotation * speed;
        speed = speed * 10;
        
        charcontroller.Move(speed  * MovementSpeedMultiplier * Time.deltaTime);
    }
    
    private void lockCamera(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    // called when the charcontroller hits something
    void OnControllerColliderHit(ControllerColliderHit hit){
        
        // for now, we only care about the platforms 
        if (hit.gameObject.CompareTag("platform"))
        {
            hit.transform.SendMessage("spam", SendMessageOptions.RequireReceiver);
        }
    }
}
