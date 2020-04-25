using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemyBulletManager : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    float mover = 0;
    public Transform firepoint;
    public GameObject fire;
    public GameObject bolt;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;

        //is all this nessessary? i have no idea
        Vector3 pointLocation = cam.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + mover);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(fire, firepoint.position, transform.rotation);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(bolt, firepoint.position, transform.rotation);
        }
    }
}