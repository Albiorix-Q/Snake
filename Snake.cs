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

        /// <summary>
        /// Constructor for the snake; this is what the game calls when it wants to create a new snake.
        /// Basically; it initializes the snake's position, direction, and other necessary variables.
        /// 
        /// For this practicum exercise, you don't need to edit this function.
        /// (Unless you want to add multiplayer.)
        /// </summary>
        public Snake(Texture2D squareTexture, Grid grid, Apples apples, Keys upKey, Keys leftKey, Keys downKey, Keys rightKey) 
        {
            this.grid = grid;
            this.apples = apples;
            this.squareTexture = squareTexture;
            this.upKey = upKey;
            this.leftKey = leftKey;
            this.rightKey = rightKey;
            this.downKey = downKey;
            
            positions = new List<Point>();
            Point startingPos = GetStartingPosition();
            positions.Add(startingPos);
            direction = GetStartingDirection(startingPos);
            positions.Add(startingPos - direction);

            stepTimer = 0;
            died = false;
        }

        /// <summary>
        /// In games and simulations, the Update function is typically called once per frame to update the state of the game.
        /// So, this function is used for handling things that need to happen over time, 
        /// such as moving the snake, checking for collisions, and handling user input.
        /// 
        /// In the case of this snake game, we don't want the snake to move every single frame, because that would be too fast. 
        /// Instead, we use a timer (stepTimer) to control how often Update is actually executing its logic.
        /// 
        /// For this practicum exercise, you don't need to edit this function.
        /// However, you will need to edit the functions that this function calls, such as HandleMovement and HandleInput.
        /// </summary>
        public void Update(GameTime gameTime, double stepDuration)
        {
            // We always handle input regardless of our custom timer,
            // because otherwise the player would need to time their key presses perfectly to change direction,
            // which would be very frustrating.
            HandleInput();

            // Decrease timer by the elapsed real time since last frame
            stepTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            // If timer is not over, do nothing
            // Else, do the update logic
            // the 'return' keyword ensures the function stops there.
            if (stepTimer > 0)
                return;

            

            // Calculate next position of the head of the snake
            // We do this here since we need it for both HandleDeathCheck and HandleMovement.
            Point nextPos = positions[0] + direction;

            // Check for game end (i.e., head is going to move into body or wall)
            HandleCollisionCheck(nextPos);

            // Save tail of the snake, in case we want to grow this step
            // We use this point in <Exercise 6>, growing the snake.
            // We need to store this before we move the snake
            // so we remember where to grow if we eat an apple.
            Point lastPos = positions.Last();

            //Move each part of the snake one step
            HandleMovement(nextPos);

            // If we eat an apple this step, add a position at the saved tail position
            HandleAppleEating(lastPos);

            // Reset the timer for the next step
            stepTimer = stepDuration;
        }

        /// <summary>
        /// Similar to Update, the Draw function is called once per frame to render the game on the screen.
        /// Essentially: Update handles game logic, while Draw handles visuals.
        /// 
        /// For this practicum exercise, you don't need to edit this function.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                spriteBatch.Draw(squareTexture, grid.GetSpriteBounds(positions[i]), Color.LimeGreen);
            }
        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // EXERCISE 2: Snake movement
        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        void HandleMovement(Point nextPos)
        {
            // TODO: Move each part of the snake one step
            // Hint: think about how the Snake should move:
            // does every body part follow the direction?

        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // EXERCISE 3: Controlling the snake
        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        void HandleInput()
        {
            //TODO: Change direction based on keyboard input
            
        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // EXERCISE 4: Snake death
        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        void HandleCollisionCheck(Point nextPos)
        {
            // TODO: if the snake collides with itself or a wall, then the snake dies.
            // (Hint: use the variables `nextPos` and `died`, and the function `OccupiesCell`)

        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // EXERCISE 6: Snake Growth
        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        void HandleAppleEating(Point lastPos)
        {
            // TODO: If the snake head collides with an apple,
            // add a position at the end of the snake
            // and remove the apple from the game. (Hint: look at the Apples.cs file)

        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // EXERCISE 7/BONUS: Random starting position
        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        Point GetStartingPosition()
        {
            // Currently, the snake always starts the game at the same position (2, 2).
            // TODO: make the snake start at a random legal position on the grid.
            return new Point(2, 2);
        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // EXERCISE 7/BONUS: Smarter starting direction
        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        Point GetStartingDirection(Point startingPos)
        {
            // Currently, the snake always starts the game moving to the right.
            // This is fine if we have a static spawn location,
            // but if we have random spawn locations,
            // the snake might start by immediately colliding with a wall.

            //TODO: Starting direction should be the direction with the most space
            return new Point(1, 0);
        }

        
        // The functions below this point are used to share information with other
        // classes, like Apples.cs and SnakeGame.cs.
        // Since these classes need to know things about the snake,
        // but they shouldn't be able to change the snake's state,
        // we use these functions to give them read-only access to the snake's information.
        
        // You will need this function for exercise 4
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
