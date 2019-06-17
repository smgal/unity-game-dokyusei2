
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourceManager
{
	public class Yui
	{
		public int x = 70;
		public int y = 50;
		public int face = 0;
	}

	public class Map
	{
		public int x_size = 0;
		public int y_size = 0;
		public ushort[] map_data = new ushort[0];

		public ushort this[int x, int y] { get { return _GetData(x, y); } }

		public bool isBlocked(int x, int y)
		{
			int index = y * x_size + x;
			return (index >= 0 && index < map_data.Length) ? (map_data[index] & 0x8000) > 0 : false;
		}

		private ushort _GetData(int x, int y)
		{
			int index = y * x_size + x;
			return (ushort)((index >= 0 && index < map_data.Length) ? map_data[index] & 0x7FFF : 0x00);
		}
	}

	static public Yui yui = new Yui();
	static public Map map = new Map();

	static public List<Sprite> bg_tile_images;
	static public List<Sprite> fg_tile_images;

	static public void LoadDokyuImage()
	{
		Color TRANSPARENT_COLOR = new Color(0,0,0,0);

		Color[] palette_data = new Color[16];

		{
			TextAsset palette_asset = Resources.Load("MATI1.PAL") as TextAsset;
			Stream palette_stream = new MemoryStream(palette_asset.bytes);

			Debug.Assert(palette_stream.Length == 16 * 3);
			BinaryReader palette = new BinaryReader(palette_stream);

			for (int i = 0; i < 16; i++)
			{
				byte[] read = palette.ReadBytes(3);

				palette_data[i].a = 1.0f;
				palette_data[i].r = (read[0] * 4U) / 255.0f;
				palette_data[i].g = (read[1] * 4U) / 255.0f;
				palette_data[i].b = (read[2] * 4U) / 255.0f;
			}
		}

		Texture2D texture_bg = new Texture2D(2, 2, TextureFormat.RGB24, false);
		Texture2D texture_fg = new Texture2D(2, 2, TextureFormat.RGBA32, false);

		{
			const int MAX_TILE = 1000;
			const int IMAGE_SIZE = 134;

			TextAsset tile_data_asset = Resources.Load("MATI1.IMG") as TextAsset;
			Stream tile_data_stream = new MemoryStream(tile_data_asset.bytes);

			Debug.Assert(tile_data_stream.Length == MAX_TILE * IMAGE_SIZE);
			BinaryReader tile_data_bin = new BinaryReader(tile_data_stream);

			byte[][] tile_data = new byte[MAX_TILE][];

			for (int i = 0; i < MAX_TILE; i++)
				tile_data[i] = tile_data_bin.ReadBytes(IMAGE_SIZE);

			int image_width = (tile_data[0][0] + tile_data[0][1] * 256) + 1;
			int image_height = (tile_data[0][2] + tile_data[0][3] * 256) + 1;
			int image_pitch = image_width / 8;

			// IMG(16x16) - 2
			// Debug.Log(string.Format("IMG({0}x{1}) - {2}", image_width, image_height, image_pitch));

			int buffer_width = image_width * MAX_TILE;
			int buffer_height = image_height;
			int buffer_size = buffer_width * buffer_height * sizeof(uint);

			texture_bg.Resize(buffer_width, buffer_height);
			texture_fg.Resize(buffer_width, buffer_height);

			// BUF(16000x16) - 1024000
			// Debug.Log(string.Format("BUF({0}x{1}) - {2}", buffer_width, buffer_height, buffer_size));

			for (int i = 0; i < MAX_TILE; i++)
			{
				int ix_tile = 4;

				int ix_tile_i = ix_tile + image_pitch * 0;
				int ix_tile_r = ix_tile + image_pitch * 1;
				int ix_tile_g = ix_tile + image_pitch * 2;
				int ix_tile_b = ix_tile + image_pitch * 3;

				for (int y = 0; y < image_height; y++)
				{
					int base_index = image_pitch * 4 * y;

					for (int x = 0; x < image_width; x++)
					{
						int index = base_index + x / 8;
						int shift = x % 8;

						uint ix_palette = 0U;
						ix_palette |= ((tile_data[i][ix_tile_r + index] & (0x80 >> shift)) != 0) ? 0x04 : 0U;
						ix_palette |= ((tile_data[i][ix_tile_g + index] & (0x80 >> shift)) != 0) ? 0x02 : 0U;
						ix_palette |= ((tile_data[i][ix_tile_b + index] & (0x80 >> shift)) != 0) ? 0x01 : 0U;

						texture_bg.SetPixel(i * image_width + x, y, palette_data[ix_palette]);

						if ((tile_data[i][ix_tile_i + index] & (0x80 >> shift)) != 0)
							texture_fg.SetPixel(i * image_width + x, y, palette_data[ix_palette]);
						else
							texture_fg.SetPixel(i * image_width + x, y, TRANSPARENT_COLOR);
					}
				}
			}

			Vector2 pivot = new Vector2(1.0f, 1.0f);

			texture_bg.filterMode = FilterMode.Point;
			texture_bg.Apply();

			bg_tile_images = new List<Sprite>();
			for (int i = 0; i < MAX_TILE; i++)
				bg_tile_images.Add(Sprite.Create(texture_bg, new Rect(image_width * i, 0.0f, image_width, image_height), pivot, 16.0f));

			texture_fg.filterMode = FilterMode.Point;
			texture_fg.Apply();

			fg_tile_images = new List<Sprite>();
			for (int i = 0; i < MAX_TILE; i++)
				fg_tile_images.Add(Sprite.Create(texture_fg, new Rect(image_width * i, 0.0f, image_width, image_height), pivot, 16.0f));
		}
	}

	static public void LoadDokyuMap()
	{
		TextAsset map_data_asset = Resources.Load("MATI1.MAP") as TextAsset;
		Stream map_data_stream = new MemoryStream(map_data_asset.bytes);
		BinaryReader map_data_bin = new BinaryReader(map_data_stream);

		// little endian
		map.x_size = map_data_bin.ReadUInt16();
		map.y_size = map_data_bin.ReadUInt16();

		map.map_data = new ushort[map.x_size * map.y_size];

		// little endian
		for (int i = 0; i < map.map_data.Length; i++)
			map.map_data[i] = map_data_bin.ReadUInt16();
	}
}
