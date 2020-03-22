using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic : MonoBehaviour
{
    public float speed = 7f;
    public Rigidbody2D rb;

    public int damage;

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

            collision.gameObject.GetComponent<EnemyHealthManager>().hurtEnemy(damage);
        }
        Destroy(gameObject);
    }
}
