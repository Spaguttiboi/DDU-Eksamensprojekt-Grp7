using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject prefap;
    public float throwSpeed;
    float cooldown;

    private void Start()
    {
        //Instantiate(prefap, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

    private void Update()
    {
        //if (rockScript.destroyed)
        //{
        //    rockScript.destroyed = false;

        //    Instantiate(prefap, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

        //    Refresh_Rock();
        //}
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (cooldown < 0)
            {
                Instantiate(prefap, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

                cooldown = throwSpeed;
            }
            else
            {
                cooldown = cooldown - Time.deltaTime;
            }
        }
    }



}
