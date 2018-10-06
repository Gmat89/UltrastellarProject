using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float moveSpeed;
	private Rigidbody myRigidbody;

	private Vector3 moveInput;
	private Vector3 moveVelocity;

	private Camera mainCamera;

	public GunController theGun;

	public bool useController;

	// Use this for initialization
	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update()
	{
		moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput * moveSpeed;
		

		//Rotate with the mouse(Not using the controller)
			if (!useController)
		{
			Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
			Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
			float rayLength;

			if (groundPlane.Raycast(cameraRay, out rayLength))
			{
				Vector3 pointToLook = cameraRay.GetPoint(rayLength);
				Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

				transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
			}
			//If the fire button is pressed then fire the gun
			if (Input.GetMouseButtonDown(0))
			{
				theGun.isFiring = true;
			}
			//If the fire button is released then stop firing
			if (Input.GetMouseButtonUp(0))
			{
				theGun.isFiring = false;
			}
		}

		//Rotate with controller(If using controller)
		if (useController)
		{
			Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RightHorizontal") + Vector3.forward * -Input.GetAxisRaw("RightVertical");
			if (playerDirection.sqrMagnitude > 0.0f)
			{
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
			}
			//If the right bumper button is pressed down then fire the gun
			if (Input.GetKeyDown(KeyCode.Joystick1Button5))
			{
				theGun.isFiring = true;
			}
			//If the right bumper button is released then stop firing the gun
			if (Input.GetKeyUp(KeyCode.Joystick1Button5))
			{
				theGun.isFiring = false;
			}
		}
	}

	void FixedUpdate()
	{
		myRigidbody.velocity = moveVelocity;
	}
}
