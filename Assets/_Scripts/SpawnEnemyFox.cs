using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemyFox : MonoBehaviour {

    public GameObject NewFox;
    public float radius;
    public float spawnFrequency;
    public float distanceFromObjects = 1;
    private Vector3 newPos;
    private Collider2D[] otherObjects;
    GameObject thePlayer;

    public List<GameObject> AllTheFoxes = new List<GameObject>();

    void Start ()
    {
        InvokeRepeating ("SpawnObject", 1f, spawnFrequency);
        radius = this.gameObject.GetComponent<CircleCollider2D>().radius;
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        float yPos = thePlayer.transform.position.x;
        transform.position =  new Vector2 (yPos, transform.position.y);
        // float thisPos = transform.position.y;
        // thisPos = yPos;
    }
    
    void SpawnObject ()
    {
        if (AllTheFoxes.Count > 4)
            return;
        newPos = FindNewPos ();
        GameObject fox = GameObject.Instantiate (NewFox, newPos, Quaternion.identity) as GameObject;
        AllTheFoxes.Add(fox);

    }

    Vector3 FindNewPos ()
    {
        // do {
        newPos = new Vector3 (transform.position.x + Random.Range (-radius, radius), transform.position.y + Random.Range (-radius, radius), 0);
            // otherObjects = Physics2D.OverlapCircleAll (newPos, distanceFromObjects);
        // } //while (otherObjects.Length > 0);
        return newPos;
    }
}
