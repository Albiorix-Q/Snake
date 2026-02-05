using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Snake
{
    internal class Grid
    {
        int width, height;
        float cellSize;
        int spriteSize;
        Vector2 origin;
        Texture2D squareTexture;
        float margin = 20f;

        public Grid(int width, int height, Viewport v, Texture2D squareTexture)
        {
            this.width = width;
            this.height = height;
            this.squareTexture = squareTexture;
            CalculateDimensions(v);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y  < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    spriteBatch.Draw(squareTexture, GetSpriteBounds(new Point(x,y)), Color.White);
                }
            }
        }

        public Rectangle GetSpriteBounds(Point p)
        {
            Vector2 spriteOrigin = new Vector2(p.X * cellSize + 0.5f * (cellSize - spriteSize), p.Y * cellSize + 0.5f * (cellSize - spriteSize));
            return new Rectangle((origin + spriteOrigin).ToPoint(), new Point(spriteSize, spriteSize));
        }

        public bool OutOfBound(Point p)
        {
            return p.X < 0 || p.Y < 0 || p.X >= width || p.Y >= height;
        }

        public void CalculateDimensions(Viewport viewport)
        {
            cellSize = Math.Min((viewport.Width - margin) / width, (viewport.Height - margin) / height);
            spriteSize = (int)(cellSize * 0.9f);
            origin = new Vector2(viewport.Width / 2, viewport.Height / 2) - new Vector2(cellSize * width / 2, cellSize * height / 2);
        }

        public int Width
        { get => width; }
        public int Height
        { get => height; }
    }
}
