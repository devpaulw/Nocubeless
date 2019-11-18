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

        GameInputKeys keys;
        

        public GameApp()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;

            keys = new GameInputKeys
            {
                MoveForward = Keys.Z,
                MoveLeft = Keys.Q,
                MoveBackward = Keys.S,
                MoveRight = Keys.D,
                MoveUpward = Keys.Space,
                MoveDown = Keys.LeftShift,
                Run = Keys.A
            };

            Content.RootDirectory = "MGContent";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Camera camera = new Camera { AspectRatio = GraphicsDevice.Viewport.AspectRatio }; // Configure camera properly (singleton or other technique)
            CameraInputComponent cameraInputComponent = new CameraInputComponent(this, keys, camera, 0.25f, 0.1f, 2.5f);
            CubeEffect cubeEffect = new CubeEffect(Content.Load<Effect>("CubeEffect")); // TODO: Load with Loaded Assets! Be able to separate LoadContent and Initialize and the construction, and do it.
            World world = new World(this, camera, cubeEffect, 0.1f); // TO-DISPOSE

            world.LayCube(new Cube(
                    new Color(0f, 1f, 0f, 1f),
                    new Coordinate(-1, -1, -1)));
            world.LayCube(new Cube(
                new Color(1f, 0.2f, 0f, 1f),
                new Coordinate(0, 0, 0)));
            world.LayCube(new Cube(
                new Color(0f, 0.2f, 1f, 1f),
                new Coordinate(2, 0, 0)));
            world.LayCube(new Cube(
                new Color(0f, 1f, 1f, 1f),
                new Coordinate(-3, 2, 1)));
            world.LayCube(new Cube(
                new Color(1f, 1f, 0f, 1f),
                new Coordinate(2, 3, 4)));

            Components.Add(cameraInputComponent);
            Components.Add(world);

            IsMouseVisible = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // entreprendre   
        }

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive) // Don't take in care when window is not focused
                return;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32, 2, 128));
            
            base.Draw(gameTime);
        }
    }
}
