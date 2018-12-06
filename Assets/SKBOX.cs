using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SKBOX : MonoBehaviour
{
	public Material BACKGROUND;
	public Camera Mycam;
	public Vector2 trippy;
	float scrollSpeed = 0.5f;
	Renderer rend;

	void Start()
	{
		BACKGROUND = GetComponent<Material>();
		rend = GetComponent<Renderer>();
		Mycam = GetComponent<Camera>();
	}

	void Update()
	{
		float offset = Time.time * scrollSpeed;
		rend.material.mainTextureOffset = new Vector2(offset, 0);
	}
}
