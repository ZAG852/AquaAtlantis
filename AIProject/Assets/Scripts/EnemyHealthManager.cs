using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public GameObject DeathParticle;
    public GameObject attackedParticle;

    public bool left = true;
    public bool isBoss = false;
    public Transform target;
    
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
            Instantiate(DeathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);

            if (isBoss)
            {
                //make stairs??
                print("you killed a boss");
            }
        }

        //checks if looking right
        if (target != null)
        {
            if (target.transform.position.x > transform.position.x)
            {
                left = false;
            }
            if (target.transform.position.x < transform.position.x)
            {
                left = true;
            }
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
