using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralisticCameraController : MonoBehaviour
	{
	public Vector3 LOOK_AT = Vector3.zero;
	public float SCROLL_SPEED = 5.0f;
	public float MAXIMUM_ANGLE = 60.0f;

	private void Update()
		{
		// Hide and lock cursor on MMB down
		if(Input.GetMouseButtonDown(2))
			{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			}

		// Show and unlock cursor on MMB release
		if(Input.GetMouseButtonUp(2))
			{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			}

		// Get mouse movement directions
		Vector3 direction = new Vector3();
		if(Input.GetMouseButton(2))
			{
			direction -= new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
			}

		// Get keyboard movement directions
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
			direction += Vector3.up;
			}
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
			direction += Vector3.left;
			}
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
			direction += Vector3.down;
			}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
			direction += Vector3.right;
			}

		// Translate and rotate camera
		Vector3 rotation = transform.rotation.eulerAngles;
		Debug.Log("o: " + direction.y);
		if((rotation.x < 180.0f && rotation.x > MAXIMUM_ANGLE && direction.y > 0)
			|| (rotation.x >= 180.0f && rotation.x < 360.0f - MAXIMUM_ANGLE && direction.y < 0))
			{
			Debug.Log(rotation.x + " " + direction.y);
			direction.y = 0.0f;
			}
		Debug.Log("n: " + direction.y);

		transform.position += Quaternion.Euler(rotation.x, rotation.y, 0) * direction;
		transform.position += (transform.rotation * Vector3.forward) * (Input.GetAxis("Mouse ScrollWheel") * SCROLL_SPEED); // Zoom with ScrollWheel

		transform.LookAt(LOOK_AT);
		}
	}
