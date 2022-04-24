using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject keyPromt;
    public GameObject signCloseup;
    bool playerInRange;

    void Start()
    {
        keyPromt.SetActive(false);

        playerInRange = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyPromt.SetActive(true);

            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyPromt.SetActive(false);

            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                signCloseup.SetActive(true);
            }
        }
    }
}
