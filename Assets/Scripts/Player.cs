using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float Speed;
    public float Sensitivity;
    public Transform SnakeHead;
    public int Health = 3;
    public int Length = 3;
    public Text HealthScore;
    public int Value;
    public GameResult GameResult;

    private Vector3 _previousMousePosition;
    private Rigidbody Rigidbody;
    private Vector3 tempVect = new Vector3(0, 0, 1);
    private SnakeBalls snakeBalls;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        HealthScore.text = Health.ToString();
        snakeBalls = GetComponent<SnakeBalls>();

    }
    
    private void Update()
    { 
        tempVect = tempVect.normalized* Speed * Time.deltaTime;
        Rigidbody.MovePosition(transform.position + tempVect);

        if (Input.GetMouseButton(0))
        {

            Vector3 moveSide = Input.mousePosition - _previousMousePosition;
            moveSide = moveSide.normalized * Speed * Sensitivity * Time.deltaTime;
            Vector3 newPosition = new Vector3(transform.position.x + moveSide.x, 0, transform.position.z + tempVect.z);
            Rigidbody.MovePosition(newPosition);
        }

        _previousMousePosition = Input.mousePosition;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BallsFood")
        {
            Value = collision.gameObject.GetComponent<BallsFood>().Value;
            Health += Value;
            HealthScore.text = Health.ToString();
            Destroy(collision.gameObject);

            for (int i = 0; i < Value; i++)
            {
                Length++;
                snakeBalls.AddBall();
            }
        }
        else if (collision.gameObject.tag == "Block")
        {
            Value = collision.gameObject.GetComponent<Block>().Value;
            if (Value >= Health)
            {
                GameResult.OnPlayerDied();
                Rigidbody.velocity = Vector3.zero;
            }
            else
            {
                Health -= Value;
                HealthScore.text = Health.ToString();
                Destroy(collision.gameObject);

                for (int i = 0; i < Value; i++)
                {
                    Length--;
                    snakeBalls.RemoveBall();
                }
            }
        }
        else if (collision.gameObject.tag == "Finish")
        {
            GameResult.OnPlayerWon();
        }
    }

}
