using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    private FPSDisplay fps;
    private bool Paused = false;

    void Start()
    {
        fps = GetComponent<FPSDisplay>();
    }

    void Update()
    {
        if (!Paused)
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        if (Paused)
            deltaTime = 0.0f;
    }
    
    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        
        GUIStyle style = new GUIStyle();
        
        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(1f, 1f, 1f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }

    public void FPSToggle()
    {
        fps.enabled = !fps.enabled;
    }

    public void FPSPause()
    {
        Paused = !Paused;
    }
}