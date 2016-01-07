using UnityEngine;
using System.Collections;

public class ActorControllerScript : MonoBehaviour
{
    public GameObject ThePlayer;
    public Rigidbody2D rb2D;
    public Transform GroundCheck;
    public Transform WallCheck;
    private float GroundRadius = 0.5f;
    private float WallRadius = 0.5f;
    public LayerMask WhatIsGround;
    public bool IsGrounded = false;
    public LayerMask WhatIsWall;
    public bool IsTouchingWall = false;
    public bool CanMoveInAir = true;
    [Header("Player Animations")]
    public Animator
        Anim;
    [Header("Player Health")]
    public bool
        isHurt;
    private HealthScript HealthController;
    public int HP;
    public int MaxHP;
    [Header("Player Movement")]
    private ActorMoveScript
        MoveController;
    public Vector2
        Movement;
    private float MoveControl;
    public bool FacingRight = true;
    public float MoveSpeed;
    [Header("Player Jumping")]
    private ActorJumpScript
        JumpController;
    public float
        JumpHeight;
    private bool TouchJumpHeld = false;
    private bool IsJumping;
    private bool IsDoubleJumping;
    private int JumpCount;
    [Header("Player Abilities")]
    public float
        DashStart;
    private bool
        TouchDashHeld = false;
    public float DashForce;
    public float DashTime;
    public bool IsDashing;

    public float AttackSpeed;
    public bool ReadyToAttack = true;
    public bool Attacking;
    public AudioClip[] SwordSounds = new AudioClip[1];

    public float DashCooldownPeriod;
    private ActorAttackScript AttackController;
    private float hInput;
    private bool KeepMoving = false;
    
    // Use this for initialization
    void Start()
    {
        ThePlayer = this.gameObject;
        rb2D = this.GetComponent<Rigidbody2D>();
        HealthController = this.GetComponent<HealthScript>();
        MoveController = this.GetComponent<ActorMoveScript>();
        JumpController = this.GetComponent<ActorJumpScript>();
        AttackController = this.GetComponent<ActorAttackScript>();
        GroundCheck = GameObject.Find(this.name + "/GroundCheck").transform;
        WallCheck = GameObject.Find(this.name + "/WallCheck").transform;
        Anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!IsGrounded)
        {
            Anim.SetFloat("vSpeed", (rb2D.velocity.y * 0.2f));
        }
        isHurt = HealthController.GotHurt;
    }
    
    void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, WhatIsGround);
        IsTouchingWall = Physics2D.OverlapCircle(WallCheck.position, WallRadius, WhatIsWall);
        
        if (Input.touchCount == 0)
        {
            TouchJumpHeld = false;
            TouchDashHeld = false;
            DashForce = 1;
        }

        if (!isHurt)
        {
            MoveController.Move(hInput);
        }
        
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
        Anim.SetBool("Attacking", Attacking);
        // Anim.SetBool("Wall", IsTouchingWall);
        // END ----- SET ANIMS

        if (KeepMoving == true)
        {
            hInput = 1;
            MoveController.Move(hInput);
        }
    }

    public void StartMoving(float HorizontalInput)
    {

        hInput = HorizontalInput;

    }

    public void StartJumping(bool JumpHeld)
    {
        if (!isHurt)
        {
            if (JumpHeld == true && IsGrounded && !TouchJumpHeld)
            {
                IsDoubleJumping = false;
                TouchJumpHeld = true;
                Anim.SetBool("Ground", false);
                rb2D.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                JumpController.Jump();
            } else if (JumpHeld == true && !IsGrounded && !TouchJumpHeld && !IsDoubleJumping)
            {
                IsDoubleJumping = true;
                TouchJumpHeld = true;
                Anim.SetBool("Ground", false);
                rb2D.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                JumpController.DoubleJump();
            } else
            {
                TouchJumpHeld = false;
            }
        }
    }
    
    public void ButtonRelease()
    {
        TouchDashHeld = false;
        DashForce = 1;
    }
    
    public void SwordAttack(bool isAttacking)
    {
        if (isAttacking == true)
        {
            if (ReadyToAttack == true)
            {
                StartCoroutine("SwordSwing");

                Attacking = isAttacking;
            }
        }

        if (isAttacking == false)
        {
            Attacking = isAttacking;
        }
    }

    IEnumerator SwordSwing()
    {
        if (ReadyToAttack == true)
        {
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(SwordSounds[Random.Range(1, SwordSounds.Length)], 0.4f);
            yield return new WaitForSeconds(0.03f);
            Attacking = false; // for animation
            ReadyToAttack = false;
            yield return new WaitForSeconds(AttackSpeed);
            ReadyToAttack = true;
        }
    }
    
    public void FlipSprite()
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
        if (other.gameObject.tag == "EndLevel")
        {
            KeepMoving = true;
        }
    }
}
