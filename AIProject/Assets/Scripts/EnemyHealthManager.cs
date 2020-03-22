using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrentHealth;

    public GameObject slimeParticle;
    public GameObject flameParticle;


    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            Instantiate(slimeParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void hurtEnemy(int damage)
    {
        enemyCurrentHealth -= damage;
    }

    public void setMaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }
}
