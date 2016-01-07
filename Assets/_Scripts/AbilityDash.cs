using UnityEngine;
using System.Collections;

public class AbilityDash : MonoBehaviour
{
    public float DashStart;
    public float DashForce;
    public float DashCooldownPeriod;
    private float LeftRight;
    public bool TouchDashHeld = false;

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            TouchDashHeld = false;
        }

        if (Input.touchCount == 2 && !TouchDashHeld)
        {
            // touch x position is bigger than half of the screen, moving right
            if (Input.GetTouch(0).position.x > Screen.width * 0.5 && Input.GetTouch(1).position.x > Screen.width * 0.5)
            {
                if (Time.time > DashStart + DashCooldownPeriod)
                {
                    TouchDashHeld = true;
                    LeftRight = 1;
                    Dash(LeftRight);
                    print ("DASH Right");
                }
            }
            // touch x position is smaller than half of the screen, moving left
            else if (Input.GetTouch(0).position.x < Screen.width * 0.5 && Input.GetTouch(1).position.x < Screen.width * 0.5)
            {
                if (Time.time > DashStart + DashCooldownPeriod)
                {
                    TouchDashHeld = true;
                    LeftRight = -1;
                    Dash(LeftRight);
                    print ("DASH Left");
                }
            }

            TouchDashHeld = false;
        }
    }

    void Dash(float HorizontalInput)
    {
        DashStart = Time.time;
        // Vector2 DashVector = this.gameObject.GetComponent<Rigidbody2D>().velocity;
        // DashVector.x = HorizontalInput * DashForce * (Time.deltaTime * 40);
        // this.gameObject.GetComponent<Rigidbody2D>().velocity = DashVector;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        // GetComponent<Rigidbody2D>().AddForce(new Vector2((HorizontalInput * DashForce), 2000));
        GetComponent<Rigidbody2D>().AddForce(new Vector2((HorizontalInput * 20000),2000));
    }
}
