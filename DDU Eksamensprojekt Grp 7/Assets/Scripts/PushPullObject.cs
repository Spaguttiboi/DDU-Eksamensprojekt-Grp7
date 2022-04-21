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
		RaycastHit2D contact = Physics2D.Raycast(transform.position, Vector2.right, 0.7f, objectLayerMask);

		if (PushableObject() && Input.GetKeyDown(KeyCode.E) && correctMood)
		{
			moveableObject = contact.collider.gameObject;

			moveableObject.GetComponent<FixedJoint2D>().enabled = false;
			moveableObject.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
		}
		else if (Input.GetKeyUp(KeyCode.E) && correctMood)
		{
			moveableObject.GetComponent<FixedJoint2D>().enabled = true;
			moveableObject.GetComponent<FixedJoint2D>().connectedBody = null;
		}
	}

	private bool PushableObject()
	{
		return Physics2D.Raycast(transform.position, Vector2.right, 0.7f, objectLayerMask) != false;
	}
}

