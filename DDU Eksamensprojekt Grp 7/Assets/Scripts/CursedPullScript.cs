using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedPullScript : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    private PushPullObject pushPullScript;
    private PlayerMovement movementScript;
    private Rigidbody2D rigidbody;
    private FixedJoint2D fixedJoint;
    float mimicMovement;
    float speed = 3.1f;
    bool connected;

    GameObject player;

	private void Start()
	{
        GameObject player = GameObject.FindWithTag("Player");
        pushPullScript = player.GetComponent<PushPullObject>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        fixedJoint = gameObject.GetComponent<FixedJoint2D>();
    }

	void Update()
    {
        mimicMovement = Input.GetAxisRaw("Horizontal");
        connected = pushPullScript.connected;

        if (mimicMovement == 1 && connected)
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        else if (mimicMovement == -1 && connected)
            rigidbody.velocity = new Vector2(+speed, rigidbody.velocity.y);
        else
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        if (IsGrounded())
            fixedJoint.enabled = true;
        else 
            fixedJoint.enabled = false;
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, (transform.localScale.y / 2) + 0.1f, platformLayerMask) != false;
    }
}
