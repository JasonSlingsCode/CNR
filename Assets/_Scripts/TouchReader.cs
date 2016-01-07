using UnityEngine;
using System.Collections;

public class TouchReader : MonoBehaviour
{
    private TouchReader reader;
    bool enabled = false;

    // Use this for initialization
    void Start ()
    {
        reader = GetComponent<TouchReader>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        
    }
    
    void OnGUI()
    {
        if (enabled)
        {
            // goes through each touch in the array
            foreach (Touch touch in Input.touches)
            {
                // creates a string called "message"
                string message = "";
                // adds a new line for ach of the below pieces of info
                message += "ID: " + touch.fingerId + "\n";
                message += "Phase: " + touch.phase + "\n";
                message += "Tap Count: " + touch.tapCount + "\n";
                message += "Pos X: " + touch.position.x + "\n";
                message += "Pos Y: " + touch.position.y + "\n";
            
                // creates a new message box for each touch
                int num = touch.fingerId;
                GUI.Label(new Rect(0 + 130 * num, 0, 120, 100), message);
            }
        }
    }

    public void ReaderToggle()
    {
        enabled = !enabled;
    }
}
