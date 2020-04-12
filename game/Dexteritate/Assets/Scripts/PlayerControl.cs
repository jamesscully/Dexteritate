using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float vertV = 0;
    // Update is called once per frame
    void Update()
    {
        float cam_hoz = Input.GetAxis("Mouse X");
        float cam_ver = Input.GetAxis("Mouse Y");
        float forwards = Input.GetAxis("Vertical");
        float sideways = Input.GetAxis("Horizontal");
        
        transform.Rotate(0, cam_hoz, 0);
        Camera.main.transform.Rotate(cam_ver, 0, 0);

        vertV += Physics.gravity.y * Time.deltaTime;

        CharacterController charcontroller = GetComponent<CharacterController>();

        if (Input.GetButton("Jump") && charcontroller.isGrounded)
        {
            vertV = 5;
        }
        
        Vector3 speed = new Vector3(cam_hoz, vertV, forwards);

        speed = transform.rotation * speed;

        charcontroller.Move(speed * Time.deltaTime);
    }
}
