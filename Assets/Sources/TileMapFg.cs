using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class MatiTileFg : TileBase
{
	public List<Sprite> sprites;

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		int x = position.x + ResourceManager.yui.x;
		int y = position.y + ResourceManager.yui.y;

		int ix_tile = ResourceManager.map[x, y];

		tileData.sprite = sprites[ix_tile];
	}
}

public class TileMapFg : MonoBehaviour
{
	Tilemap tile_map_fg;
	MatiTileFg tile_base;

	void Start()
	{
		tile_map_fg = GetComponent<Tilemap>();

		tile_base = MatiTileFg.CreateInstance<MatiTileFg>();
		tile_base.sprites = ResourceManager.fg_tile_images;
	}

	void Update()
	{
		tile_map_fg.ClearAllTiles();
		for (int y = -10; y <= 10; y++)
		{
			Vector3Int vec3 = new Vector3Int(0, y, 0);

			for (int x = -16; x <= 16; x++)
			{
				vec3.x = x;
				tile_map_fg.SetTile(vec3, tile_base);
			}
		}
	}
}
