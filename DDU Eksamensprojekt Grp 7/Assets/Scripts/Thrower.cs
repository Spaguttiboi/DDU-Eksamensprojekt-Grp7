using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public float cooldownReset;
    public float cooldown;
    public float xTargetValue;
    public float yTargetValue;
    public GameObject prefap;
    string name;
    public float xOffset;
    public float yOffset;
<<<<<<< Updated upstream
    public float throwForce;
=======
    public float throwSpeed;
>>>>>>> Stashed changes

    private void Start()
    {
        name = gameObject.name;

        xTargetValue = GetComponent<BoxCollider2D>().offset.x + transform.position.x + xOffset;
        yTargetValue = GetComponent<BoxCollider2D>().offset.y - transform.position.y + yOffset;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;

        //Debug.Log(cooldown);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (cooldown <= 0)
            {

                GameObject Stone = Instantiate(prefap, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
                Stone.transform.parent = GameObject.Find(name).transform;

                cooldown = cooldownReset;

            }
            else
            {
                //cooldown -= Time.deltaTime; 
            }
        }
    }
}
