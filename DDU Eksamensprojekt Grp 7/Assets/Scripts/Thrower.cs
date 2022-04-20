using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public float cooldownReset;
    public float cooldown;
    public static float xTargetValue;
    public static float yTargetValue;
    public GameObject prefap;

    private void Start()
    {
        xTargetValue = GetComponent<BoxCollider2D>().offset.x + transform.position.x;
        yTargetValue = GetComponent<BoxCollider2D>().offset.y - transform.position.y;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (cooldown <= 0)
            {
                Instantiate(prefap, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

                cooldown = cooldownReset;
            }
            else
            {
                cooldown -= Time.deltaTime; 
            }

            Debug.Log(cooldown);
            Debug.Log(xTargetValue);
            Debug.Log(yTargetValue);
        }
    }

}
