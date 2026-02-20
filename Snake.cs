using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    internal class Snake
    {
        Point direction;
        List<Point> positions;
        Texture2D squareTexture;
        Grid grid;
        Apples apples;
        double stepTimer;
        bool died;
        Keys leftKey, upKey, downKey, rightKey;

        public Snake(Point startingPos, Texture2D squareTexture, Grid grid, Apples apples, Keys upKey, Keys leftKey, Keys downKey, Keys rightKey) 
        {
            positions = new List<Point>();
            positions.Add(startingPos);
            this.squareTexture = squareTexture;
            this.grid = grid;
            this.apples = apples;
            stepTimer = 0;
            GetStartingDirection(startingPos);
            positions.Add(startingPos - direction);
            this.downKey = downKey;
            this.upKey = upKey;
            this.leftKey = leftKey;
            this.rightKey = rightKey;
        }

        public void Update(GameTime gameTime, double stepDuration)
        {
            stepTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            HandleInput();

            // If Timer is not over, do nothing
            // Else, do update logic
            if (stepTimer > 0)
                return;

            // Save tail of the snake, in case we want to grow this step
            Point lastPos = positions.Last();

            // Calculate next position of the head of the snake
            Point nextPos = positions.First() + direction;


            // Check for game end: head is going to move into body or wall
            // TODO: Check if the snake collides with itself
            
            // END


            // TODO: Move each part of the snake one step
            
            // END


            //TODO: If we eat an apple this step, add a position at the saved tail
            
            //END

            // Reset the timer
            stepTimer = stepDuration;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                spriteBatch.Draw(squareTexture, grid.GetSpriteBounds(positions[i]), Color.LimeGreen);
            }
        }

        void GetStartingDirection(Point position)
        {
            direction = new Point(1, 0);
            //TODO: Starting direction should be the direction with the most space
            
            //END
        }

        void HandleInput()
        {
            //TODO: Change direction based on keyboard input
            
            //END
        }

        public bool OccupiesCell(Point p)
        {
            return positions.Contains(p);
        }

        public int Length
        {
            get => positions.Count;
        }

        public bool Died
        {
            get => died;
        }
    }
}
