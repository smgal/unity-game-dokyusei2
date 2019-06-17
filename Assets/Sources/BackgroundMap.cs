using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundMap : MonoBehaviour
{
	List<Sprite> bg_sprites;
	List<Sprite> fg_sprites;

	Tilemap tile_map_bg;
	MatiTile tile_base;

	void Start()
    {
		ResourceManager.LoadDokyuMap();
		ResourceManager.LoadDokyuImage(out bg_sprites, out fg_sprites);

		tile_map_bg = GetComponent<Tilemap>();

		tile_base = MatiTile.CreateInstance<MatiTile>();
		tile_base.sprites = bg_sprites;
	}

	void Update()
    {
		if (Input.GetKey(KeyCode.LeftArrow))
			ResourceManager.yui.x += 1;
		if (Input.GetKey(KeyCode.RightArrow))
			ResourceManager.yui.x -= 1;
		if (Input.GetKey(KeyCode.UpArrow))
			ResourceManager.yui.y -= 1;
		if (Input.GetKey(KeyCode.DownArrow))
			ResourceManager.yui.y += 1;

		tile_map_bg.ClearAllTiles();
		for (int y = -5; y <= 5; y++)
		{
			Vector3Int vec3 = new Vector3Int(0, y, 0);

			for (int x = -9; x <= 9; x++)
			{
				vec3.x = x;
				tile_map_bg.SetTile(vec3, tile_base);
			}
		}
	}
}
