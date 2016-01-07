using UnityEngine;
using System.Collections;

public class FlickeringLightFX : MonoBehaviour {

    public Light FlickeringLight;
    public float RandomFlicker;

    // Use this for initialization
    void Start () {
        FlickeringLight = GetComponent<Light>();
        FlickerOn();
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    void FlickerOn()
    {
        FlickeringLight.intensity = 2;
        RandomFlicker = Random.Range(0.01f, 0.3f);
        Invoke("FlickerOff", RandomFlicker);
    }

    void FlickerOff()
    {
        FlickeringLight.intensity = 0;
        RandomFlicker = Random.Range(0.01f, 0.3f);
        Invoke("FlickerOn", RandomFlicker);
    }
}
