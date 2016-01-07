using UnityEngine;
using System.Collections;

public class ActorAttackScript : MonoBehaviour
{

    public GameObject ControllingActor;
    public ActorControllerScript ActorBrain;

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
    
    }
}
