using UnityEngine;
using System.Collections;

public class VentDoor : MonoBehaviour
{
    public GameObject[] Sliders = new GameObject[]
    {

    };
    public Color GreenSlider;
    public Color RedSlider;

    public bool Lock_01, Lock_02, Lock_03, Lock_04;

    public GameObject TheVentDoor;

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        foreach (GameObject slider in Sliders)
        {
            if (slider.transform.localPosition.y > 2f)
            {
                slider.GetComponent<SpriteRenderer>().color = RedSlider;
            }
            if (slider.transform.localPosition.y <= 2f)
            {
                slider.GetComponent<SpriteRenderer>().color = GreenSlider;
            }
        }

        if (Sliders [0].GetComponent<SpriteRenderer>().color == GreenSlider)
            Lock_01 = true;
        else if (Sliders [0].GetComponent<SpriteRenderer>().color == RedSlider)
            Lock_01 = false;

        if (Sliders [1].GetComponent<SpriteRenderer>().color == GreenSlider)
            Lock_02 = true;
        else if (Sliders [1].GetComponent<SpriteRenderer>().color == RedSlider)
            Lock_02 = false;

        if (Sliders [2].GetComponent<SpriteRenderer>().color == GreenSlider)
            Lock_03 = true;
        else if (Sliders [2].GetComponent<SpriteRenderer>().color == RedSlider)
            Lock_03 = false;

        if (Sliders [3].GetComponent<SpriteRenderer>().color == GreenSlider)
            Lock_04 = true;
        else if (Sliders [3].GetComponent<SpriteRenderer>().color == RedSlider)
            Lock_04 = false;

        if (Lock_01 && Lock_02 && Lock_03 && Lock_04)
        {
            TheVentDoor.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }


}
