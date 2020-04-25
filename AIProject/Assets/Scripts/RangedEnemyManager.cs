using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyManager : MonoBehaviour
{
    public float distance;
    public float range;
    public float interval;
    public float bulletspeed;
    public float bulletTimer;

    public bool inRange = false;
    public bool attackk = false;

    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPoint;

       private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attack();
        }
    }
    // Update is called once per frame
    void Update()
    {
        rangeCheck();
    }
    public void rangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < range)
        {
            attack();
            print("attack");
            inRange = true;
        }
        if(distance > range)
        {
            attack();
            inRange = false;
        }
    }
    public void attack() {

        if(inRange)
        {
            Instantiate(bullet, shootPoint.position, transform.rotation);

        }
    }

}
