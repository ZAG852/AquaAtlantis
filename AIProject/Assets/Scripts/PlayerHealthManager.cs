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
    public Animator[] anim;

    // Start is called before the first frame update
    void Start()
    {
        health = numberOfHearts;
    }

    // Update is called once per frame
    void Update()
    {

        // anim.SetBool("empty", empty);
        death();
        manageHealth();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "heart")
        {
            if (health != numberOfHearts)
            {
                print("heart");
                health = health + 1;
                Destroy(collision.gameObject);
                FXplayer.fxplayer.PlayFX(fxOptions.itemPickup);
                manageHealth();
            }
        }
    }
    void manageHealth()
    {
        //checks and makes sure players cant have more than the max health
        if (health > numberOfHearts)
        {
            health = numberOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i  < health)
            {
                hearts[i].sprite = fullHeart;
                empty = false;
                anim[i].SetBool("empty", empty);
            }
            else if(i + 1 > health)
            {
                hearts[i].sprite = emptyHeart;
                empty = true;
                anim[i].SetBool("empty", empty);
            }
            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    void death()
    {
        if (health <= 0)
        {
            Instantiate(DamageParticle, transform.position, Quaternion.identity);
            FXplayer.fxplayer?.PlayFX(fxOptions.deathFX);
            Destroy(gameObject);
        }
    }
    public void hurtPlayer(int damage)
    {
        health -= damage;
        FXplayer.fxplayer?.PlayFX(fxOptions.bonk);
    }

    public void setMaxHealth()
    {
        health = numberOfHearts;
    }
}