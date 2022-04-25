using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KingOfPhantoms : MonoBehaviour
{
    Vector3 origin;
    Vector3 target;
    public float endPoint;
    public float kingsSpeed;
    public bool eventTriggered;

    private void Start()
    {
        origin = transform.position;

        target = new Vector3(endPoint, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("World", LoadSceneMode.Single);
        }
    }

    public void ProwlingKing()
    {
        eventTriggered = true;
    }

    void Update()
    {
        if (eventTriggered == true)
        {
            transform.position = Vector3.MoveTowards(origin, target, kingsSpeed * Time.deltaTime);

            origin = transform.position;
        }
    }
}
