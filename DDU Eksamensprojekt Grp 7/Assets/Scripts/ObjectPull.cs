using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPull : MonoBehaviour
{
	public float defaultMass;
	public float imovableMass;
	public bool beingPushed;
	float xPos;

	public Vector2 lastPos;

	public int mode;
	public int colliding;
	// Use this for initialization
	void Start()
	{
		xPos = transform.position.x;
		lastPos = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (mode == 0)
		{
			if (beingPushed == false)
				transform.position = new Vector2(xPos, transform.position.y);
			else
				xPos = transform.position.x;
		}
		else if (mode == 1)
		{
			if (beingPushed == false)
				GetComponent<Rigidbody2D>().mass = imovableMass;
			else
				GetComponent<Rigidbody2D>().mass = defaultMass;
		}
	}
}
