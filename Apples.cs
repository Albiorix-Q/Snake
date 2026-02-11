using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    internal class Apples
    {
        List<Point> apples;
        double stepTimer;
        int spawnRate;
        int lastSpawn;
        Texture2D squareTexture;
        Grid grid;
        public Snake snake;


        public Apples(Texture2D squareTexture, Grid grid) 
        {
            this.grid = grid;
            this.squareTexture = squareTexture;
            apples = new List<Point>();
            spawnRate = 10;
            lastSpawn = 0;
        }

        public void Update(GameTime gameTime, double stepDuration)
        {
            stepTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            // If Timer is not over, do nothing
            // Else, do update logic
            if (stepTimer > 0)
                return;

            // Reset the timer
            stepTimer = stepDuration;

            lastSpawn++;
            if (lastSpawn < spawnRate)
                return;

            SpawnApple();

            lastSpawn = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < apples.Count; i++)
            {
                spriteBatch.Draw(squareTexture, grid.GetSpriteBounds(apples[i]), Color.Red);
            }
        }

        public bool EatApples(Point p)
        {
            bool eats = apples.Contains(p);
            apples.Remove(p);
            return eats;
        }

        void SpawnApple()
        {
            // TODO: Spawn apple
            // Calculate the number of unoccupied cells and choose a random index
            int freeCells = grid.Height * grid.Width - apples.Count - snake.Length;
            int targetCell = SnakeGame.random.Next(freeCells);
            
            // Loop over all cells and increase i if the cell is unoccupied. Check if i equals the random index
            Point newPos = new Point(-1, -1);
            int i = 0;
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    Point curPoint = new Point(x, y);
                    if (apples.Contains(curPoint) || snake.OccupiesCell(curPoint))
                        continue;
                    if (i == targetCell)
                    {
                        newPos = curPoint;
                        break;
                    }
                    i++;
                }
                if (newPos != new Point(-1, -1))
                    break;
            }
            apples.Add(newPos);
            // END
        }
    }
}
