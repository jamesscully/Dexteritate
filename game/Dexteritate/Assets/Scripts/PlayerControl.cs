using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // how much we have to fall at this frame
    private float verticalVelocity = 0;
    
    // to lock & hide the cursor in the center
    private bool  MOUSE_LOCKED = true;
    
    // sensitivity of mouse - this should be extradited to editor variable
    private float MOUSE_SENS = 4f;
    private float MOVEMENT_SPEED = 10f;

    private float cam_hoz = 0f, cam_ver = 0f;
    
    // Update is called once per frame
    void Update()
    {
        // escape will free the mouse
        if (Input.GetKeyDown("escape"))
            MOUSE_LOCKED = !MOUSE_LOCKED;
        
        lockCamera(MOUSE_LOCKED);

        // by accumulating these numbers, we can prevent having to drag mouse back if over 180deg
        cam_hoz += (Input.GetAxis("Mouse X") * MOUSE_SENS);
        cam_ver += (Input.GetAxis("Mouse Y") * MOUSE_SENS) * -1;
        
        float forwards = Input.GetAxis("Vertical");
        float sideways = Input.GetAxis("Horizontal");

        // we don't want to go over 180 deg range - we'll end up spinning around
        cam_ver = Mathf.Clamp(cam_ver, -90f, 90f);
        
        Camera.main.transform.rotation = Quaternion.Euler(cam_ver, 0, 0);
        
        transform.Rotate(0, cam_hoz, 0);

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        CharacterController charcontroller = GetComponent<CharacterController>();

        if (Input.GetButton("Jump") && charcontroller.isGrounded)
            verticalVelocity = 5;
        
        Vector3 speed = new Vector3(sideways , verticalVelocity, forwards);
        speed = transform.rotation * speed;
        speed = speed * MOVEMENT_SPEED;

        charcontroller.Move(speed * Time.deltaTime);
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
}
