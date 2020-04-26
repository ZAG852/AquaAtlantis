using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic : MonoBehaviour
{
    public float speed = 7f;
    public Rigidbody2D rb;

    public int damage;

    public GameObject flameParticle;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
      
    }
    //for later
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "enemy")
        {
            //Destroy(collision.gameObject);
            Instantiate(flameParticle, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<EnemyHealthManager>().hurtEnemy(damage);
            Destroy(gameObject);

        }
        if (collision.tag != "Player" && collision.tag != "heart")
        {
            Destroy(gameObject);
            Instantiate(flameParticle, transform.position, Quaternion.identity);
        }
    }
}
