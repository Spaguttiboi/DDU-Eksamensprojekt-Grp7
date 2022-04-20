using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 currentPos;
    public GameObject player;
    float throwVelocity = 20;
    bool ready = false;
    public int scale;
    bool roll = false;
    float slowDown = 0;
    public bool destroyed = false;
    bool canKill = true;


    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        currentPos = transform.position;

        if (ready)
        {
            transform.position = Vector3.MoveTowards(currentPos, targetPos * scale, throwVelocity * Time.deltaTime);
            

            if (targetPos.x > GameObject.Find("Dispenser").transform.position.x)
            {
                targetPos = targetPos - new Vector3(-0.1f, 0.01f, 0);
            }

            if (targetPos.x < GameObject.Find("Dispenser").transform.position.x)
            {
                targetPos = targetPos - new Vector3(0.1f, 0.01f, 0);
            }

        }



        if (roll)
        {
            if (GameObject.Find("Dispenser").transform.position.x > currentPos.x)
            {
                transform.position = Vector3.MoveTowards(currentPos, (transform.position + Vector3.left), (3 - slowDown) * Time.deltaTime);
            }
            else if (GameObject.Find("Dispenser").transform.position.x < currentPos.x)
            {
                transform.position = Vector3.MoveTowards(currentPos, (-transform.position - Vector3.left), (3 - slowDown) * Time.deltaTime);
            }

            if (slowDown < 3)
            {
                slowDown += 0.01f;
            }
            else
            {
                destroyed = true;

                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            targetPos = other.transform.position + new Vector3(0,1,0);

            ready = true;

            GetComponent<Rigidbody2D>().gravityScale = 1;

            Debug.Log("In Range");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ready = false;

        roll = true;

        if (canKill && other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
        else
        {
            canKill = false;
        }
    }



}
