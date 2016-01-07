using UnityEngine;
using System.Collections;

public class PlayerButtonControls : MonoBehaviour
{
    public GameObject ThePlayer;
    public Rigidbody2D ThePlayerRigidbody2D;
    public Transform GroundCheck;
    public Transform WallCheck;
    private float GroundRadius = 0.5f;
    private float WallRadius = 0.5f;
    public LayerMask WhatIsGround;
    public bool IsGrounded = false;
    public LayerMask WhatIsWall;
    public bool TouchingWall = false;
    public bool CanMoveInAir = true;
    [Header("Player Animations")]
    private Animator Anim;
    [Header("Player Movement")]
    private Vector2
        Movement;
    private float MoveControl;
    private float LeftRight = 0;
    private bool FacingRight = true;
    public float Speed;
    [Header("Player Jumping")]
    public float
        JumpHeight = 500f;
    private bool TouchJumpHeld = false;
    private bool IsJumping;
    private bool IsDoubleJumping;
    private int JumpCount;
    [Header("Player Abilities")]
    public GameObject DashEffect;
    public float
        DashStart;
    private bool
        TouchDashHeld = false;
    public float DashForce;
    public float DashTime;
    public bool IsDashing;
    public float DashCooldownPeriod;
    private float hInput;
    [Header("Audio")]
    public AudioClip
        JumpSound;
    public AudioClip DoubleJumpSound;
    public AudioClip DashSound;
    
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

        if (Input.touchCount == 0)
        {
            TouchJumpHeld = false;
            TouchDashHeld = false;
            DashForce = 1;
        }

        Move(hInput);

// START ----- SET ANIMS
        Anim.SetBool("Ground", IsGrounded);
        Anim.SetFloat("Speed", Mathf.Abs(hInput));
        if (IsGrounded)
        {
            Anim.SetBool("DoubleJump", false);
            IsJumping = false;
            IsDoubleJumping = false;
        }
        Anim.SetFloat("DashForce", DashForce);
        // Anim.SetBool("Wall", TouchingWall);
// END ----- SET ANIMS
    }

    public void ButtonRelease()
    {
        TouchDashHeld = false;
        DashForce = 1;
    }

    public void StartMoving(float HorizontalInput)
    {
        hInput = HorizontalInput;
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

    public void StartJumping(bool JumpHeld)
    {
        if (JumpHeld == true && IsGrounded && !TouchJumpHeld)
        {
            IsDoubleJumping = false;
            TouchJumpHeld = true;
            Anim.SetBool("Ground", false);
            ThePlayerRigidbody2D.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            Jump();
        } else if (JumpHeld == true && !IsGrounded && !TouchJumpHeld && !IsDoubleJumping)
        {
            IsDoubleJumping = true;
            TouchJumpHeld = true;
            Anim.SetBool("Ground", false);
            ThePlayerRigidbody2D.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            DoubleJump();
        } else
        {
            TouchJumpHeld = false;
        }
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            ThePlayerRigidbody2D.velocity = (JumpHeight * Vector2.up);
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(JumpSound, 0.4f);
        }
    }

    public void DoubleJump()
    {
        if (!IsGrounded)
        {
            Anim.SetBool("DoubleJump", true);
            ThePlayerRigidbody2D.velocity = (JumpHeight * Vector2.up);
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(DoubleJumpSound, 0.7f);
        }
    }

    public void Dash(float HorizontalInput)
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

    public void SwordAttack()
    {

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
}
