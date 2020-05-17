using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField]
    public int damage;
    [SerializeField]
    public float interval;
    public float timer;

    public bool attack = false;


    public Transform target = null;
    public GameObject flameParticle;

    private void Update()
    {
        if (attack && target != null && Mathf.Sqrt(Mathf.Pow((target.transform.position.x - transform.position.x), 2) + Mathf.Pow((target.transform.position.y - transform.position.y), 2)) <= 1)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                timer = 0;
                target.GetComponent<PlayerHealthManager>().hurtPlayer(damage);
                Instantiate(flameParticle, target.transform.position, Quaternion.identity);
            }

            //attack = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
          // if (timer == 0)
           // {
                attack = true;

                target = collision.gameObject.transform;
                
          //  }

        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            attack = false;

        }
    }

}
