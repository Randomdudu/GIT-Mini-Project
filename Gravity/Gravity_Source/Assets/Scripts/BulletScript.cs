using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject BulletExplosion;
    public PlayerScript player;
    public float speed;
    public int dmg;
    Rigidbody2D rb2d;

    Vector2 moveDir;

    void Awake()
    {
        Destroy(gameObject, 5);
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
       
    }

    void Start()
    {     
        rb2d = GetComponent<Rigidbody2D>();
        moveDir = (player.transform.position - transform.position).normalized * speed;
        rb2d.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 4);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(BulletExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            player.TakeDamage(dmg);
        }
    }
}
