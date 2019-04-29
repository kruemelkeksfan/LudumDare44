using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralisticCameraController : MonoBehaviour
	{
	public Vector3 LOOK_AT = Vector3.zero;
	public float CAMERA_SPEED = 5.0f;
	public float ROTATION_FACTOR = 1.0f;
	public float SCROLL_FACTOR = 8.0f;
	public float MIN_HEIGHT = 40.0f;
	public float MAX_HEIGHT = 200.0f;
	public float MIN_SCROLL_DISTANCE = 200.0f;
	public float MAX_SCROLL_DISTANCE = 500.0f;
	public float ROTATION_AXIS_SAFETY_DISTANCE = 100.0f;
	public float MIN_ANGLE = 10.0f;
	public float MAX_ANGLE = 80.0f;

	private SettingManager settings = null;

	private void Start()
		{
		settings = GameObject.Find("SettingManager").GetComponent<SettingManager>();
		}

	private void Update()
		{
		CAMERA_SPEED = settings.getCameraSpeed();

		Vector3 direction = new Vector3();
		Vector3 rotation = transform.rotation.eulerAngles;

		// Hide and lock cursor on MMB down and show and unlock cursor on MMB up
		if(Input.GetMouseButtonDown(2))
			{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			}
		if(Input.GetMouseButtonUp(2))
			{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			}

		// Get mouse movement directions
		if(Input.GetMouseButton(2))
			{
			direction -= new Vector3(Input.GetAxis("Mouse X"), 0.0f, 0.0f);
			rotation.x -= Input.GetAxis("Mouse Y") * CAMERA_SPEED * ROTATION_FACTOR;
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

		// Translate camera and check distance to rotation axis
		Vector3 oldposition = transform.position;
		transform.position += Quaternion.Euler(rotation) * (direction * CAMERA_SPEED);

		Vector2 safetyvector =  new Vector2(LOOK_AT.x, LOOK_AT.z) - new Vector2(transform.position.x, transform.position.z);
		if(safetyvector.magnitude < ROTATION_AXIS_SAFETY_DISTANCE)
			{
			transform.position = oldposition;
			}

		// Zoom with ScrollWheel
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		float scrolldistance = (transform.position - LOOK_AT).magnitude;
		if((scrolldistance >= MIN_SCROLL_DISTANCE && scrolldistance <= MAX_SCROLL_DISTANCE)
			|| (scroll < 0 && scrolldistance <= MIN_SCROLL_DISTANCE)
			|| (scroll > 0 && scrolldistance >= MAX_SCROLL_DISTANCE))
			{
			transform.position += (transform.rotation * Vector3.forward) * (scroll * CAMERA_SPEED * SCROLL_FACTOR);
			}

		// After applying movement refocus monument
		rotation.y = Quaternion.LookRotation(LOOK_AT - transform.position).eulerAngles.y;

		// After transformation firming, to avoid too large threshold violation
		scrolldistance = (transform.position - LOOK_AT).magnitude;
		if(scrolldistance < MIN_SCROLL_DISTANCE)
			{
			transform.position += (Quaternion.Euler(rotation) * Vector3.back) * (MIN_SCROLL_DISTANCE - scrolldistance);
			}
		else if(scrolldistance > MAX_SCROLL_DISTANCE)
			{
			transform.position += (Quaternion.Euler(rotation) * Vector3.forward) * (scrolldistance - MAX_SCROLL_DISTANCE);
			}
		if(transform.position.y < MIN_HEIGHT)
			{
			transform.position = new Vector3(transform.position.x, MIN_HEIGHT, transform.position.z);
			}
		else if(transform.position.y > MAX_HEIGHT)
			{
			transform.position = new Vector3(transform.position.x, MAX_HEIGHT, transform.position.z);
			}

		// After rotation firming, to avoid too large threshold violation
		if(rotation.x < MIN_ANGLE)
			{
			rotation.x = MIN_ANGLE;
			}
		else if(rotation.x < 180.0f && rotation.x > MAX_ANGLE)
			{
			rotation.x = MAX_ANGLE;
			}

		// Rotate camera
		transform.rotation = Quaternion.Euler(rotation);
		}
	}
