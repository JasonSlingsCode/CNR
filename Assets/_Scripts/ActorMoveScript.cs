using UnityEngine;
using System.Collections;

public class ActorMoveScript : MonoBehaviour
{

    public GameObject ControllingActor;
    public ActorControllerScript ActorBrain;
    public float MoveSpeed;
    public float MaxMoveSpeed;

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
        MoveSpeed = ActorBrain.MoveSpeed;
    }

    /*
    public void StartMoving(float HorizontalInput)
    {
        hInput = HorizontalInput;
    }
    */

    public void Move(float HorizontalInput)
    {
        ActorBrain.Movement = ActorBrain.rb2D.velocity;
        if (ActorBrain.CanMoveInAir && !ActorBrain.IsTouchingWall)
        { // Checks if can move in air AND if not touching wall
            ActorBrain.Movement.x = HorizontalInput * (MoveSpeed * ActorBrain.DashForce) * Time.deltaTime;
        }
        if (!ActorBrain.CanMoveInAir && !ActorBrain.IsGrounded && !ActorBrain.IsTouchingWall)
        { // Checks if can move in air AND if not touching ground OR wall
            return;
        }
        ActorBrain.rb2D.velocity = ActorBrain.Movement;
        float MoveControl = HorizontalInput;
        // if sprite is moving and facing left
        if (MoveControl > 0 && !ActorBrain.FacingRight)
        {
            //flip sprite facing
            ActorBrain.FlipSprite();
        }
        // else if sprite is moving and facing right
        else if (MoveControl < 0 && ActorBrain.FacingRight)
        {
            //flip sprite facing
            ActorBrain.FlipSprite();
        }
    }


}
