using UnityEngine;
using System.Collections;

public class ButtonChangeState : MonoBehaviour
{

	public Transform TheButton;
	public Transform StateChangePoint;
	public Transform StateResetPoint;
	public bool ButtonState;
	public bool ButtonIsReset = true;
	public Sprite[] ButtonStates = new Sprite[2];

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (TheButton.position == StateChangePoint.position && !ButtonState && ButtonIsReset) {
			ButtonState = !ButtonState;
			ButtonIsReset = false;
			TheButton.GetComponent<SpriteRenderer> ().sprite = ButtonStates [0];
		}

		if (TheButton.position == StateChangePoint.position && ButtonState && ButtonIsReset) {
			ButtonState = !ButtonState;
			ButtonIsReset = false;
			TheButton.GetComponent<SpriteRenderer> ().sprite = ButtonStates [1];
		}

		if (TheButton.position == StateResetPoint.position) {
			ButtonIsReset = true;
		}

	}
}

