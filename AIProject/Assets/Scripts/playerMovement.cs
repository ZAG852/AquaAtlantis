﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D rb;
    bool facingRight = true;

    Vector2 movement;
    GameManager gm;
    public Animator anim;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gm.paused) { 
            //input

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            anim.SetFloat("H", movement.x);
            anim.SetFloat("V", movement.y);
            anim.SetFloat("speed", movement.sqrMagnitude);


            if (facingRight == false && movement.x > 0)
            {

                Flip();
            }
            else if (movement.x < 0 && facingRight == true)
            {
                Flip();
            }
            if(Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    void FixedUpdate()
    {
        //movement

        rb.MovePosition(rb.position + (movement * speed * Time.fixedDeltaTime));
    }

}
