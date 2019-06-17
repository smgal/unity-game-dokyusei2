
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// [CreateAssetMenu]
public class MatiTile : TileBase
{
	public List<Sprite> sprites;

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		int ix_tile = ResourceManager.map[position.x + ResourceManager.yui.x, position.y + ResourceManager.yui.y];

		tileData.sprite = sprites[ix_tile];
	}
}
