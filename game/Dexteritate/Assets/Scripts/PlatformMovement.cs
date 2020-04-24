using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatformMovement : MonoBehaviour
{
    

    public float speed;

    public float range = 0f;

    public bool moveX, moveY, moveZ;

    private float startTime;
    
    private float distance = 0f;
    private float travelled = 0f;

    private BoxCollider platform = null;

    private Vector3 PositionA, PositionB;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        // c# doesn't implicitly cast bool to int
        int mX = moveX ? 1 : 0; int mY = moveY ? 1 : 0; int mZ = moveZ ? 1 : 0;
        
        // PositionA = transform.position - new Vector3(range * mX, range * mY, range * mZ);
        
        PositionA = transform.position;
        PositionB = transform.position + new Vector3(range * mX, range * mY, range * mZ);
        
        distance = Vector3.Distance(PositionA, PositionB);
    }

    // Update is called once per frame
    void Update()
    {
        // for each second, add unit of speed
        travelled = (Time.time - startTime) * speed;

        // however far we have travelled out of our distance
        float frac = travelled / distance;
        
        // move there, i.e. if we was 200u out of 400u, this would mean 0.5, and we should be half way
        transform.position = Vector3.Lerp(PositionA, PositionB, frac);

        // if we are at positionB (end-pos) then reverse.
        // note: I should probably look into floating-point rounding errs. this seems vulnerable
        if (transform.position.Equals(PositionB))
        {
            reverse();
        }
    }

    void reverse()
    {
        // swap both A and B
        Vector3 temp = PositionA;
        PositionA = PositionB;
        PositionB = temp;

        // restart time
        startTime = Time.time;
    }
    
    
}
