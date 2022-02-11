using Microsoft.Xna.Framework;

namespace SurviveTheWoods.Collisions
{
    /// <summary>
    /// A bounding rectangle for collision detection
    /// </summary>
    public struct BoundingRectangle
    {
        public float X;

        public float Y;

        public float Width;

        public float Height;

        public float Left => X;

        public float Right => X + Width;

        public float Top => Y;

        public float Bottom => Y + Height;

         

        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /* public bool CollidesWith(Vector2 mousePosition)
         {
             return CollisionHelper.Collides(this, mousePosition);
         }*/
    }
}
