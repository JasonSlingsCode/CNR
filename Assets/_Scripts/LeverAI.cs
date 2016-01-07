using UnityEngine;
using System.Collections;

public class LeverAI : MonoBehaviour
{
    public bool LeverIsOn;

    public Color LeverOnColor;
    public Color LeverOffColor;

    void Awake()
    {

    }

    void Update()
    {
        switch (LeverIsOn)
        {
            case true:
                GetComponent<SpriteRenderer>().color = LeverOnColor;
                break;
            case false:
                GetComponent<SpriteRenderer>().color = LeverOffColor;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "LeverOnPoint")
        {
            if (!LeverIsOn)
            {
                LeverOn();
            }
        }
        else if (other.gameObject.name == "LeverOffPoint")
        {
            if (LeverIsOn)
            {
                LeverOff();
            }
        }
    }

    void LeverOn()
    {
        LeverIsOn = true;
    }

    void LeverOff()
    {
        LeverIsOn = false;
    }


}