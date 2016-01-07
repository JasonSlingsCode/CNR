using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{

	Rigidbody2D RB;
	public GameObject Explosion;
	SpriteRenderer SR;
	Collider2D Col;
	public float valX;
	public float valY;
	GameObject Raccoon;

	// Use this for initialization
	void Start ()
	{
		Raccoon = GameObject.FindGameObjectWithTag ("Player");
		RB = GetComponent<Rigidbody2D> ();
		SR = GetComponent<SpriteRenderer> ();
		Col = GetComponent<Collider2D> ();
	}

	void FixedUpdate ()
	{
		Vector2 PlayerPos = Raccoon.transform.position;
		valX = PlayerPos.x - this.transform.position.x;
		valY = PlayerPos.y - this.transform.position.y;
		if (valY < 0 && valY >= -10) {
			if (valX >= -1 && valX <= 1) {
				if (gameObject.transform.parent != null) {
					if (gameObject.transform.parent.gameObject.GetComponent<FoxAI> ().HasBomb == true) {
						RB.isKinematic = false;
						gameObject.transform.parent.gameObject.GetComponent<FoxAI> ().HasBomb = false;
						GameObject Fox = gameObject.transform.parent.gameObject;
                        Col.isTrigger = false;
                        FoxAI brain = Fox.GetComponent<FoxAI>();
                        brain.Invoke("Dead", 3);
						gameObject.transform.parent = null;
					}
				}
			}
		}
	}

	/*
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && gameObject.transform.parent.gameObject.GetComponent<FoxAI>().HasBomb == true)
		{


		}
	}
*/

    void OnCollisionEnter2D (Collision2D other)
    {

            SR.enabled = false;
            Col.enabled = false;
            RB.isKinematic = true;
            Explosion.SetActive(true);
            Invoke("DestroyBomb", 1);
        
	}

	void DestroyBomb ()
	{
		Destroy (gameObject);
	}
}



