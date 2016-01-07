using UnityEngine;
using System.Collections;

public class SpeechBubbleFollow : MonoBehaviour {

	public GameObject Target;
	public Vector3 PositionOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.localPosition = Target.transform.position + PositionOffset;
	}
}
