using UnityEngine;
using System.Collections;

public class ButtonTriggerSwitch : MonoBehaviour
{
    public bool TriggersAreSwitched;

    public GameObject TriggeredObject;
    // public GameObject FromTrigger;
    public GameObject ToTrigger;
    
    void Awake()
    {

    }
    
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Button_Base")
        {
            print ("Switch");
            if (!TriggersAreSwitched)
            {
                SwitchTriggers();
            }
        }
    }
    
    void SwitchTriggers()
    {
        TriggeredObject.GetComponent<Platform>().TriggerMechanism = ToTrigger;
        TriggeredObject.GetComponent<Platform>().SwitchState();
        TriggersAreSwitched = true;
    }
}