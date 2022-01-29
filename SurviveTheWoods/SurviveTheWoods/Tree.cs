﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SurviveTheWoods
{
    public class Tree
    {
        private Texture2D texture;

        private Vector2 position;

        /// <summary>
        /// Sets tree position
        /// </summary>
        /// <param name="position">the position of the tree</param>
        public Tree(Vector2 position)
        {
            this.position = position;
            //this.bounds = new BoundingRectangle()
        }

        /// <summary>
        /// Loads the tree sprite
        /// </summary>
        /// <param name="content">the tree sprite</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("[Base]BaseChip_pipo16");
        }

        /// <summary>
        /// Draws the tree sprite
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(32, 16, 32, 32), Color.White);
        }
    }
}