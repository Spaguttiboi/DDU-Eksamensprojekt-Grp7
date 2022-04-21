using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Braindead : MonoBehaviour
{
    Vector3 throwPoint;
    Vector3 targetPoint;
    float throwVelocity = 15;
    public float xOffset;
    public float yOffset;

    void Start()
    {
        targetPoint = new Vector3(GetComponentInParent<Thrower>().xTargetValue + xOffset, GetComponentInParent<Thrower>().yTargetValue + yOffset);
    }

    private void Update()
    {
        throwPoint = transform.position;
        transform.position = Vector3.MoveTowards(throwPoint, targetPoint * 1, throwVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("World");
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }




}
