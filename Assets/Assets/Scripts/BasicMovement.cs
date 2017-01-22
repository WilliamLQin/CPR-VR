using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

	public float MoveSpeed;
	public Transform Reference;

	void Update() {

		float vertMove = Input.GetAxis ("Vertical");
		float horMove = Input.GetAxis ("Horizontal");

		Vector3 thrust = new Vector3 (Reference.forward.x, 0.0f, Reference.forward.z).normalized;
		Vector3 steer = new Vector3 (Reference.right.x, 0.0f, Reference.right.z).normalized;

		transform.position += thrust * vertMove * MoveSpeed * Time.deltaTime;
		transform.position += steer * horMove * MoveSpeed * Time.deltaTime;

	}
}
