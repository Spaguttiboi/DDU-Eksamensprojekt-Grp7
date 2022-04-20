using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Braindead : MonoBehaviour
{
    Vector3 throwPoint;
    Vector3 targetPoint;
    float throwVelocity = 15;

    void Start()
    {
        targetPoint = new Vector3(GetComponentInParent<Thrower>().xTargetValue, GetComponentInParent<Thrower>().yTargetValue);
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
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }




}
