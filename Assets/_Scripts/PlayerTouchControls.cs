using UnityEngine;
using System.Collections;

public class PlayerTouchControls : MonoBehaviour
{
    public GameObject ThePlayer;
    public Rigidbody2D ThePlayerRigidbody2D;
    public Transform GroundCheck;
    public Transform WallCheck;
    private float GroundRadius = 0.5f;
    private float WallRadius = 0.5f;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsWall;
    public Animator Anim;
    [Header("Player Movement")]
    private Vector2
        Movement;
    private float MoveControl;
    private float LeftRight = 0;
    private bool FacingRight = true;
    public float Speed = 500f;
    public bool IsGrounded = false;
    public bool TouchingWall = false;
    public bool CanMoveInAir = true;
    [Header("Player Jumping")]
    public float
        JumpHeight = 500f;
    private bool TouchJumpHeld = false;
    private bool IsDoubleJumping;
    public AudioClip JumpSound;
    public AudioClip DoubleJumpSound;
    [Header("Player Abilities")]
    public float
        DashStart;
    private bool
        TouchDashHeld = false;
    public float DashForce;
    public float DashTime;
    public bool IsDashing;
    public AudioClip DashSound;
    public float DashCooldownPeriod;
    public GameObject DashEffect;





    // Use this for initialization
    void Start()
    {
        ThePlayer = this.gameObject;
        ThePlayerRigidbody2D = this.GetComponent<Rigidbody2D>();
        GroundCheck = GameObject.Find(this.name + "/GroundCheck").transform;
        WallCheck = GameObject.Find(this.name + "/WallCheck").transform;
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (DashForce > 1)
            DashEffect.SetActive(true);

        if (DashForce <= 1)
            DashEffect.SetActive(false);

        if (!IsGrounded)
        {
            Anim.SetFloat("vSpeed", (ThePlayerRigidbody2D.velocity.y * 0.2f));
        }


    }

    void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, WhatIsGround);
        TouchingWall = Physics2D.OverlapCircle(WallCheck.position, WallRadius, WhatIsWall);


        // START ----- SET ANIMS
        Anim.SetBool("Ground", IsGrounded);
        if (IsGrounded)
        {
            Anim.SetBool("DoubleJump", false);
        }
        Anim.SetFloat("DashForce", DashForce);
        // Anim.SetBool("Wall", TouchingWall);
        // END ----- SET ANIMS

// KEYBOARDS

        #if UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT
        Move(Input.GetAxisRaw("Horizontal"));
        MoveControl = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        #endif


// TOUCH CONTROLS

        // #if UNITY_ANDROID || UNITY_IPHONE || UNITY_BLACKBERRY || UNITY_WINRT
        float LeftRight = 0;
        float Speed = 50f;

        // sets the Move variable to equal player input
        float MoveAnimation = Input.touchCount;
        if (Input.touchCount == 0)
        {
            TouchJumpHeld = false;
            TouchDashHeld = false;
            DashForce = 1;
            Move(0.0f);
        }

        if (Input.touchCount == 1 || Input.touchCount == 2)
        {
            foreach (Touch touch in Input.touches)
            {
                // touch x position is bigger than half of the screen, moving right
                if (Input.GetTouch(0).position.x > Screen.width * 0.5)
                    LeftRight = 1;
                // touch x position is smaller than half of the screen, moving left
                else if (Input.GetTouch(0).position.x < Screen.width * 0.5)
                    LeftRight = -1;

                MoveControl = LeftRight;
                // feeds the Speed variable into the animator component based on the absolute value of Move
                Anim.SetFloat("Speed", Mathf.Abs(MoveAnimation));
                Move(LeftRight);
            }

            if ((Input.GetTouch(0).position.y > Screen.height * 0.5 && IsGrounded && !TouchJumpHeld))
            {
                IsDoubleJumping = false;
                TouchJumpHeld = true;
                Anim.SetBool("Ground", false);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                Jump();
            }

            if ((Input.GetTouch(0).position.y > Screen.height * 0.5 && !IsGrounded && !TouchJumpHeld && !IsDoubleJumping))
            {
                IsDoubleJumping = true;
                TouchJumpHeld = true;
                Anim.SetBool("Ground", false);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                DoubleJump();
            }

            // if sprite is moving and facing left
            if (LeftRight > 0 && !FacingRight)
                //flip sprite facing
                FlipSprite();
            // else if sprite is moving and facing right
            else if (LeftRight < 0 && FacingRight)
                //flip sprite facing
                FlipSprite();
        }

        Anim.SetFloat("Speed", Mathf.Abs(MoveAnimation));

        // DASHING
        if (Input.touchCount == 2)
        {
            // touch x position is bigger than half of the screen, moving right
            if (Input.GetTouch(0).position.x > Screen.width * 0.5 && Input.GetTouch(1).position.x > Screen.width * 0.5 && !TouchDashHeld)
            {
                if (Time.time > DashStart + DashCooldownPeriod)
                {
                    TouchDashHeld = true;
                    LeftRight = 1;
                    Dash(LeftRight);
                }
            }
            // touch x position is smaller than half of the screen, moving left
            else if (Input.GetTouch(0).position.x < Screen.width * 0.5 && Input.GetTouch(1).position.x < Screen.width * 0.5 && !TouchDashHeld)
            {
                if (Time.time > DashStart + DashCooldownPeriod)
                {
                    TouchDashHeld = true;
                    LeftRight = -1;
                    Dash(LeftRight);
                }
            }
        }
        // #endif

    }

    public void Move(float HorizontalInput)
    {
        Movement = ThePlayerRigidbody2D.velocity;
        if (CanMoveInAir && !TouchingWall)
        { // Checks if can move in air AND if not touching wall
            Movement.x = HorizontalInput * (Speed * DashForce) * Time.deltaTime;
        }
        if (!CanMoveInAir && !IsGrounded && !TouchingWall)
        { // Checks if can move in air AND if not touching ground OR wall
            return;
        }
        ThePlayerRigidbody2D.velocity = Movement;
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
        if (IsGrounded)
        {
            ThePlayerRigidbody2D.velocity += JumpHeight * Vector2.up * Time.deltaTime;
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(JumpSound, 0.4f);
        }
    }

    void DoubleJump()
    {

        if (!IsGrounded)
        {
            Anim.SetBool("DoubleJump", true);
            ThePlayerRigidbody2D.velocity = new Vector2(ThePlayerRigidbody2D.velocity.x, 0);
            ThePlayerRigidbody2D.velocity += JumpHeight * Vector2.up * Time.deltaTime;
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(DoubleJumpSound, 0.7f);
        }
    }

    void Dash(float HorizontalInput)
    {
        if (!IsDashing)
        {
            DashStart = Time.time;
            IsDashing = true;
            DashForce = 3;
            Movement.x = HorizontalInput * (Speed * DashForce) * Time.deltaTime;
            ThePlayerRigidbody2D.velocity = Movement;
            Invoke("DashExtent", DashTime);
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(DashSound, 0.4f);
        }
    }

    void DashExtent()
    {
        IsDashing = false;
        DashForce = 1;
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Apple")
            Destroy(coll.gameObject);
        
    }
}

/*
switch (touch.phase)
                {
                    case TouchPhase.Ended:
                        if (!DoubleJump && !Grounded)
                            DoubleJump = true;
                        break;
                    default:
                        break;
                }
*/
