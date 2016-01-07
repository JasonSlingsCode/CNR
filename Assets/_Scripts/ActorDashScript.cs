using UnityEngine;
using System.Collections;

public class ActorDashScript : MonoBehaviour
{
    public GameObject ControllingActor;
    public ActorControllerScript ActorBrain;
    public float DashStart;
    private bool TouchDashHeld = false;
    public float DashForce;
    public float DashTime;
    public bool IsDashing;
    public float DashCooldownPeriod;
    public GameObject DashEffect;
    public AudioClip DashSound;

    private float MoveSpeed;

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
        if (DashForce > 1)
            DashEffect.SetActive(true);
        
        if (DashForce <= 1)
            DashEffect.SetActive(false);
    }

    public void Dash(float HorizontalInput)
    {
        if (!IsDashing)
        {
            DashStart = Time.time;
            IsDashing = true;
            DashForce = 3;
            ActorBrain.Movement.x = HorizontalInput * (MoveSpeed * DashForce) * Time.deltaTime;
            ActorBrain.rb2D.velocity = ActorBrain.Movement;
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
	
}
