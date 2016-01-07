using UnityEngine;
using System.Collections;

public class IfPlayerIsInside : MonoBehaviour
{

	public GameObject ThePlayer;
	public GameObject TheDoor;
	public Vector2 ThePlayerPos;
    public Bounds TriggerBounds;

	void Awake()
	{
        TriggerBounds = this.gameObject.GetComponent<BoxCollider2D>().bounds;
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start ()
	{
	
	}

	// Update is called once per frame
	void Update ()
	{
		ThePlayerPos = ThePlayer.transform.position;
        if (TriggerBounds.Contains (ThePlayerPos)) {
			TheDoor.gameObject.layer = 27;
		} else {
			TheDoor.gameObject.layer = 0;
		}
	}



}
