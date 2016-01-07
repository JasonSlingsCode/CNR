using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpeechBubbleList : MonoBehaviour
{

	public GameObject SpeechBubblePanel;
	public Text SpeechBubbleText;

	public string[] SpeechList = new string[]
	{
		"HELLO WORLD",
		"HELLO AGAIN"
	};
	void Awake()
	{
		SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(0f, 0f, false);
		SpeechBubbleText.CrossFadeAlpha(0f, 0f, false);
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
