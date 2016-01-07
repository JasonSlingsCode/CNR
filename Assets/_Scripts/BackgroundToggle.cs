using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BackgroundToggle : MonoBehaviour
{

	public GameObject BackgroundImage;
	public ButtonChangeState ButtonState;
	private bool Active;
    public GameObject LightController;
    public List<Transform> LightGizmos = new List<Transform>();
    private Light WallLight;

	// Use this for initialization
	void Start ()
	{
        foreach (Transform child in LightController.transform)
            LightGizmos.Add(child);
        // WallLight = LightControl.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
	{
			// BackgroundImage.gameObject.SetActive (ButtonState.ButtonState);
        LightController.SetActive(ButtonState.ButtonState);
	}
}
