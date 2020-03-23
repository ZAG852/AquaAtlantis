using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    public GameObject DamageParticle;
    public GameObject flameParticle;


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            Instantiate(DamageParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void hurtEnemy(int damage)
    {
        CurrentHealth -= damage;
    }

    public void setMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}