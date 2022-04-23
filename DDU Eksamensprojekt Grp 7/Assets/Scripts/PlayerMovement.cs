using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    public float playerSpeed;
    public float scaredPlayerSpeed;
    float moveSpeed = 3f;

    public float playerJumpHeight;
    public float anxiousPlayerJumpHeight;
    float jumpHeight = 10f;
    float gravity = -9.82f;

    bool plswork = false;
    float timework = 2f;

    public float pushLength = 0.2f;
    public float distance = 1f;

    private new Rigidbody2D rigidbody;

    //Ikke optimal og burde ændres hvis mulig
    public bool numb = true;
    public bool angry = false;
    public bool anxious = false;
    public bool scared = false;

    Camera playerCamera;
    Animator MoveAnimation;

    public AudioSource audioSource;

    //Stingers
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip deathSound;
    public AudioClip angrySound;
    public AudioClip anxietySound;
    public AudioClip numbSound;
    public AudioClip runSound;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        MoveAnimation = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        //MoveDirection();

        //MoodChooser();

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity = Vector2.up * Mathf.Sqrt((jumpHeight * rigidbody.gravityScale) * (-2) * gravity);
            MoveAnimation.SetBool("IsJumping", true);
            plswork = true;

            audioSource.loop = false;
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
        if (plswork && IsGrounded() && Time.time > timework)
        {
            timework = Time.time + 1f;
            MoveAnimation.SetBool("IsJumping", false);
            plswork = false;

            audioSource.loop = false;
            audioSource.clip = landSound;
            audioSource.Play();
        }

        temperaryMovementSystem();
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
            scale.x = -1f;
            transform.localScale = scale;
        }
        else if (direction == -1)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1f;
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

            audioSource.loop = false;
            audioSource.clip = numbSound;
            audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Alpha2) && angry == false)
        {
            numb = false;
            angry = true;
            anxious = false;
            scared = false;

            audioSource.loop = false;
            audioSource.clip = angrySound;
            audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Alpha3) && anxious == false)
        {
            numb = false;
            angry = false;
            anxious = true;
            scared = false;

            audioSource.loop = false;
            audioSource.clip = anxietySound;
            audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Alpha4) && scared == false)
        {
            numb = false;
            angry = false;
            anxious = false;
            scared = true;

<<<<<<< Updated upstream

=======
            
>>>>>>> Stashed changes
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * pushLength);
    }

    void temperaryMovementSystem()
    {
        //Mood system
        if (Input.GetKey(KeyCode.Alpha1) && numb == false)
        {
            numb = true;
            angry = false;
            anxious = false;
            scared = false;

            audioSource.loop = false;
            audioSource.clip = numbSound;
            audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Alpha2) && angry == false)
        {
            numb = false;
            angry = true;
            anxious = false;
            scared = false;

            audioSource.loop = false;
            audioSource.clip = angrySound;
            audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Alpha3) && anxious == false)
        {
            numb = false;
            angry = false;
            anxious = true;
            scared = false;

            audioSource.loop = false;
            audioSource.clip = anxietySound;
            audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Alpha4) && scared == false)
        {
            numb = false;
            angry = false;
            anxious = false;
            scared = true;
        }

        float direction = Input.GetAxisRaw("Horizontal");

        if (direction == -1 && angry == false || direction == 1 && angry)
            rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
        else if (direction == 1 && angry == false || direction == -1 && angry)
            rigidbody.velocity = new Vector2(+moveSpeed, rigidbody.velocity.y);
        else
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        //Change speed when scared
        if (numb || angry || anxious)
            moveSpeed = playerSpeed;
        else if (scared)
        {
            moveSpeed = scaredPlayerSpeed;

            audioSource.loop = true;
            audioSource.clip = runSound;
            audioSource.Play();
        }
        //Change jump height when anxious
        if (numb || angry || scared)
        {
            jumpHeight = playerJumpHeight;
            playerCamera.orthographicSize = 5;
        }
        else if (anxious)
        {
            jumpHeight = anxiousPlayerJumpHeight;
            playerCamera.orthographicSize = 3;
        }





        //Visual system
        FlipPlayerModel(direction);
        MoveAnimation.SetFloat("Speed", Mathf.Abs(direction));
    }
}
