using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Snake
{
    public class SnakeGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Grid grid;
        Snake snake;
        Apples apples;
        Texture2D squareTexture;
        SpriteFont menuFont;
        public static Random random;
        double stepDuration;
        GameState gameState;
        KeyboardState previousKeyboardState;

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            grid = new Grid(10, 4, GraphicsDevice.Viewport, squareTexture);

            previousKeyboardState = Keyboard.GetState();
            random = new Random();

            //TODO: Generate a random starting position for the snake, not on the edge
            Point startingPos = new Point(random.Next(1, grid.Width-1), random.Next(1, grid.Height - 1));
            //END

            apples = new Apples(squareTexture, grid);
            snake = new Snake(startingPos, squareTexture, grid, apples, Keys.W, Keys.A, Keys.S, Keys.D);
            apples.snake = snake;
            stepDuration = 0.2;
            gameState = GameState.Menu;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            squareTexture = new Texture2D(GraphicsDevice, 1, 1);
            squareTexture.SetData(new Color[] { Color.White });
            menuFont = Content.Load<SpriteFont>("MenuFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // Enable fullscreen
            if (IsKeyPressed(Keys.F))
            {
                if (graphics.PreferredBackBufferHeight == 1080)
                {
                    graphics.PreferredBackBufferWidth = 800;
                    graphics.PreferredBackBufferHeight = 480;
                }
                else
                {
                    graphics.PreferredBackBufferWidth = 1920;
                    graphics.PreferredBackBufferHeight = 1080;
                }
                graphics.ApplyChanges();
                grid.CalculateDimensions(GraphicsDevice.Viewport);
            }

            // Choose update logic depending on the current game state
            switch (gameState)
            {
                case GameState.Menu:
                    if (IsKeyPressed(Keys.Space))
                        gameState = GameState.Playing;
                    break;

                case GameState.Playing:
                    if (IsKeyPressed(Keys.P))
                        gameState = GameState.Paused;

                    snake.Update(gameTime, stepDuration);
                    apples.Update(gameTime, stepDuration);
                    
                    // Check for the win and lose conditions
                    if (snake.Length >= grid.Width * grid.Height || snake.Died)
                        gameState = GameState.Finished;
                    break;

                case GameState.Paused:
                    if (IsKeyPressed(Keys.P))
                        gameState = GameState.Playing;
                    break;

                case GameState.Finished:
                    if (IsKeyPressed(Keys.Space))
                        Initialize();
                    break;
            }
            
            previousKeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            
            grid.Draw(spriteBatch);

            Rectangle r = new Rectangle();
            r.Width = 400;
            r.Height = 100;
            r.Location = new Point((GraphicsDevice.Viewport.Width - r.Width) / 2, (GraphicsDevice.Viewport.Height - r.Height) / 2);
            Vector2 textSize;

            switch (gameState)
            {
                case GameState.Menu:
                    spriteBatch.Draw(squareTexture, r, Color.Black);
                    textSize = menuFont.MeasureString("Press <SPACE> to start");
                    spriteBatch.DrawString(menuFont, "Press <SPACE> to start", r.Center.ToVector2() - textSize / 2, Color.White);
                    break;

                case GameState.Playing:
                    snake.Draw(spriteBatch);
                    apples.Draw(spriteBatch);
                    break;

                case GameState.Paused:
                    snake.Draw(spriteBatch);
                    apples.Draw(spriteBatch);
                    spriteBatch.Draw(squareTexture, r, Color.Black);
                    textSize = menuFont.MeasureString("Paused");
                    spriteBatch.DrawString(menuFont, "Paused", r.Center.ToVector2() - textSize / 2, Color.White);
                    break;

                case GameState.Finished:
                    snake.Draw(spriteBatch);
                    apples.Draw(spriteBatch);
                    spriteBatch.Draw(squareTexture, r, Color.Black);
                    string gameOverMessage = $"Game over: {snake.Length}/{grid.Width * grid.Height}";
                    textSize = menuFont.MeasureString(gameOverMessage);
                    spriteBatch.DrawString(menuFont, gameOverMessage, r.Center.ToVector2() - new Vector2(textSize.X / 2, textSize.Y), Color.White);
                    textSize = menuFont.MeasureString("Press <SPACE> to reset");
                    spriteBatch.DrawString(menuFont, "Press <SPACE> to reset", r.Center.ToVector2() - new Vector2(textSize.X / 2, 0), Color.White);
                    break;
            }

            base.Draw(gameTime);
            spriteBatch.End();
        }


        bool IsKeyPressed(Keys k)
        {
            return previousKeyboardState.IsKeyDown(k) && Keyboard.GetState().IsKeyUp(k);
        }
    }

    enum GameState
    {
        Menu,
        Playing,
        Paused,
        Finished
    }
}
