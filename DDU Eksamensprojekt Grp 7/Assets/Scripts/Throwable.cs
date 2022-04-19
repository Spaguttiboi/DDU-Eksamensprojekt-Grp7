using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 currentPos;
    public GameObject player;
    public float throwVelocity;
    bool ready;


    private void Start()
    {
        
    }

    void Update()
    {
        if (ready)
        {
            currentPos = transform.position;

            Vector2 d = new Vector2(targetPos.x, targetPos.y);

            transform.position = Vector3.MoveTowards(currentPos, targetPos, throwVelocity * Time.deltaTime);

            targetPos = player.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            targetPos = other.transform.position;

            ready = true;

            GetComponent<Rigidbody2D>().gravityScale = 1;

            Debug.Log("In Range");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }


        Destroy(this);
    }



}
