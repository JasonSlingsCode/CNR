using UnityEngine;
using System.Collections;

public class ButtonBoxTrigger : MonoBehaviour {

	public AudioClip sound;
	AudioSource audio;
	bool SoundPlayed;
	
	// Use this for initialization
	void Start ()
	{
		audio = GetComponent<AudioSource>();
		GetComponent<SliderJoint2D>().useMotor = false;
	}
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "BoxTrigger")
		{
			GetComponent<SliderJoint2D>().useMotor = true;
			GetComponent<DoorOpen>().enabled = false;
			if (SoundPlayed == false)
			{
				PlaySound();
				SoundPlayed = true;
			}
		}
	}
	
	
	
	void PlaySound()
	{
		audio.PlayOneShot(sound, 0.2F);
	}
}
