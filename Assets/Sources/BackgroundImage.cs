using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
	public TextAsset image_asset;

	// Start is called before the first frame update
	void Start()
    {
		Texture2D texture = new Texture2D(2, 2);
		texture.LoadImage(image_asset.bytes);
		texture.filterMode = FilterMode.Point;

		GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
	}
}
