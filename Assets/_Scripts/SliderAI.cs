using UnityEngine;
using System.Collections;

public class SliderAI : MonoBehaviour
{
    public bool SliderIsDown;
    public float SwitchThreshold;
    public int SwitchTimer;

    public Color SliderOnColor;
    public Color SliderOffColor;


    void Update()
    {
        switch (SliderIsDown)
        {
            case true:
                GetComponent<SpriteRenderer>().color = SliderOnColor;
                break;
            case false:
                GetComponent<SpriteRenderer>().color = SliderOffColor;
                break;
            default:
                break;
        }

        if (transform.localPosition.y < SwitchThreshold)
        {
            SliderIsDown = true;
        }
        else if (transform.localPosition.y >= SwitchThreshold)
        {
            SliderIsDown = false;
        }
    }
}