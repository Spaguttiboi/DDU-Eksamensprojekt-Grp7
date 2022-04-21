using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullObject : MonoBehaviour
{
	[SerializeField] private LayerMask objectLayerMask;

	private PlayerMovement test;
	GameObject moveableObject;
	bool correctMood;

	private void Start()
	{
		test = gameObject.GetComponent<PlayerMovement>();
	}


	private void Update()
	{
		correctMood = test.angry;
		RaycastHit2D contactright = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, objectLayerMask);

		if (PushableObjectRight() && Input.GetKeyDown(KeyCode.E) && correctMood)
		{
			moveableObject = contactright.collider.gameObject;

			moveableObject.GetComponent<FixedJoint2D>().enabled = false;
			moveableObject.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
		}
		else if (Input.GetKeyUp(KeyCode.E) && correctMood)
		{
			moveableObject.GetComponent<FixedJoint2D>().enabled = true;
			moveableObject.GetComponent<FixedJoint2D>().connectedBody = null;
		}

		RaycastHit2D contactleft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, objectLayerMask);

		if (PushableObjectLeft() && Input.GetKeyDown(KeyCode.E) && correctMood)
		{
			moveableObject = contactleft.collider.gameObject;

			moveableObject.GetComponent<FixedJoint2D>().enabled = false;
			moveableObject.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
		}
		else if (Input.GetKeyUp(KeyCode.E) && correctMood)
		{
			moveableObject.GetComponent<FixedJoint2D>().enabled = true;
			moveableObject.GetComponent<FixedJoint2D>().connectedBody = null;
		}
	}

	private bool PushableObjectRight()
	{
		return Physics2D.Raycast(transform.position, Vector2.right, 0.5f, objectLayerMask) != false;
	}

	private bool PushableObjectLeft()
	{
		return Physics2D.Raycast(transform.position, Vector2.left, 0.5f, objectLayerMask) != false;
	}

}

