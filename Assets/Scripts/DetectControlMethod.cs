using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectControlMethod : MonoBehaviour
{
	public PlayerController thePlayer;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//Detect Mouse Input, if any of the 3 mouse buttons are pressed then the game will setup for Keyboard/Mouse Controls
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
		{
			thePlayer.useController = false;
		}
		//If the mouse is moved then the game will use mouse controls
		if (Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0.0f)
		{
			thePlayer.useController = false;
		}

		//Detect Controller Input
		if (Input.GetAxisRaw("RightHorizontal") != 0.0f || Input.GetAxisRaw("RightVertical") != 0.0f)
		{
			thePlayer.useController = true;
		}
		//If any button is pressed on the controller then switch input to controller
		if (Input.GetKey(KeyCode.Joystick1Button0) ||
			Input.GetKey(KeyCode.Joystick1Button1) ||
			Input.GetKey(KeyCode.Joystick1Button2) ||
			Input.GetKey(KeyCode.Joystick1Button3) ||
			Input.GetKey(KeyCode.Joystick1Button4) ||
			Input.GetKey(KeyCode.Joystick1Button5) ||
			Input.GetKey(KeyCode.Joystick1Button6) ||
			Input.GetKey(KeyCode.Joystick1Button7) ||
			Input.GetKey(KeyCode.Joystick1Button8) ||
			Input.GetKey(KeyCode.Joystick1Button9) ||
			Input.GetKey(KeyCode.Joystick1Button10))
		{
			thePlayer.useController = true;
		}
	}
}



			
		


