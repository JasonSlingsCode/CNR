using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{

    public int Health = 12;
    public int MaxHealth = 12;
    public bool LowHealthAlert;
    public Animator Anim;
    public Rigidbody2D ThisBody;
    public Transform HitBox;
    public bool GotHit = false;
    public bool GotHurt = false;
    public float HitForce;
    Light TailLight;
    public GameObject Light;
    Color Green = new Color(0.0F, 1.0F, 0.0F);
    Color Yellow = new Color(1.0F, 0.929F, 0.450F);
    Color Red = new Color(1.0F, 0.380F, 0.333F);
    Color DarkRed = new Color(0.282F, 0.098F, 0.0F);
    Color TransColor = new Color(1, 1, 1, .5F);
    Color NormalColor = new Color(1, 1, 1, 1);
    public SpriteRenderer SpriteRen;
    private string ControlsUsed;
    GameObject food;
    public GameObject DeadRaccoonParts;
    public GameObject GameOver;

    void Start()
    {
        ThisBody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        TailLight = Light.GetComponent<Light>();
        // SpriteRen = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Health < 0)
            Health = 0;
        float x = (float)Health / (float)MaxHealth;
        x = 1 - x;

        if (GotHit == true)
        {
            SpriteRen.color = Color.Lerp(TransColor, NormalColor, Mathf.PingPong(Time.time * 1, .2F));
            TailLight.color = Color.Lerp(TransColor, NormalColor, Mathf.PingPong(Time.time * 1, .2F));
        } else
        {
            SpriteRen.color = NormalColor;

        }

        if (Health <= 0)
        {
            Invoke("Death", 0);
        }
        TailLight.color = new Color((2.0f * x), (2.0f * (1 - x)), 0);
    }

    void FixedUpdate()
    {
        Anim.SetInteger("Health", Health);
        Anim.SetBool("GotHurt", GotHurt);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "Enemy") || (col.gameObject.tag == "Hazard"))
        {
            if (Health > 0)
                Health--;

            Vector2 poc = col.gameObject.transform.position;
            AddExplosionForce(ThisBody, (col.gameObject.GetComponent<Rigidbody2D>().mass * HitForce), poc, 100f);

            // this.gameObject.GetComponent<ActorControllerScript>().enabled = false;
            // this.gameObject.GetComponent<ActorJumpScript>().enabled = false;

            GotHurt = true;
            Invoke("Hurt", 1);

            Invoke("IgnoreLayTrue", 0f);

            Debug.Log("HP: " + Health);
        }

        if (col.gameObject.tag == "Food")
        {
            if (Health < MaxHealth)
                Health++;
            
            food = col.gameObject;
            food.GetComponent<ParticleSystem>().enableEmission = true;
            food.GetComponent<SpriteRenderer>().enabled = false;
            food.GetComponent<Rigidbody2D>().isKinematic = true;
            food.GetComponent<PolygonCollider2D>().enabled = false;
            food.GetComponent<FoodAI>().Invoke("DestroyFood", 1f);
            
            Debug.Log("HP: " + Health);
            
        }
    }

    void Hurt()
    {
        // this.gameObject.GetComponent<ActorControllerScript>().enabled = true;
        // this.gameObject.GetComponent<ActorJumpScript>().enabled = true;
        GotHurt = false;
    }

    void IgnoreLayTrue()
    {
        print("IgnoreLayTrue");
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Invoke("IgnoreLayFalse", .5f);
        GotHit = true;
        
    }

    void IgnoreLayFalse()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        GotHit = false;

    }

    void AddExplosionForce(Rigidbody2D body, float expForce, Vector2 expPosition, float expRadius)
    {
        Vector2 PlayerPos = new Vector2(HitBox.position.x, HitBox.position.y);
        Vector2 dir = (PlayerPos - expPosition);
        Debug.DrawRay(expPosition, dir);
        body.velocity = new Vector2(0, 0);
        float calc = 1 - (dir.magnitude / expRadius);
        if (calc <= 0)
        {
            calc = 0;       
        }
        
        body.AddForce(dir.normalized * expForce * calc);
    }

    void Death()
    {
        foreach (Transform child in transform)
        {
            
            child.gameObject.SetActive(false);
        }

        DeadRaccoonParts.SetActive(true);
        GameOver.SetActive(true);
        GotHurt = false;
    }

}
