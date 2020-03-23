using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public int health;
    public int numberOfHearts;

    public GameObject DamageParticle;
    public GameObject flameParticle;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    bool empty = false;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        health = numberOfHearts;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("empty", empty);

        if (health <= 0)
        {
            Instantiate(DamageParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        //checks and makes sure players cant have more than the max health
        if(health > numberOfHearts)
        {
            health = numberOfHearts;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
                empty = false;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                empty = true;
            }
            if(i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void hurtEnemy(int damage)
    {
        health -= damage;
    }

    public void setMaxHealth()
    {
        health = numberOfHearts;
    }
}