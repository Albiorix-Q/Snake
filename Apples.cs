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

        /// <summary>
        /// Constructor for the Apples; this is what the game calls 
        /// to initialize legal starting locations and spawn rates of new apples.
        /// 
        /// For this practicum exercise, you don't need to edit this function.
        /// </summary>
        public Apples(Texture2D squareTexture, Grid grid) 
        {
            this.grid = grid;
            this.squareTexture = squareTexture;
            apples = new List<Point>();
            spawnRate = 10;
            lastSpawn = 0;
        }

        /// <summary>
        /// Just like in Snake.cs, the Update function is used to handle logic.
        /// </summary>
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

        /// <summary>
        /// Just like in Snake.cs, the Draw function is used to handle the visuals.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < apples.Count; i++)
            {
                spriteBatch.Draw(squareTexture, grid.GetSpriteBounds(apples[i]), Color.Red);
            }
        }

        /// <summary>
        /// If the given point p holds an apple, 
        /// eat the apple (i.e., remove it from the list of apples) and return true.
        /// Else, return false.
        /// 
        /// You will need to call this function in Snake.cs for exercise 6.
        /// </summary>
        public bool EatApples(Point p)
        {
            bool eats = apples.Contains(p);
            apples.Remove(p);
            return eats;
        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // EXERCISE 5: Apple spawning
        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        void SpawnApple()
        {
            // TODO: Spawn apple
            // Calculate the number of unoccupied cells and choose a random index

        }
    }
}
