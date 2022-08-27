using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;
    public Rigidbody Rigidbody;
    public float Sensitivity;

    private Vector3 _previousMousePosition;


    public void Movement()
    {
        Rigidbody.velocity = new Vector3(0, 0, Speed);
    }

    private void Update()
    { 
        Movement(); 
            
    }



}
