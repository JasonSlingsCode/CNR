using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

	public GameObject Sword;
	public GameObject SwordButton;
	public bool Attack;


	void Start()
	{
		//Sword = gameObject.transform.Find("Swordg").gameObject;
		//Sword.SetActive(false);
		SwordButton.SetActive(false);
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Sword") {
			Sword.SetActive(true);
			Destroy(col.gameObject);
			SwordButton.SetActive(true);
		}

	}


}
