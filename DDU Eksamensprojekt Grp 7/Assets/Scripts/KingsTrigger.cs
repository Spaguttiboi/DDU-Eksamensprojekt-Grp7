using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingsTrigger : MonoBehaviour
{
    public GameObject king;
    KingOfPhantoms kingsScript;

    private void Start()
    {
        kingsScript = king.GetComponent<KingOfPhantoms>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            kingsScript.ProwlingKing();
        }
    }
}
