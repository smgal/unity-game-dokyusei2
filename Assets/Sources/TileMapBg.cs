using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class MatiTileBg : TileBase
{
	public List<Sprite> sprites;

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		int ix_tile = ResourceManager.map[position.x + ResourceManager.yui.x, position.y + ResourceManager.yui.y];

		tileData.sprite = sprites[ix_tile];
	}
}

public class TileMapBg : MonoBehaviour
{
	Tilemap tile_map_bg;
	MatiTileBg tile_base;

	void Start()
    {
		tile_map_bg = GetComponent<Tilemap>();

		tile_base = MatiTileBg.CreateInstance<MatiTileBg>();
		tile_base.sprites = ResourceManager.bg_tile_images;
	}

	void Update()
    {
		tile_map_bg.ClearAllTiles();
		for (int y = -10; y <= 10; y++)
		{
			Vector3Int vec3 = new Vector3Int(0, y, 0);

			for (int x = -16; x <= 16; x++)
			{
				vec3.x = x;
				tile_map_bg.SetTile(vec3, tile_base);
			}
		}
	}
}
