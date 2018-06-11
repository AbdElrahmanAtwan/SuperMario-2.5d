using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{

    public float speed = 5.0f;
    public float maxDistance = 0.5f;

    private bool facingRight=false;
    private Rigidbody rb;
    private RaycastHit hit;
    private Vector3 dir;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dir = Vector3.right;
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, dir * maxDistance, Color.blue);
        if (Physics.Raycast(transform.position, dir, out hit, maxDistance))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "Enemy")
            {
                dir *= -1;
                transform.eulerAngles *= -1;
                if (facingRight)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    facingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    facingRight = true;
                }
            }
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.eulerAngles = new Vector3(0, -270, 0);
            collision.collider.GetComponent<Rigidbody>().velocity = Vector3.up * 15f;
            collision.collider.isTrigger = true;
            Destroy(collision.collider.gameObject, 1.5f);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "DeadlyPoint")
    //    {
    //        transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
    //        rb.velocity = Vector3.right * 0.0f;
    //        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    //        rb.detectCollisions = false;
    //        Destroy(gameObject, 1f);
    //    }
    //}
    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }
}
