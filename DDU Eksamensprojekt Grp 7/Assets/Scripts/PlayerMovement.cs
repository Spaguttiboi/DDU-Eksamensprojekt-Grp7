using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    public float moveSpeed = 3f;
    public float jumpHeight = 10f;
    float gravity = -9.82f;

    public float pushLength = 0.2f;
    public float distance = 1f;

    private new Rigidbody2D rigidbody;

    //Ikke optimal og burde ændres hvis mulig
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
            
            MoveAnimation.SetBool("IsJumping", false);
        }

        MoodChooser();
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

            //movespeed

        }
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * pushLength); 
	}
}
