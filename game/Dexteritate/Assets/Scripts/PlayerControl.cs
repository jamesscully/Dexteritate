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
        float leftright = Input.GetAxis("Mouse X");
        float updown = Input.GetAxis("Mouse Y");
        float forwards = Input.GetAxis("Vertical");
        float sideways = Input.GetAxis("Horizontal");
        
        transform.Rotate(0, leftright, 0);
        Camera.main.transform.Rotate(updown, 0, 0);

        vertV += Physics.gravity.y * Time.deltaTime;
        
        CharacterController 




    }
}
