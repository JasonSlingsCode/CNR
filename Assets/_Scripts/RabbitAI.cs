using UnityEngine;
using System.Collections;

public class RabbitAI : MonoBehaviour {


    public Rigidbody2D ThePlayerRigidbody2D;
    public float Rate = 1;
    public float RandomRate;
    public float JumpHeight;
    public float Speed;
    public float DashForce = 1;


    private bool FacingRight = true;

    private bool CanMoveInAir = true;
    private bool TouchingWall = false;
    private bool IsGrounded = true;
    private float MoveControl;


    private float hInput;

	public GameObject DeadRabbitParts;

    void Awake()
    {
        ThePlayerRigidbody2D = GetComponent<Rigidbody2D>();

    }
	// Use this for initialization
	void Start () {
        InvokeRepeating("RandomTimer", 1, Rate);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move(hInput);
    }

    void RandomTimer()
    {
        JumpHeight = Random.Range(5, 12);
        RandomRate = Random.Range(1, 6);
        Speed = Random.Range(0, 80);
        Invoke("Jump", RandomRate);


    }

    public void Move(float HorizontalInput)
    {
        Vector2 Movement = ThePlayerRigidbody2D.velocity;
        if (CanMoveInAir && !TouchingWall)
        { // Checks if can move in air AND if not touching wall
            Movement.x = HorizontalInput * (Speed * DashForce) * Time.deltaTime;
        }
        if (!CanMoveInAir && !IsGrounded && !TouchingWall)
        { // Checks if can move in air AND if not touching ground OR wall
            return;
        }
        ThePlayerRigidbody2D.velocity = Movement;
        
        MoveControl = HorizontalInput;
        // if sprite is moving and facing left
        if (MoveControl > 0 && !FacingRight)
        {
            //flip sprite facing
            FlipSprite();
        }
        // else if sprite is moving and facing right
        else if (MoveControl < 0 && FacingRight)
        {
            //flip sprite facing
            FlipSprite();
        }
    }

    public void Jump()
    {
        ThePlayerRigidbody2D.velocity = new Vector2(ThePlayerRigidbody2D.velocity.x, 0);
            ThePlayerRigidbody2D.velocity = (JumpHeight * Vector2.up);
        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                hInput = -1;
                break;
            case 1:
                hInput = 1;
                break;
            default:
                break;
        }


            // AudioSource source = GetComponent<AudioSource>();
            // source.PlayOneShot(JumpSound, 0.4f);

    }

    void FlipSprite()
    {
        // face the opposite direction
        FacingRight = !FacingRight;
        
        // get the local scale
        Vector3 TheScale = transform.localScale;
        
        // flip the x axis
        TheScale.x *= -1;
        
        // apply back to local scale
        transform.localScale = TheScale;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Sword")
		{
			foreach(Transform child in transform)
			{

					child.gameObject.SetActive(false);
			}

			DeadRabbitParts.SetActive(true);
			Invoke("Dead", 1);
		}
	}

	void Dead()
	{
		Destroy (transform.parent.gameObject);
	}
}
