using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float rotateSpeed;
	public float rotateBodySpeed;
	public Field field;
	
	private Rigidbody rigidBody;
	private Animator animator;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
	}
	
	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		
		transform.Rotate(0, horizontal * rotateSpeed, 0);
		
		var playerAngle = Mathf.Deg2Rad * (270 - transform.eulerAngles.y);
		var x2 = Mathf.Sin(playerAngle);
		var z2 = -Mathf.Cos(playerAngle);
		Vector3 movement = new Vector3(x2, 0f, z2) * vertical;
		rigidBody.AddForce(movement * speed);

		animator.speed = rigidBody.velocity.magnitude * rotateBodySpeed;

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Finish")
		{
			field.Finish();
		}
	}
}