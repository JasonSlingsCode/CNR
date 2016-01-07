using UnityEngine;
using System.Collections;

public class FoodAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		gameObject.GetComponent<ParticleSystem>().enableEmission = false;

	}

    void DestroyFood()
    {
        Destroy(this.gameObject);
    }

}
