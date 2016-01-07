using UnityEngine;
using System.Collections;

public class TrapDoor : MonoBehaviour
{
    public GameObject TheTrapDoor;
    public GameObject TheLock;
    public bool Locked = true;
    public float ResetTimer;

    private Vector2 OriginalPosition;
    private Quaternion OriginalRotation;

    // Use this for initialization
    void Start()
    {
        OriginalPosition = this.gameObject.transform.localPosition;
        OriginalRotation = this.gameObject.transform.rotation;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Locked == false)
        {
            float dist = Vector3.Distance(TheLock.transform.position, TheTrapDoor.transform.position);
            print (dist);
            if (dist >= 0.0001 && dist <= 0.009)
            {
                print("Trap Door Locked");
                TheTrapDoor.GetComponent<Rigidbody2D>().isKinematic = true;
                Locked = true;
                TheTrapDoor.GetComponent<HingeJoint2D>().useMotor = false;
                this.gameObject.transform.localPosition = OriginalPosition;
                this.gameObject.transform.rotation = OriginalRotation;
            }
        }
    }

    public void UnlockTrapDoor()
    {
        print("Unlocked");
       

        TheTrapDoor.GetComponent<Rigidbody2D>().isKinematic = false;
        TheTrapDoor.GetComponent<HingeJoint2D>().useMotor = false;
        TheTrapDoor.GetComponent<Rigidbody2D>().AddTorque(-100, ForceMode2D.Impulse);
        Invoke("Reset", ResetTimer);
    }

    void Reset()
    {
        Locked = false;
        print("Reset");
        TheTrapDoor.GetComponent<HingeJoint2D>().useMotor = true;
        // TheTrapDoor.GetComponent<HingeJoint2D>().motor.motorSpeed = MotorStrength;
    }
}
