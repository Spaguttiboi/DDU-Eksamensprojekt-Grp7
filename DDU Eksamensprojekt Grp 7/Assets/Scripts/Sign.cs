using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject player;
    public GameObject keyPromt;
    public GameObject signCloseup;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyPromt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                signCloseup.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyPromt.SetActive(false);
        }
    }
}
