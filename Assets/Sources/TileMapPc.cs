using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class MatiTilePc : TileBase
{
	public List<Sprite> sprites;

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		tileData.sprite = sprites[ResourceManager.yui.face];
	}
}

public class TileMapPc : MonoBehaviour
{
	Tilemap tile_map_pc;
	MatiTilePc tile_base;

	void Start()
    {
		tile_map_pc = GetComponent<Tilemap>();

		tile_base = MatiTilePc.CreateInstance<MatiTilePc>();
		tile_base.sprites = ResourceManager.yui_images;
	}

	// Update is called once per frame
	void Update()
    {
		tile_map_pc.ClearAllTiles();

		for (int y = -0; y <= 0; y++)
		{
			Vector3Int vec3 = new Vector3Int(0, y, 0);

			for (int x = -0; x <= 0; x++)
			{
				vec3.x = x;
				tile_map_pc.SetTile(vec3, tile_base);
			}
		}
	}
}
