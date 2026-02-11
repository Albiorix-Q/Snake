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
            if (OccupiesCell(nextPos) || grid.OutOfBound(nextPos))
            {
                died = true;
                return;
            }
            // END

            // TODO: Move each part of the snake one step
            for (int i = positions.Count - 1; i > 0; i--)
            {
                positions[i] = positions[i - 1];
            }
            positions[0] = nextPos;
            // END

            //TODO: If we eat an apple this step, add a position at the saved tail
            if (apples.EatApples(positions[0]))
                positions.Add(lastPos);
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
            int right = grid.Width - position.X - 1;
            int left = position.X;
            int up = grid.Height - position.Y - 1;
            int down = position.Y;
            int maxDistance = Math.Max(Math.Max(Math.Max(right, left), up), down);
            if (right == maxDistance)
                direction = new Point(1, 0);
            else if (left == maxDistance)
                direction = new Point(-1, 0);
            else if (up == maxDistance)
                direction = new Point(0, -1);
            else if (down == maxDistance)
                direction = new Point(0, 1);
            //END
        }

        void HandleInput()
        {
            //TODO: Change direction based on keyboard input
            Point oldDirection = direction;

            if (Keyboard.GetState().IsKeyDown(upKey))
                direction = new Point(0, -1);

            if (Keyboard.GetState().IsKeyDown(leftKey))
                direction = new Point(-1, 0);

            if (Keyboard.GetState().IsKeyDown(downKey))
                direction = new Point(0, 1);

            if (Keyboard.GetState().IsKeyDown(rightKey))
                direction = new Point(1, 0);

            // Reverse updating the direction if it is towards the tail
            if (positions[0] + direction == positions[1])
                direction = oldDirection;
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
