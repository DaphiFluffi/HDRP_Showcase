using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Simple Script to make the Torus spin 
 */
public class Spin : MonoBehaviour
{
    [SerializeField] private float xAngle = 0 ;
    [SerializeField] private float yAngle = 0;
    [SerializeField] private float zAngle= 0;

    void FixedUpdate ()
    {
        transform.Rotate (xAngle *Time.fixedDeltaTime,yAngle *Time.fixedDeltaTime,zAngle *Time.fixedDeltaTime); //rotates 50 degrees per second around z axis
    }
}
