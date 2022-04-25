using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullObject : MonoBehaviour
{
	[SerializeField] private LayerMask objectLayerMask;

	private PlayerMovement movementScript;
	Animator playerAnimator;
	GameObject moveableObject;
	bool correctMood;
	public bool connected;

	public bool pullingLeft;
	public bool pullingRight;

	private void Start()
	{
		movementScript = gameObject.GetComponent<PlayerMovement>();
		playerAnimator = gameObject.GetComponent<Animator>();
	}


	private void Update()
	{
		//Checks if the player is in the correct mood and that there is a moveable object close enough to him.
		correctMood = movementScript.angry;
		RaycastHit2D contactright = Physics2D.Raycast(transform.position, Vector2.right, 0.7f, objectLayerMask);
		RaycastHit2D contactleft = Physics2D.Raycast(transform.position, Vector2.left, 0.7f, objectLayerMask);

		//Connects the player to the object and disables the FixedJoint2D
		if (PushableObjectRight() && Input.GetKeyDown(KeyCode.E) && correctMood)
		{
			moveableObject = contactright.collider.gameObject;

			moveableObject.GetComponent<FixedJoint2D>().enabled = false;
			moveableObject.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();

			playerAnimator.SetBool("IsPushing", true);
			connected = true;
		}
		else if (PushableObjectLeft() && Input.GetKeyDown(KeyCode.E) && correctMood)
		{
			moveableObject = contactleft.collider.gameObject;

			moveableObject.GetComponent<FixedJoint2D>().enabled = false;
			moveableObject.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();


			playerAnimator.SetBool("IsPushing", true);
			connected = true;
		}
		else if (Input.GetKeyUp(KeyCode.E) && correctMood)
		{
			moveableObject.GetComponent<FixedJoint2D>().enabled = true;
			moveableObject.GetComponent<FixedJoint2D>().connectedBody = null;

			pullingLeft = false;
			pullingRight = false;
			connected = false;
			playerAnimator.SetBool("IsPushing", false);
			playerAnimator.SetBool("IsPulling", false);
		}
	}

	private bool PushableObjectRight()
	{
		return Physics2D.Raycast(transform.position, Vector2.right, 0.7f, objectLayerMask) != false;
	}

	private bool PushableObjectLeft()
	{
		return Physics2D.Raycast(transform.position, Vector2.left, 0.7f, objectLayerMask) != false;
	}

}

