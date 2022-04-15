using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods
{
    /// <summary>
    /// A class for rendering a heart out of triangles
    /// </summary>
    public class HeartHealth
    {
        /// <summary>
        /// The effect to use rendering the heart
        /// </summary>
        BasicEffect effect;

        /// <summary>
        /// The game this heart belongs to 
        /// </summary>
        Game game;

        /// <summary>
        /// The vertices of the heart
        /// </summary>
        VertexPositionColor[] vertices;

        /// <summary>
        /// Initializes the vertices of the heart
        /// </summary>
        void InitializeVertices()
        {
            vertices = new VertexPositionColor[12];
            // vertex 0
            vertices[0].Position = new Vector3(0, 0, 0);
            vertices[0].Color = Color.Red;
            // vertex 1
            vertices[1].Position = new Vector3(-1.25f, 2, 0);
            vertices[1].Color = Color.Pink;
            // vertex 2
            vertices[2].Position = new Vector3(0, 2.3f, 0);
            vertices[2].Color = Color.DarkRed;

            // vertex 3
            vertices[3].Position = new Vector3(0, 0, 0);
            vertices[3].Color = Color.Red;
            // vertex 4
            vertices[4].Position = new Vector3(1.25f, 2, 0);
            vertices[4].Color = Color.IndianRed;
            // vertex 5
            vertices[5].Position = new Vector3(0, 2.3f, 0);
            vertices[5].Color = Color.DarkRed;

            // vertex 6
            vertices[6].Position = new Vector3(-1.25f, 2, 0);
            vertices[6].Color = Color.Pink;
            // vertex 7
            vertices[7].Position = new Vector3(-0.6f, 3, 0);
            vertices[7].Color = Color.OrangeRed;
            // vertex 8
            vertices[8].Position = new Vector3(0, 2.3f, 0);
            vertices[8].Color = Color.DarkRed;

            // vertex 9
            vertices[9].Position = new Vector3(0.6f, 3, 0);
            vertices[9].Color = Color.Red;
            // vertex 10
            vertices[10].Position = new Vector3(1.25f, 2, 0);
            vertices[10].Color = Color.IndianRed;
            // vertex 11
            vertices[11].Position = new Vector3(0, 2.3f, 0);
            vertices[11].Color = Color.DarkRed;
        }

        /// <summary>
        /// Initializes the BasicEffect to render our heart
        /// </summary>
        void InitializeEffect(int whichHeart)
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.Identity;
            switch(whichHeart)
            {
                case 1:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 40), new Vector3(23, 19, 0), Vector3.Up);
                    break;
                case 2:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, -0.25f, 40), new Vector3(21, 19, -0.5f), Vector3.Up);
                    break;
                case 3:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, -0.5f, 40), new Vector3(19, 19, -1), Vector3.Up);
                    break;
                case 4:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, -0.75f, 40), new Vector3(17, 19, -1.5f), Vector3.Up);
                    break;
                case 5:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, -1, 40), new Vector3(15, 19, -2), Vector3.Up);
                    break;
                case 6:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, -1.25f, 40), new Vector3(13, 19, -2.5f), Vector3.Up);
                    break;
                case 7:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, -1.5f, 40), new Vector3(11, 19, -3), Vector3.Up);
                    break;
                case 8:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, -1.75f, 40), new Vector3(9, 19, -3.5f), Vector3.Up);
                    break;
                case 9:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, -2, 40), new Vector3(7, 19, -4), Vector3.Up);
                    break;
                case 10:
                    effect.View = Matrix.CreateLookAt(new Vector3(0, -2.25f, 40), new Vector3(5, 19, -4.5f), Vector3.Up);
                    break;
            }
            
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,                         // The field-of-view 
                game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
                0.1f, // The near plane distance 
                100.0f // The far plane distance
            );
            effect.VertexColorEnabled = true;
        }

        /// <summary>
        /// Constructs a triangle instance
        /// </summary>
        /// <param name="game">The game that is creating the triangle</param>
        public HeartHealth(Game game, int whichHeart)
        {
            this.game = game;
            InitializeVertices();
            InitializeEffect(whichHeart);
        }

        /// <summary>
        /// Draws the triangle
        /// </summary>
        public void Draw()
        {
            // Cache old rasterizer state
            RasterizerState oldState = game.GraphicsDevice.RasterizerState;

            // Disable backface culling
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            game.GraphicsDevice.RasterizerState = rasterizerState;

            // Apply our effect
            effect.CurrentTechnique.Passes[0].Apply();

            // Draw the triangle
            game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                vertices,       // The vertex data 
                0,              // The first vertex to use
                4               // The number of triangles to draw
            );

            // Restore the prior rasterizer state 
            game.GraphicsDevice.RasterizerState = oldState;
        }

        /// <summary>
        /// Rotates the triangle around the y-axis
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            float angle = (float)gameTime.TotalGameTime.TotalSeconds;
            effect.World = Matrix.CreateRotationY(angle);
        }
    }
}
