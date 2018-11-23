using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
	//Player move speed
	public float moveSpeed;
	//Player rigidbody
	private Rigidbody myRigidbody;
	//Move Input
	private Vector3 moveInput;
	//Move velocity
	private Vector3 moveVelocity;
	//The main game camera
	private Camera mainCamera;
	//The reference to the gun
	public GunController theGun;
	//Bool for checking if the player is using a controller
	public bool useController;
	//Reference to the Game Manager
	private GameManager theGameManager;
	//Damage Flashlength
	public float flashLength;
	//Damage flash counter
	public float flashCounter;
	//This objects renderer
	private Renderer rend;
	//A Custom colour that is stored
	private Color storedColor;
	//The Particle effect upon death
	public ParticleSystem deathEffect;
	//Reference to the Health Manager
	public HealthManager theHealthManager;

	//ship mesh
	public Transform shipMesh;

	//As soon as the game is played
	void Awake()
	{
		//Find the GameManager
		theGameManager = FindObjectOfType<GameManager>();
		//Set the player controller in the game manager
		theGameManager.SetPlayerController(this);
	}
	
	// Use this for initialization
	void Start ()
	{
		//Find this objects renderer
		rend = GetComponent<Renderer>();
		//Create a stored colour value on the renderer
		storedColor = rend.material.GetColor("_Color");
		//Find this objects rigidbody
		myRigidbody = GetComponent<Rigidbody>();
		//Get the camera
		mainCamera = FindObjectOfType<Camera>();
		//Find the HealthManager and subscribe to an event based on the health variable changing
		GetComponent<HealthManager>().OnHealthChanged += RespondToHealthChanged;
		//Get the health manager and subscribe to the Onplayer death event and listen for when to call this scripts Onplayer death function
		GetComponent<HealthManager>().OnPlayerDeath += OnPlayerDeath;
		theGameManager = FindObjectOfType<GameManager>();

	}

	//Respond to Damage
	private void RespondToHealthChanged(float amount)
	{
		//Set the flash counter value to the amount of the flash length
		flashCounter = flashLength;
		//Set the players colour to red when damaged
		rend.material.SetColor("_Color", Color.red);
	}
	//Run is when the player has died
	public void OnPlayerDeath(float amount)
	{
		//Checks if the current health of this object is <= 0 then...
		if (theHealthManager.currentHealth <= 0)
		{
			theHealthManager.playerIsDead = true;
			//Spawns the death effect at the relative location
			Instantiate(deathEffect, transform.position, transform.rotation);
			//sets the particle effect visibility to false once it's finished playing
			Destroy(gameObject);
			//theHealthManager.playerIsDead = true;
			//gameObject.SetActive(false);
			//Destroy the particle effect
			//Destroy(deathEffect);
			//Load the gameover scene
			//SceneManager.LoadScene("GameOver");
		}
	}
	

	// Update is called once per frame
	void Update()
	{
		moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput * moveSpeed;

		//ship rotation
		//shipmesh.localRotation = Quaternion.Euler(0,0,moveInput.x*100);
		shipMesh.localRotation = Quaternion.Lerp(shipMesh.localRotation, Quaternion.Euler(0, 0, moveInput.x * 45), Time.deltaTime * 10f);
		//Check if the flash counter is greater than zero
		if (flashCounter > 0)
		{
			//set the flash counter to the current time value then - from it(countdown)
			flashCounter -= Time.deltaTime;
			//Check if the flash counter is less than equal to zero
			if (flashCounter <= 0)
			{
				//Sets the renderers flash colour from the stored colour
				rend.material.SetColor("_Color", storedColor);
			}
		}
		
		//Rotate with the mouse(if Not using the controller)
		if (!useController)
		{
			//Cast a ray relevant to where the player points the mouse
			Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
			//Create a ground variable
			Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
			//Ray length variable
			float rayLength;

			if (groundPlane.Raycast(cameraRay, out rayLength))
			{
				//Determine where the player is looking, cast the ray to the length of distance
				Vector3 pointToLook = cameraRay.GetPoint(rayLength);
				//Visualise the raycast
				Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
				//Show where the plaeyr is looking
				transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
			}
			//If the fire button is pressed then fire the gun
			if (Input.GetMouseButtonDown(0))
			{
				//Set the firing bool to true 
				theGun.isFiring = true;
			}
			//If the fire button is released then stop firing
			if (Input.GetMouseButtonUp(0))
			{
				//Set the firing bool to false
				theGun.isFiring = false;
			}
		}

		//Rotate with controller(If the player is using controller)
		if (useController)
		{
			//Move the player using the analog sticks, with custom input for the right analog stick for turning the direction of the player
			Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RightHorizontal") + Vector3.forward * -Input.GetAxisRaw("RightVertical");
			
			if (playerDirection.sqrMagnitude > 0.0f)
			{
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
			}
			//If the right bumper button is pressed down then fire the gun
			if (Input.GetKeyDown(KeyCode.Joystick1Button5))
			{
				//Set the firing bool to true
				theGun.isFiring = true;
			}
			//If the right bumper button is released then stop firing the gun
			if (Input.GetKeyUp(KeyCode.Joystick1Button5))
			{
				//Set the firing bool to false
				theGun.isFiring = false;
			}
		}
	}

	void FixedUpdate()
	{
		//Set the rigidbody velocity equal to the moveVelocity variable.
		myRigidbody.velocity = moveVelocity;
	}



}
