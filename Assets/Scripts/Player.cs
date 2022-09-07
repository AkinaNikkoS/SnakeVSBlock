using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float Speed;
    public float Sensitivity;
    public Transform SnakeHead;
    public int Health = 1;
    public int Length = 1;
    public Text HealthScore;
    public int Value;
    public GameResult GameResult;
    public AudioClip AudioClip;

    private Vector3 _previousMousePosition;
    private Rigidbody Rigidbody;
    private Vector3 tempVect = new Vector3(0, 0, 1);
    private SnakeBalls snakeBalls;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        HealthScore.text = Health.ToString();
        snakeBalls = GetComponent<SnakeBalls>();
        Health += Value;
        HealthScore.text = Health.ToString();
        for (int i = 0; i < Value; i++)
        {
            Length++;
            snakeBalls.AddBall();
        }
    }
   
    private void Update()
    {
        tempVect = tempVect.normalized * Speed * Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            Vector3 moveSide = Input.mousePosition - _previousMousePosition;
            moveSide = moveSide.normalized * Speed * Sensitivity * Time.deltaTime;
            float moveX = transform.position.x + moveSide.x;
            if (0.6 > moveX) moveX = 0.6f;
            else { if (moveX > 5.4) moveX = 5.4f; } 
            Vector3 newPosition = new Vector3(moveX, 0, transform.position.z + tempVect.z);
            Rigidbody.MovePosition(newPosition);
        }
        else 
        {
            Rigidbody.MovePosition(transform.position + tempVect);
        }

        _previousMousePosition = Input.mousePosition;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            _audio.Play();
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
            _audio.PlayOneShot(AudioClip);
            Value = collision.gameObject.GetComponent<Block>().Value;
            if (Value >= Health)
            {
                Rigidbody.velocity = Vector3.zero;
                GameResult.OnPlayerDied();
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
