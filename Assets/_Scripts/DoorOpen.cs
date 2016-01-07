using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour
{

    public GameObject TriggerMechanism;
    public AudioClip sound;
    AudioSource audio;
    bool SoundPlayed;

    // Use this for initialization
    void Start ()
    {
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        // Button AI
        if (TriggerMechanism.tag == "Button")
        {
            if (TriggerMechanism.GetComponent<ButtonAI>().ButtonIsDown == true)
            {
                GetComponent<SliderJoint2D>().useMotor = true;
                if (SoundPlayed == false)
                {
                    PlaySound();
                    SoundPlayed = true;
                }
            }

            if (TriggerMechanism.GetComponent<ButtonAI>().ButtonIsDown == false)
            {
                GetComponent<SliderJoint2D>().useMotor = false;
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
            print("TEST");
            if (TriggerMechanism.GetComponent<LeverAI>().LeverIsOn == true)
            {
                GetComponent<SliderJoint2D>().useMotor = true;
                if (SoundPlayed == false)
                {
                    PlaySound();
                    SoundPlayed = true;
                }
            }

            if (TriggerMechanism.GetComponent<LeverAI>().LeverIsOn == false)
            {
                GetComponent<SliderJoint2D>().useMotor = false;
                if (SoundPlayed == true)
                {
                    //PlaySound();
                    SoundPlayed = false;
                }
            }
        }
    }

    void PlaySound()
    {
            audio.PlayOneShot(sound, 0.2F);
    }
}
