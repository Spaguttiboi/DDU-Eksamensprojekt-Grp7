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

    private new Rigidbody2D rigidbody;

    //Ikke optimal og burde �ndres hvis mulig
    public bool numb = true;
    public bool angry = false;
    public bool anxious = false;
    public bool scared = false;

    Animator MoveAnimation;

    void Awake()
    {
       rigidbody = GetComponent<Rigidbody2D>();
       MoveAnimation= GetComponent<Animator>();
    }

    void Update()
    {
        MoveDirection();


        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity = Vector2.up * Mathf.Sqrt((jumpHeight * rigidbody.gravityScale) * (-2) * gravity);
            MoveAnimation.SetBool("IsJumping", true);
        }

        if (IsGrounded())
            MoveAnimation.SetBool("IsJumping", false);

        MoodChooser();

        //PushAndPullObjects();
	}

	private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.3f, platformLayerMask) != false;
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

        FlipPlayerModel(direction);
        MoveAnimation.SetFloat("Speed", Mathf.Abs(direction));
    }

    void FlipPlayerModel(float direction)
    {
        if (direction == 1)
        {
            Vector3 scale = transform.localScale;
            scale.x = -3f;
            transform.localScale = scale;
        }
        else if (direction == -1)
        {
            Vector3 scale = transform.localScale;
            scale.x = 3f;
            transform.localScale = scale;
        }
    }


    void MoodChooser()
    {
        if (Input.GetKey(KeyCode.Alpha1) && numb == false)
        {
            numb = true;
            angry = false;
            anxious = false;
            scared = false;


        }
        if (Input.GetKey(KeyCode.Alpha2) && angry == false)
        {
            numb = false;
            angry = true;
            anxious = false;
            scared = false;

        }
        if (Input.GetKey(KeyCode.Alpha3) && anxious == false)
        {
            numb = false;
            angry = false;
            anxious = true;
            scared = false;

        }
        if (Input.GetKey(KeyCode.Alpha4) && scared == false)
        {
            numb = false;
            angry = false;
            anxious = false;
            scared = true;

        }
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * pushLength); 
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
