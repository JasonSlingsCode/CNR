using UnityEngine;
using System.Collections;

public class ActorJumpScript : MonoBehaviour
{

    public GameObject ControllingActor;
    public ActorControllerScript ActorBrain;

    private float JumpHeight;
    private bool TouchJumpHeld = false;
    private bool IsJumping;
    private bool IsDoubleJumping;
    private int JumpCount;

    public AudioClip JumpSound;
    public AudioClip DoubleJumpSound;

    void Awake()
    {
        ControllingActor = this.gameObject;
        ActorBrain = GetComponent<ActorControllerScript>();
    }

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
        JumpHeight = ActorBrain.JumpHeight;
    }


    
    public void Jump()
    {
        if (ActorBrain.IsGrounded)
        {
            ActorBrain.rb2D.velocity = (JumpHeight * Vector2.up);
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(JumpSound, 0.4f);
        }
    }
    
    public void DoubleJump()
    {
        if (!ActorBrain.IsGrounded)
        {
            ActorBrain.Anim.SetBool("DoubleJump", true);
            ActorBrain.rb2D.velocity = (JumpHeight * Vector2.up);
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(DoubleJumpSound, 0.7f);
        }
    }
}
