using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxEffects : MonoBehaviour
{
	public Material skyBox;

	public float scrollSpeed;
	// Use this for initialization
	void Start ()
	{
		RenderSettings.skybox = skyBox;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float offset = Time.time * scrollSpeed;
		skyBox.mainTextureOffset = new Vector2(offset, 5);
	}
}
