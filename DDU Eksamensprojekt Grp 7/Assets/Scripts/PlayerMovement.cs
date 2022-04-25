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
    public float fearPlayerJumpHeight;
    float jumpHeight = 10f;
    float gravity = -9.82f;

    bool jumpSwitch = false;
    float timeJumpSwitch = 2f;

    public float pushLength = 0.2f;
    public float distance = 1f;

    private new Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;

    //Ikke optimal og burde ændres hvis mulig
    public bool numb = true;
    public bool angry = false;
    public bool anxious = false;
    public bool fear = false;

    bool pullingObjectLeft;
    bool pullingObjectRight;
    bool connected;

    Camera playerCamera;
    Animator MoveAnimation;
    private PushPullObject pushPullScript;
    public GameObject anxietyFog;
    private SpriteRenderer spriteRenderer;

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
        boxCollider = GetComponent<BoxCollider2D>();
        MoveAnimation = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>();
        pushPullScript = GetComponent<PushPullObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (IsGrounded() && Input.GetButtonDown("Jump") || IsGrounded() && Input.GetKey(KeyCode.W))
        {
            rigidbody.velocity = Vector2.up * Mathf.Sqrt((jumpHeight * rigidbody.gravityScale) * (-2) * gravity);
            MoveAnimation.SetBool("IsJumping", true);
            jumpSwitch = true;

            audioSource.loop = false;
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
        if (jumpSwitch && IsGrounded() && Time.time > timeJumpSwitch)
        {
            timeJumpSwitch = Time.time + 1f;
            MoveAnimation.SetBool("IsJumping", false);
            jumpSwitch = false;

            audioSource.loop = false;
            audioSource.clip = landSound;
            audioSource.Play();
        }

        MoodChooser();

        pullingObjectLeft = pushPullScript.pullingLeft;
        pullingObjectRight = pushPullScript.pullingRight;
        connected = pushPullScript.connected;
    }

    public bool IsGrounded()
    {
        //return Physics2D.Raycast(transform.position, Vector2.down, 1.3f, platformLayerMask) != false;
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
    }

    void MoodChooser()
    {
        //Mood system
        if (IsGrounded())
        {
            if (Input.GetKey(KeyCode.Alpha1) && numb == false)
            {
                numb = true;
                angry = false;
                anxious = false;
                fear = false;

                audioSource.loop = false;
                audioSource.clip = numbSound;
                audioSource.Play();

                MoveAnimation.SetBool("IsNumb", true);
                MoveAnimation.SetBool("IsAngry", false);
                MoveAnimation.SetBool("IsAnxiety", false);
                MoveAnimation.SetBool("IsFear", false);
            }
            if (Input.GetKey(KeyCode.Alpha2) && angry == false)
            {
                numb = false;
                angry = true;
                anxious = false;
                fear = false;

                audioSource.loop = false;
                audioSource.clip = angrySound;
                audioSource.Play();

                MoveAnimation.SetBool("IsNumb", false);
                MoveAnimation.SetBool("IsAngry", true);
                MoveAnimation.SetBool("IsAnxiety", false);
                MoveAnimation.SetBool("IsFear", false);
            }
            if (Input.GetKey(KeyCode.Alpha3) && anxious == false)
            {
                numb = false;
                angry = false;
                anxious = true;
                fear = false;

                audioSource.loop = false;
                audioSource.clip = anxietySound;
                audioSource.Play();

                MoveAnimation.SetBool("IsNumb", false);
                MoveAnimation.SetBool("IsAngry", false);
                MoveAnimation.SetBool("IsAnxiety", true);
                MoveAnimation.SetBool("IsFear", false);
            }
            if (Input.GetKey(KeyCode.Alpha4) && fear == false)
            {
                numb = false;
                angry = false;
                anxious = false;
                fear = true;

                MoveAnimation.SetBool("IsNumb", false);
                MoveAnimation.SetBool("IsAngry", false);
                MoveAnimation.SetBool("IsAnxiety", false);
                MoveAnimation.SetBool("IsFear", true);
            }
        }

        MoveDirection(numb, angry, anxious, fear);
    }

    void MoveDirection(bool numb, bool angry, bool anxious, bool fear)
    {
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
        else if (fear)
        {
            moveSpeed = scaredPlayerSpeed;

            audioSource.loop = true;
            audioSource.clip = runSound;
            audioSource.Play();
        }
        //Change jump height when anxious
        if (numb || angry)
        {
            jumpHeight = playerJumpHeight;
            anxietyFog.SetActive(false);
        }
        else if (anxious)
        {
            jumpHeight = anxiousPlayerJumpHeight;
            anxietyFog.SetActive(true);
        }
        else if(fear)
        {
            jumpHeight = fearPlayerJumpHeight;
            anxietyFog.SetActive(false);
        }

        //Visual system
        FlipPlayerModel(direction, angry);
        MoveAnimation.SetFloat("Speed", Mathf.Abs(direction));
    }

    void FlipPlayerModel(float direction, bool angry)
    {
        if (direction == 1 && angry == false || direction == -1 && angry || direction == 1 && connected && pullingObjectLeft || direction == -1 && connected && pullingObjectRight)
            spriteRenderer.flipX = false;
        else if (direction == -1 && angry == false || direction == 1 && angry || direction == -1 && connected && pullingObjectLeft || direction == -1 && connected && pullingObjectRight)
            spriteRenderer.flipX = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * pushLength);
    }

    /*
    void temperaryMovementSystem()
    {
        //Mood system
        if (Input.GetKey(KeyCode.Alpha1) && numb == false)
        {
            numb = true;
            angry = false;
            anxious = false;
            fear = false;

            audioSource.loop = false;
            audioSource.clip = numbSound;
            audioSource.Play();

            MoveAnimation.SetBool("IsNumb", true);
            MoveAnimation.SetBool("IsAngry", false);
            MoveAnimation.SetBool("IsAnxiety", false);
            MoveAnimation.SetBool("IsFear", false);
        }
        if (Input.GetKey(KeyCode.Alpha2) && angry == false)
        {
            numb = false;
            angry = true;
            anxious = false;
            fear = false;

            audioSource.loop = false;
            audioSource.clip = angrySound;
            audioSource.Play();

            MoveAnimation.SetBool("IsNumb", false);
            MoveAnimation.SetBool("IsAngry", true);
            MoveAnimation.SetBool("IsAnxiety", false);
            MoveAnimation.SetBool("IsFear", false);
        }
        if (Input.GetKey(KeyCode.Alpha3) && anxious == false)
        {
            numb = false;
            angry = false;
            anxious = true;
            fear = false;

            audioSource.loop = false;
            audioSource.clip = anxietySound;
            audioSource.Play();

            MoveAnimation.SetBool("IsNumb", false);
            MoveAnimation.SetBool("IsAngry", false);
            MoveAnimation.SetBool("IsAnxiety", true);
            MoveAnimation.SetBool("IsFear", false);
        }
        if (Input.GetKey(KeyCode.Alpha4) && fear == false)
        {
            numb = false;
            angry = false;
            anxious = false;
            fear = true;

            MoveAnimation.SetBool("IsNumb", false);
            MoveAnimation.SetBool("IsAngry", false);
            MoveAnimation.SetBool("IsAnxiety", false);
            MoveAnimation.SetBool("IsFear", true);
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
        else if (fear)
        {
            moveSpeed = scaredPlayerSpeed;

            audioSource.loop = true;
            audioSource.clip = runSound;
            audioSource.Play();
        }
        //Change jump height when anxious
        if (numb || angry || fear)
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
        FlipPlayerModel(direction, angry);
        MoveAnimation.SetFloat("Speed", Mathf.Abs(direction));
    }
    */
}
