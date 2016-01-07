using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour
{
	public Platform ThePlatform;
	public GameObject TheButtonBase;
	public bool ButtonIsDown;
	public int SwitchTimer;
	private float Proximity;
	public GameObject TriggeringObject;
	
	void Awake ()
	{
		
	}

	void Update ()
	{
		if (TriggeringObject != null) {
			Proximity = Vector2.Distance (TriggeringObject.transform.position, this.gameObject.transform.position);
			print (Proximity);
			if (Proximity >= 5) {
				TriggeringObject = null;
				Invoke ("ButtonUp", SwitchTimer);
				ThePlatform.Invoke ("SwitchState", SwitchTimer);
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject != TheButtonBase) {
			print ("Button Press");
			if (!ButtonIsDown) {
				TriggeringObject = other.gameObject;
				ButtonDown ();
			}
		}
	}

	void ButtonDown ()
	{
		print ("Button Down");
		ButtonIsDown = true;
		ThePlatform.SwitchState ();
	}
	
	void ButtonUp ()
	{
		print ("Button Up");
		ButtonIsDown = false;
	}
	
	
}