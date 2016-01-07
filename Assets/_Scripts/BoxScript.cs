using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour {

	public GameObject Box;
	public GameObject BrokenBoxParts;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Sword")
		{				
			Box.SetActive(false);
			
			BrokenBoxParts.SetActive(true);
			Invoke("Dead", 1);
		}
	}

	void Dead()
	{
		Destroy (gameObject);
	}
}
