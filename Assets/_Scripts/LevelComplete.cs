using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour {

	public GameObject LevelCompleteCanvas;

	public void NextLevel(string SceneName)
	{
		Application.LoadLevel(SceneName);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			LevelCompleteCanvas.SetActive(true);
		
		}
	}
}
