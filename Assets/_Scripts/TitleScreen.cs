using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	public GameObject TitleCanvas;
	public GameObject LoadingCanvas;
	public GameObject LoadingShuriken;
	public bool isRotating = false;


	void Awake()
	{
		TitleCanvas.SetActive (true);
		LoadingCanvas.SetActive (false);
	}

    public void StartGame(string SceneName)
	{
        Application.LoadLevel (SceneName);
	}




	public void TitleCanvasActive()
	{
		TitleCanvas.SetActive (true);
		LoadingCanvas.SetActive (false);
	}

	public void LoadingCanvasActive()
	{
		TitleCanvas.SetActive (false);
		LoadingCanvas.SetActive (true);
		isRotating = true;
	}

	void Update()
	{
		if (isRotating)
		{
			LoadingShuriken.transform.Rotate (Vector3.forward);
		}
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
