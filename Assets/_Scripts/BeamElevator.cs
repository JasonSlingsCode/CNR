using UnityEngine;
using System.Collections;

public class BeamElevator : MonoBehaviour {

	public SliderJoint2D slide;

	// Use this for initialization
	void Start () {

		slide.useLimits = false;
		slide = GetComponent<SliderJoint2D>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			slide.useLimits = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			slide.useLimits = false;
		}
	}
}

