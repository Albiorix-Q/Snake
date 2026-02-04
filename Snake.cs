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
        Keys[] controls;

        public Snake(Point startingPos, Texture2D squareTexture, Grid grid, Apples apples, Keys[] controls) 
        {
            positions = new List<Point>();
            positions.Add(startingPos);
            this.squareTexture = squareTexture;
            this.grid = grid;
            this.apples = apples;
            stepTimer = 0;
            GetStartingDirection(startingPos);
            positions.Add(startingPos - direction);
            this.controls = controls;
        }

        public void Update(GameTime gameTime, double stepDuration)
        {
            stepTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            HandleInput();

            // If Timer is not over, do nothing
            // Else, do update logic
            if (stepTimer > 0)
                return;

            // Check for game end: head is going to move into body or wall
            if (positions.Skip(1).Contains(positions[0] + direction) || grid.OutOfBound(positions[0] + direction))
            {
                died = true;
                return;
            }

            // Save tail of the snake, in case we want to grow this step
            Point lastPos = positions.Last();

            // For each cell of the snake, move its position one
            // Move the head of the snake by the direction
            for (int i = positions.Count - 1; i > 0; i--)
            {
                positions[i] = positions[i - 1];
            }
            positions[0] += direction;

            // If we eat an apple this step, add a position at the saved tail
            if (apples.EatApples(positions[0]))
                positions.Add(lastPos);

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
            // Starting direction should be the direction with the most space towards the center of the grid
            int xDif = position.X - (grid.Width / 2);
            int yDif = position.Y - (grid.Height / 2);
            if (xDif == 0)
                direction = new Point(-1, 0);
            else if (Math.Abs(xDif) > Math.Abs(yDif))
                direction = new Point(Math.Sign(-xDif) * 1, 0);
            else
                direction = new Point(0, Math.Sign(-yDif) * 1);
        }

        void HandleInput()
        {
            Point oldDirection = direction;

            if (Keyboard.GetState().IsKeyDown(controls[0]))
                direction = new Point(0, -1);

            if (Keyboard.GetState().IsKeyDown(controls[1]))
                direction = new Point(-1, 0);

            if (Keyboard.GetState().IsKeyDown(controls[2]))
                direction = new Point(0, 1);

            if (Keyboard.GetState().IsKeyDown(controls[3]))
                direction = new Point(1, 0);

            // Reverse updating the direction if it is towards the tail
            if (positions[0] + direction == positions[1])
                direction = oldDirection;
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
