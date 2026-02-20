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

            // END
        }
    }
}
