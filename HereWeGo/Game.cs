using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace HereWeGo
{
    class Game : GameWindow
    {
        private KeyboardState keyboardInput;
        private MouseState mouseInput;

        private readonly Camera playerCamera;
        private readonly Scene scene;

        public Game(int width, int height, string title, bool fullScreen)
            : base(width, height, GraphicsMode.Default, title)
        {
            if (fullScreen) WindowState = WindowState.Fullscreen;

            playerCamera = new Camera(Vector3.Zero, (width / (float)height));

            #region World Init and Lay Test Cubes
            scene = new Scene(ref playerCamera, 0.025f);
            { // Cubes Installing
                scene.LayCube(new Cube(
                    new Color4(0f, 1f, 0f, 1f),
                    new Coordinate(-1, -1, -1)));
                scene.LayCube(new Cube(
                    new Color4(1f, 0.2f, 0f, 1f),
                    new Coordinate(0, 0, 0)));
                scene.LayCube(new Cube(
                    new Color4(0f, 0.2f, 1f, 1f),
                    new Coordinate(2, 0, 0)));
                scene.LayCube(new Cube(
                    new Color4(0f, 1f, 1f, 1f),
                    new Coordinate(-3, 2, 1)));
                scene.LayCube(new Cube(
                    new Color4(1f, 1f, 0f, 1f),
                    new Coordinate(2, 3, 4)));
            }
            #endregion
        }

        protected override void OnLoad(EventArgs e)
        {
            #region Console Introduction
#pragma warning disable CA1303 // Do not pass literals as localized parameters BYPASS
            Console.WriteLine("Nocubeless, Version Before-Release 1.0 (INDEV).");

            GL.GetInteger(GetPName.MajorVersion, out int oglMajorVersion);
            GL.GetInteger(GetPName.MinorVersion, out int oglMinorVersion);
            Console.WriteLine("Powered with OpenGL Core " + oglMajorVersion + "." + oglMinorVersion + ".");

            Console.WriteLine("Game started successfully.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters BYPASS
            #endregion

            #region Window init
            CursorVisible = false;
            #endregion

            #region OpenGL Settings
            GL.Enable(EnableCap.DepthTest);
            #endregion

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            scene.Draw();

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused) return;

            keyboardInput = Keyboard.GetState();
            mouseInput = Mouse.GetState();

            #region Close with Escape
            if (keyboardInput.IsKeyDown(Key.Escape))
                Exit();
            #endregion

            playerCamera.MoveFromKeyboard(keyboardInput, e.Time);
            playerCamera.RotateFromMouse(mouseInput);

            //player.RotateView(Inputs.GetViewAxis(e.Time));

            base.OnUpdateFrame(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (Focused) Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);

            base.OnMouseMove(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            scene.Dispose();

            base.OnUnload(e);
        }
    }
}

// TO-DO :
// FIX DESIGN: Make a Model and Cube model children, deprecate handy cube
// REVIEW SHADERS

// Tuesday challenge
// /\
// ||

// Lay Cube Pointed Highlighter
// Sounds effects + Musics // SUNDAY TARGET OBJECTIVE : GL, GOOD COURAGE, GOOD DEV, GLDF CSHARP EDITION.
// Collision and gravity
// Saves HEX
// Inventory and Main Menu