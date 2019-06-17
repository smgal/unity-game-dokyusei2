using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
	void Awake()
	{
		ResourceManager.LoadDokyuMap();
		ResourceManager.LoadDokyuImage();
	}

	void Start()
    {
	}

	void Update()
    {
		if (Input.GetKey(KeyCode.LeftArrow))
			ResourceManager.yui.x -= 1;
		if (Input.GetKey(KeyCode.RightArrow))
			ResourceManager.yui.x += 1;
		if (Input.GetKey(KeyCode.UpArrow))
			ResourceManager.yui.y -= 1;
		if (Input.GetKey(KeyCode.DownArrow))
			ResourceManager.yui.y += 1;
	}
}
