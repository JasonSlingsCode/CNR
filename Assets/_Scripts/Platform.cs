using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour 
{
	public Transform _platform;
    public Transform startPoint;
    public Transform endPoint;
	public GameObject TriggerMechanism;
    public enum UseCase { Auto, Manual}
    public UseCase useCase;
    [SerializeField]
    private float _moveSpeed = 1;
    private bool _return;
    [SerializeField]
    private bool _switch = true;
	public float SwitchTimer;
	private bool _switched;
    private Rigidbody2D _platformRigidBody = null;
	public AudioClip sound;
	AudioSource audio;
	bool SoundPlayed;
    
    
    void Start()
    {
        _platformRigidBody = _platform.transform.GetComponent<Rigidbody2D>();
        _platformRigidBody.mass = 200;
		audio = GetComponent<AudioSource>();
    }
    
    public void SwitchState()
    {
        _switch = (_switch == true) ? false : true;
    }
    
    void SetState(bool state)
    {
        _switch = state;
        
    }

	// Update is called once per frame
	void Update ()
	{
		
		// Button AI
		if (TriggerMechanism.tag == "Button")
		{
			if (TriggerMechanism.GetComponent<ButtonAI>().ButtonIsDown == true)
			{
				if (_switched == false)
				{
					SwitchState();
					_switched = true;
					if (SwitchTimer > 0) Invoke("SwitchState", SwitchTimer);
				}
				if (SoundPlayed == false)
				{
					PlaySound();
					SoundPlayed = true;
				}
			}
			
			if (TriggerMechanism.GetComponent<ButtonAI>().ButtonIsDown == false)
			{
				if (_switched == true)
				{
					// SwitchState();
					_switched = false;
				}
				if (SoundPlayed == true)
				{
					//PlaySound();
					SoundPlayed = false;
				}
			}
		}
		
		// Lever AI
		if (TriggerMechanism.tag == "Lever")
		{
			if (TriggerMechanism.GetComponent<LeverAI>().LeverIsOn == true)
			{
				if (_switched == false)
				{
					SwitchState();
					_switched = true;
					if (SwitchTimer > 0) Invoke("SwitchState", SwitchTimer);
				}
				if (SoundPlayed == false)
				{
					PlaySound();
					SoundPlayed = true;
				}
			}
			
			if (TriggerMechanism.GetComponent<LeverAI>().LeverIsOn == false)
			{
				if (_switched == true)
				{
					// SwitchState();
					_switched = false;
				}
				if (SoundPlayed == true)
				{
					//PlaySound();
					SoundPlayed = false;
				}
			}
		}

		// Slider AI
		if (TriggerMechanism.tag == "Slider")
		{
			if (TriggerMechanism.GetComponent<SliderAI>().SliderIsDown == true)
			{
				if (_switched == false)
				{
					SwitchState();
					_switched = true;
					if (SwitchTimer > 0) Invoke("SwitchState", SwitchTimer);
				}
				if (SoundPlayed == false)
				{
					PlaySound();
					SoundPlayed = true;
				}
			}
			
			if (TriggerMechanism.GetComponent<SliderAI>().SliderIsDown == false)
			{
				if (_switched == true)
				{
					// SwitchState();
					_switched = false;
				}
				if (SoundPlayed == true)
				{
					//PlaySound();
					SoundPlayed = false;
				}
			}
		}
	}
    
    void FixedUpdate()
    {
        if (useCase == UseCase.Auto)
        {
            if (_platform.position == endPoint.position) _return = true;
            if (_platform.position == startPoint.position) _return = false;
            
            if (_return) _platform.position = Vector3.Lerp(_platform.position, startPoint.position, _moveSpeed * Time.fixedDeltaTime);
			if (!_return) _platform.position = Vector3.Lerp(_platform.position, endPoint.position, _moveSpeed * Time.fixedDeltaTime);
        }
        if (useCase == UseCase.Manual)
        {          
            if (_switch) _platform.position = Vector3.MoveTowards(_platform.position, startPoint.position, _moveSpeed * Time.fixedDeltaTime);
            if (!_switch) _platform.position = Vector3.MoveTowards(_platform.position, endPoint.position, _moveSpeed * Time.fixedDeltaTime);       
        }
        
    }

	void PlaySound()
	{
		audio.PlayOneShot(sound, 0.2F);
	}
	/*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(startPoint.position, transform.localScale);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(endPoint.position, transform.localScale);
    }
    */
}