using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedPullScript : MonoBehaviour
{
    private PushPullObject pushPullScript;
    private Rigidbody2D rigidbody;
    float mimicMovement;
    float speed = 3.1f;
    bool connected;

    GameObject player;

	private void Start()
	{
        GameObject player = GameObject.FindWithTag("Player");
        pushPullScript = player.GetComponent<PushPullObject>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

	void Update()
    {
        mimicMovement = Input.GetAxisRaw("Horizontal"); ;
        connected = pushPullScript.connected;

        if (mimicMovement == 1 && connected)
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        else if (mimicMovement == -1 && connected)
            rigidbody.velocity = new Vector2(+speed, rigidbody.velocity.y);
        else
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }


}
