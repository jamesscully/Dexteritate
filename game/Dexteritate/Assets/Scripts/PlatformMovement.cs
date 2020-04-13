using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatformMovement : MonoBehaviour
{
    
    

    private float deltaX = 0f, deltaY = 0f, deltaZ = 0f;

    public float speed;

    [FormerlySerializedAs("startPos")] public Vector3 PositionA;
    [FormerlySerializedAs("endPos")] public Vector3 PositionB;

    private float startTime;
    
    private float distance = 0f;
    private float travelled = 0f;

    private BoxCollider platform = null;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        distance = Vector3.Distance(PositionA, PositionB);

        platform = GetComponentInChildren<BoxCollider>();
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

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision occured");
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
