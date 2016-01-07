using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpeechBubbles : MonoBehaviour
{

    public GameObject ThePlayer;
    public GameObject SpeechBubbleTrigger;
    public SpeechBubbles SpeechBubbleArray;
    public GameObject SpeechBubblePanel;
    private GameObject PanelToFade;
    public Text SpeechBubbleText;
    private Text TextToFade;
    private GameObject NodeToDelete;
    public List<Transform> SpeechNodes = new List<Transform>();
    public float FadeTimer;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform.parent)
            SpeechNodes.Add(child);
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == ThePlayer)
        {
            print("COLLISION");

            if (this.gameObject.name == SpeechNodes [0].name)
            {
                SpeechBubbleText.text = "Look... My cage is unlocked.\n\nMaybe I can get out of here...";
                SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                SpeechBubbleText.CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                PanelToFade = SpeechBubblePanel;
                TextToFade = SpeechBubbleText;
                NodeToDelete = this.gameObject;
                StartCoroutine(FadeOut(PanelToFade, TextToFade));
            }

            if (this.gameObject.name == SpeechNodes [1].name)
            {
                SpeechBubbleText.text = "This button is the light switch. \n\nLet's turn on the lights.";
                SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                SpeechBubbleText.CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                PanelToFade = SpeechBubblePanel;
                TextToFade = SpeechBubbleText;
                NodeToDelete = this.gameObject;
                StartCoroutine(FadeOut(PanelToFade, TextToFade));
            }

            if (this.gameObject.name == SpeechNodes [2].name)
            {
                SpeechBubbleText.text = "Hmm.\n\nI wonder what this lever is for...";
                SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                SpeechBubbleText.CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                PanelToFade = SpeechBubblePanel;
                TextToFade = SpeechBubbleText;
                NodeToDelete = this.gameObject;
                StartCoroutine(FadeOut(PanelToFade, TextToFade));
            }

            if (this.gameObject.name == SpeechNodes [3].name)
            {
                SpeechBubbleText.text = "My tail circuits are showing I have low energy.\n\nI should find something to eat.";
                SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                SpeechBubbleText.CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                PanelToFade = SpeechBubblePanel;
                TextToFade = SpeechBubbleText;
                NodeToDelete = this.gameObject;
                StartCoroutine(FadeOut(PanelToFade, TextToFade));
            }

            if (this.gameObject.name == SpeechNodes [4].name)
            {
                SpeechBubbleText.text = "Mmmm... \n\nI smell sushi in this trash can!";
                SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                SpeechBubbleText.CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                PanelToFade = SpeechBubblePanel;
                TextToFade = SpeechBubbleText;
                NodeToDelete = this.gameObject;
                StartCoroutine(FadeOut(PanelToFade, TextToFade));
            }
            
            if (this.gameObject.name == SpeechNodes [5].name)
            {
                SpeechBubbleText.text = "How did all these lab animals get loose?";
                SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                SpeechBubbleText.CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                PanelToFade = SpeechBubblePanel;
                TextToFade = SpeechBubbleText;
                NodeToDelete = this.gameObject;
                StartCoroutine(FadeOut(PanelToFade, TextToFade));
            }
            
            if (this.gameObject.name == SpeechNodes [6].name)
            {
                SpeechBubbleText.text = "This computer is connected to that hatch.\n\nI bet the sliders will get it open.";
                SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                SpeechBubbleText.CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                PanelToFade = SpeechBubblePanel;
                TextToFade = SpeechBubbleText;
                NodeToDelete = this.gameObject;
                StartCoroutine(FadeOut(PanelToFade, TextToFade));
            }
            
            if (this.gameObject.name == SpeechNodes [7].name)
            {
                SpeechBubbleText.text = "What happened here? \n\nSomething must have gone horribly wrong in the lab...";
                SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                SpeechBubbleText.CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                PanelToFade = SpeechBubblePanel;
                TextToFade = SpeechBubbleText;
                NodeToDelete = this.gameObject;
                StartCoroutine(FadeOut(PanelToFade, TextToFade));
            }
            
            if (this.gameObject.name == SpeechNodes [8].name)
            {
                SpeechBubbleText.text = "Mmmm... \n\nI'll escape out of here soon enough...";
                SpeechBubblePanel.GetComponent<Image>().CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                SpeechBubbleText.CrossFadeAlpha(255f, (Time.deltaTime * 10000), false);
                PanelToFade = SpeechBubblePanel;
                TextToFade = SpeechBubbleText;
                NodeToDelete = this.gameObject;
                StartCoroutine(FadeOut(PanelToFade, TextToFade));
            }
        }
    }

    IEnumerator FadeOut(GameObject FadingPanel, Text FadingText)
    {
        yield return new WaitForSeconds(FadeTimer);
        FadingPanel.GetComponent<Image>().CrossFadeAlpha(0f, (Time.deltaTime * 100), false);
        FadingText.CrossFadeAlpha(0f, (Time.deltaTime * 100), false);
        NodeToDelete.SetActive(false);
    }
}
