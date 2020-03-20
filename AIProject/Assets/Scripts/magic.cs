using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;

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
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
