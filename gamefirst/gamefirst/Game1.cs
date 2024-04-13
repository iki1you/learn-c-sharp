using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net.Security;
using System.Reflection.Emit;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace gamefirst
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        Point charSize = new Point(40, 100);
        float charScale = 4;



        Vector2 position = Vector2.Zero;
        float speed = 5f;
        SpriteEffects charMainEffect = SpriteEffects.None;
        int currentTime = 0;
        int period = 500;


        int frameWidth = 50;
        int frameHeight = 38;
        Point spriteEnd = new Point(3, 0);
        Point currentFrame = new Point(0, 0);
        
        Vector2 playerSpeed = new Vector2(0, 0);
        

        State state;
        Texture2D grass;
        Point sizeGrass = new Point(200, 200);

        bool isMoving = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            state = new State();
            state.LoadLevel("level1.txt", sizeGrass);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("mainchar2");
            grass = Content.Load<Texture2D>("grass");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            var prefPos = position;
            

            currentTime += gameTime.ElapsedGameTime.Milliseconds;
            position += playerSpeed;
            playerSpeed = Vector2.Zero;
            if (currentTime > period)
            {
                currentTime -= period;
                currentFrame.X = (currentFrame.X + 1) % spriteEnd.X;
            }    
            
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                playerSpeed.X = -speed;
                charMainEffect = SpriteEffects.FlipHorizontally;
                AnimateRun();
            }
                
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                playerSpeed.X = speed;
                charMainEffect = SpriteEffects.None;
                AnimateRun();
            }    

            if (keyboardState.IsKeyDown(Keys.Up))
                playerSpeed.Y = -speed;
            if (keyboardState.IsKeyDown(Keys.Down))
                playerSpeed.Y = speed;

            
            if (playerSpeed == Vector2.Zero)
            {
                AnimateStay();
            }

            foreach (var line in state.level)
                foreach (var tile in line)
                    if (tile.Name == TileName.Wall && 
                        IsCollide(tile.CollisionRect, new Rectangle((int)position.X, (int)position.Y, 
                        texture.Width / 10 * (int)charScale, texture.Height * (int)charScale  / 17)))
                        position = prefPos;
            base.Update(gameTime);
        }

        protected bool IsCollide(Rectangle tile, Rectangle otherTile)
        {
            if (tile.Intersects(otherTile))
                return true;
            return false;
        }

        protected void AnimateRun()
        {
            period = 200;
            spriteEnd.X = 5;
            currentFrame.Y = 1;
        }

        protected void AnimateStay()
        {
            currentFrame.Y = 0;
            currentFrame.X = (currentFrame.X) % spriteEnd.X;
            spriteEnd.X = 3;
            period = 500;
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            for (int i = 0; i < state.level.Count; i++)
            {
                for (int j = 0; j < state.level[0].Count; j++)
                {
                    if (state.level[i][j].Name == TileName.Wall)
                        spriteBatch.Draw(grass, 
                            new Rectangle(sizeGrass.X * i, sizeGrass.Y * j, sizeGrass.X, sizeGrass.Y),
                            new Rectangle(0, 0, grass.Width, grass.Height),
                            Color.White);
                }
            }

            

            spriteBatch.Draw(texture, position,
                new Rectangle(currentFrame.X * frameWidth,
                    currentFrame.Y * frameHeight,
                    frameWidth, frameHeight),
                Color.White, 0, Vector2.Zero,
                charScale, charMainEffect, 1);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}