using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignClose : MonoBehaviour
{
    public GameObject signCloseupParent;

    public void CloseSign()
    {
        signCloseupParent.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CloseSign();
        }
    }
}
