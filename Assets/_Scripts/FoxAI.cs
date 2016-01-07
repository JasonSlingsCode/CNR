using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoxAI : MonoBehaviour
{
    public Rigidbody2D thisRB2D;
    public float Rate = 1;
    public float RandomRate;
    public float JumpHeight;
    public float Speed;
    private bool FacingRight = false;
    private bool CanMoveInAir = true;
    private bool TouchingWall = false;
    private float MoveControl;
    private float hInput;
    public GameObject DeadFoxParts;
    public Transform GroundCheck;
    public Transform Target;
    public LayerMask WhatIsGround;
    public bool IsGrounded = false;
    private float GroundRadius = 5f;
    public float vForce;
    public bool HasBomb = true;
    public bool InRange = false;
    public GameObject ThePlayer;
    Transform[] children;
    private Vector3 newPos;
    public float radius;
    public List<Collider2D> otherObjects;
    public float distanceFromObjects = 0;

    void Awake()
    {
        thisRB2D = GetComponent<Rigidbody2D>();
        ThePlayer = GameObject.FindGameObjectWithTag("Player");
        children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "GroundCheck")
            {
                GroundCheck = child;
            }
            if (child.name == "Target")
            {
                Target = child;
            }
            radius = Target.GetComponent<CircleCollider2D>().radius;
        }
        // GroundCheck = GameObject.Find(this.gameObject.name + "/GroundCheck").transform;
        // JetpackY = Random.Range(JetpackMin, JetpackMax);
    }

    void Start()
    {
        InvokeRepeating("Fly", 1, Rate);
    }

    void Update()
    {

        if (ThePlayer.transform.position.x > this.transform.position.x && FacingRight == false)
        {
            FlipSprite();
            Speed = -Speed;
        } else if (ThePlayer.transform.position.x < this.transform.position.x && FacingRight == true)
        {
            FlipSprite();
            Speed = -Speed;
        }


        Vector2 vVel = thisRB2D.velocity;
        if (vVel.y < -5)
        {
            vVel.y = -5;

        }
        else if (vVel.y > 20)
        {
            vVel.y = 20;
        }
        thisRB2D.velocity = vVel;
    }

    void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, WhatIsGround);
        if (IsGrounded)
        {
            thisRB2D.AddForce(new Vector2(0, ThePlayer.transform.localPosition.y) * vForce, ForceMode2D.Force);
        }
        switch (HasBomb)
        {
            case true:
                thisRB2D.mass = 2;
                // thisRB2D.gravityScale = 0;
                break;
            case false:
                thisRB2D.mass = 0.2f;
                thisRB2D.AddForce(transform.up, ForceMode2D.Impulse);
                Destroy(gameObject, 4);
                break;
            default:
                break;
        }

    }

    public void Fly()
    {
        if (HasBomb)
        {
            // thisRB2D.AddForce(new Vector2(ThePlayer.transform.position.x, 0) * Speed * Time.deltaTime, ForceMode2D.Force);
            Target.transform.position = FindNewPos();
            // newPos = FindNewPos ();
            var heading = newPos - this.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            if (FacingRight)
            {
                thisRB2D.AddForce(-direction * Speed, ForceMode2D.Force);
            }
            if (!FacingRight)
            {
                thisRB2D.AddForce(direction * Speed, ForceMode2D.Force);
            }
            // thisRB2D.AddForce(-direction * Speed, ForceMode2D.Force);
        }
    }

    Vector3 FindNewPos ()
    {
        otherObjects = new List<Collider2D>();
        do {
            GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
            newPos = new Vector3 (thePlayer.transform.position.x + Random.Range (-radius, radius), thePlayer.transform.position.y + Random.Range (-radius, radius), 0);
            foreach(Collider2D other in otherObjects)
            {
                if (other.gameObject.tag != "Enemy")
                {
                    otherObjects.Add(other);
                }
                if (other.gameObject.tag == null)
                {
                    otherObjects.Remove(other);
                }
            }
        } while (otherObjects.Count > 0);
        return newPos;
    }


    void FlipSprite()
    {
        // face the opposite direction
        FacingRight = !FacingRight;
        
        // get the local scale
        Vector3 TheScale = transform.localScale;
        
        // flip the x axis
        TheScale.x *= -1;
        
        // apply back to local scale
        transform.localScale = TheScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword")
        {
            foreach (Transform child in transform)
            {

                child.gameObject.SetActive(false);
            }

            DeadFoxParts.SetActive(true);
            Invoke("Dead", 1);
        }
    }

    public void Dead()
    {
        // Destroy(this.gameObject);
        GameObject foxSpawner = GameObject.Find("FoxSpawner");
        SpawnEnemyFox spawnPoint = foxSpawner.GetComponent<SpawnEnemyFox>();
        spawnPoint.AllTheFoxes.Remove(this.gameObject);
        foreach(GameObject foxInList in spawnPoint.AllTheFoxes)
        {
            print (foxInList);
            if (foxInList == null)
            {
                print (foxInList);
                spawnPoint.AllTheFoxes.Remove(foxInList);
            }
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine (transform.position, Target.transform.position);
        
    }
}
