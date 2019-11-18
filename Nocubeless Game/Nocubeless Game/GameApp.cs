using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Nocubeless
{
    public class GameApp : Game
    {
        GraphicsDeviceManager graphics;

        GameInputKeys keysInputs;
        Camera camera;
        CubeRenderer cubeRenderer;

        public GameApp()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;

            keysInputs.MoveForward = Keys.Z;
            keysInputs.MoveLeft = Keys.Q;
            keysInputs.MoveBackward = Keys.S;
            keysInputs.MoveRight = Keys.D;
            keysInputs.MoveUpward = Keys.Space;
            keysInputs.MoveDown = Keys.LeftShift;
            keysInputs.Run = Keys.A;

            camera = new Camera();

            Content.RootDirectory = "MGContent";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            CubeEffect cubeEffect = new CubeEffect(Content.Load<Effect>("CubeEffect"));
            cubeRenderer = new CubeRenderer(GraphicsDevice, cubeEffect, 0.1F);
        }

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive) // Don't take in care when window is not focused
                return;

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            int windowMiddleX = GraphicsDevice.Viewport.Width / 2;
            int windowMiddleY = GraphicsDevice.Viewport.Height / 2;

            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();

            Console.WriteLine(mouse.X);
            Console.WriteLine(windowMiddleX);

            if (mouse.X != windowMiddleX || mouse.Y != windowMiddleY)
                Mouse.SetPosition(windowMiddleX, windowMiddleY);

            camera.MoveFromKeyboard(keyboard, gameTime.ElapsedGameTime.TotalSeconds);
            camera.RotateFromMouse(mouse);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32, 2, 128));

            cubeRenderer.Draw(camera, Color.Gold);
            
            base.Draw(gameTime);
        }
    }
}
