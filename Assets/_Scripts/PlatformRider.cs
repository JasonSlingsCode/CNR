using UnityEngine;
using System.Collections;

public class PlatformRider : MonoBehaviour
{

    void Awake()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.parent = gameObject.transform;


    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.transform.parent = null;
    }
}
