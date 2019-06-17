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

	static int tick = 0;

	void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
			ResourceManager.yui.Move(-1, 0);
		if (Input.GetKey(KeyCode.RightArrow))
			ResourceManager.yui.Move(+1, 0);
		if (Input.GetKey(KeyCode.UpArrow))
			ResourceManager.yui.Move(0, -1);
		if (Input.GetKey(KeyCode.DownArrow))
			ResourceManager.yui.Move(0, +1);

		if (++tick % 20 == 0)
			ResourceManager.yui.Move();
	}
}
