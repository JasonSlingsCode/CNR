using UnityEngine;
using System.Collections;

public class IgnoreCollisions : MonoBehaviour {

    public GameObject player;
     
    
    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Invoke ("IgnoreCol", .1f);
        }
        
        
    }
    
    void IgnoreCol()
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(),GetComponent<Collider2D>());
    }
}
