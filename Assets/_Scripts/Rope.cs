using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

	void Awake()
	{
		Physics2D.IgnoreLayerCollision (10, 10, true);
	}
}
