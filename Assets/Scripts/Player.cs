using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    public float Speed;
    public float Sensitivity;
    public Transform SnakeHead;
    public int Health = 3;
    public int Length = 3;
    public TextMeshPro PointsText;
    

    private Vector3 _previousMousePosition;
    private Rigidbody Rigidbody;
    private Vector3 tempVect = new Vector3(0, 0, 1);

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        PointsText.SetText(Health.ToString());
    }
    public void Movement()
    {
        Rigidbody.velocity = new Vector3(0, 0, Speed);
    }

    private void Update()
    { 
        tempVect = tempVect.normalized* Speed * Time.deltaTime;
        Rigidbody.MovePosition(transform.position + tempVect);

        if (Input.GetMouseButton(0))
        {

            Vector3 moveSide = Input.mousePosition - _previousMousePosition;
            moveSide = moveSide.normalized* Speed * Time.deltaTime;
            Vector3 newPosition = new Vector3(transform.position.x + moveSide.x, 0, transform.position.z + tempVect.z);
            Rigidbody.MovePosition(newPosition);
        }

        _previousMousePosition = Input.mousePosition;
    }



}
