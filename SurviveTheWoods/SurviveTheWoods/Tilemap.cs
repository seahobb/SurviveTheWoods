﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SurviveTheWoods
{
    public class Tilemap
    {
        /// <summary>
        /// The dimensions of tiles and the map
        /// </summary>
        int _tileWidth, _tileHeight, _mapWidth, _mapHeight;

        /// <summary>
        /// The tileset texture
        /// </summary>
        Texture2D _tilesetTexture;

        /// <summary>
        /// The tile info in the tileset
        /// </summary>
        Rectangle[] _tiles;

        /// <summary>
        /// The tile map data
        /// </summary>
        int[] _map;

        /// <summary>
        /// The filename of the map file
        /// </summary>
        string _filename;

        public Tilemap(string filename)
        {
            _filename = filename;
        }

        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText(Path.Join(content.RootDirectory, _filename));
            var lines = data.Split('\n');

            // First line is the tileset filename
            var tilesetFilename = lines[0].Trim();
            _tilesetTexture = content.Load<Texture2D>(tilesetFilename);

            // Second line is the tile size
            var secondLine = lines[1].Split(',');
            _tileWidth = int.Parse(secondLine[0]);
            _tileHeight = int.Parse(secondLine[1]);

            // Now we can determine our tile bounds
            int tilesetColumns = _tilesetTexture.Width / _tileWidth;
            int tilesetRows = _tilesetTexture.Height / _tileHeight;
            _tiles = new Rectangle[tilesetColumns * tilesetRows];

            for (int y = 0; y < tilesetColumns; y++)
            {
                for (int x = 0; x < tilesetRows; x++)
                {
                    int index = y * tilesetColumns + x;
                    _tiles[index] = new Rectangle(
                        x * _tileWidth,
                        y * _tileHeight,
                        _tileWidth,
                        _tileHeight
                    );
                }
            }

            // Third line is the map size
            var thirdLine = lines[2].Split(',');
            _mapWidth = int.Parse(thirdLine[0]);
            _mapHeight = int.Parse(thirdLine[1]);

            // Now we can create our map
            var fourthLine = lines[3].Split(',');
            _map = new int[_mapWidth * _mapHeight]; //width*height = map size so int[1328] //28 trees, 3128=36*36
            for (int i = 0; i < _mapWidth * _mapHeight; i++)
            {
                _map[i] = int.Parse(fourthLine[i]);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    int index = _map[y * _mapWidth + x] - 1;
                    if (index == -1) continue;
                    spriteBatch.Draw(
                        _tilesetTexture,
                        new Vector2(
                            x * _tileWidth,
                            y * _tileHeight
                            ),
                        _tiles[index],
                        Color.White
                    );
                }
            }
        }
    }
}
