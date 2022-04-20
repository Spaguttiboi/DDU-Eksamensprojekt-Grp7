using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    float movementSmooth = .05f;
    bool aircontrol = true;

    bool facingRight = true;
    Vector3 velocity = Vector3.zero;

    Rigidbody2D PlayerRigidbody2D;

    void Awake()
    {
        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float move, bool crouch, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, PlayerRigidbody2D.velocity.y);
        PlayerRigidbody2D.velocity = Vector3.SmoothDamp(PlayerRigidbody2D.velocity, targetVelocity, ref velocity, movementSmooth);
        

    }

    private void Flip()
    {

    }

}
