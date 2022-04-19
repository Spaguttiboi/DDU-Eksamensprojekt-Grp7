using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask ObjectMask;

    public float moveSpeed = 5f;
    public float jumpHeight = 10f;
    float gravity = -9.82f;

    public float pushLength = 0.7f;
    public float distance = 1f;
    GameObject box;

    public bool[] mood = new bool[4];

    private new Rigidbody2D rigidbody;

    Animator MoveAnimation;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveDirection();

        if (IsGrounded() && Input.GetButtonDown("Jump"))
            rigidbody.velocity = Vector2.up * Mathf.Sqrt((jumpHeight * rigidbody.gravityScale) * (-2) * gravity);

        //MoveAnimation.SetFloat("Horizontal", movement.x);

        MoodChooser();

        //PushAndPullObjects();
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.1f, platformLayerMask) != false;
    }

    void MoveDirection()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        if (direction == -1)
            rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
        else if (direction == 1)
            rigidbody.velocity = new Vector2(+moveSpeed, rigidbody.velocity.y);
        else
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }

    void MoodChooser()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            /*
            Array.fill(mood, false);
            mood[0] = true;

            for(int i = 0; i<mood.Length; i++)
                Debug.Log(mood);

            */
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            bool[] mood = new bool[4];
            mood[1] = true;
            for (int i = 0; i < mood.Length; i++)
                Debug.Log(mood);

        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            bool[] mood = new bool[4];
            mood[2] = true;
            for (int i = 0; i < mood.Length; i++)
                Debug.Log(mood);

        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            bool[] mood = new bool[4];
            mood[3] = true;
            for (int i = 0; i < mood.Length; i++)
                Debug.Log(mood);

        }
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * pushLength); 
	}


    /*
    void PushAndPullObjects()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, pushLength, ObjectMask);

        if (hit.collider != null && hit.collider.gameObject.tag == "Pushable" && angry && Input.GetKey(KeyCode.E))
        {
            box = hit.collider.gameObject;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<ObjectPull>().beingPushed = true;

        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<ObjectPull>().beingPushed = false;
        }
    }
    */
}
