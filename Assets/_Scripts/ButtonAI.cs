using UnityEngine;
using System.Collections;

public class ButtonAI : MonoBehaviour
{
    public bool ButtonIsDown;
    public int SwitchTimer;

    public Color ButtonOn;
    public Color ButtonOff;

    public AudioClip sound;
    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (ButtonIsDown)
        {
            case true:
                GetComponent<SpriteRenderer>().color = ButtonOn;
                break;
            case false:
                GetComponent<SpriteRenderer>().color = ButtonOff;
                break;
            default:
                break;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Button_Base")
        {
            print ("Switch");
            if (!ButtonIsDown)
            {
                ButtonDown();
            }
        }
    }
    
    void ButtonDown()
    {
        print("Button Down");
        ButtonIsDown = true;
        // ThePlatform.SwitchState();
        Invoke("ButtonUp", SwitchTimer);
        PlaySound();
    }
    
    void ButtonUp()
    {
        print("Button Up");
        ButtonIsDown = false;
    }

    void PlaySound()
    {
        audio.PlayOneShot(sound, 0.1F);
    }

}