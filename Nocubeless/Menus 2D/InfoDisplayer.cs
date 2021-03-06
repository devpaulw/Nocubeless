﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
//using MonoGame.Extended.Gui;
//using MonoGame.Extended.Gui.Controls;
//using MonoGame.Extended.ViewportAdapters;

namespace Nocubeless
{
    class InfoDisplayer : NocubelessDrawableComponent
    {
        //private GuiSystem guiSystem;
        private SpriteFont font;
        private Vector2 coordinatesDrawPosition;
        private Vector2 fpsDrawPosition;
        private string state;

        private readonly FramesPerSecondCounter fpsCounter = new FramesPerSecondCounter();

        public CubeCoordinates PlayerCoordinates { get; private set; } // DOLATER: This fonction, should be in a specific Player kind class
        public CubeCoordinates ChunkCoordinates { get; private set; }

        public InfoDisplayer(Nocubeless nocubeless) : base(nocubeless) { }

        protected override void LoadContent()
        {
            //var viewportAdapter = new DefaultViewportAdapter(GraphicsDevice);
            //var guiRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, () => Matrix.Identity);
            font = Game.Content.Load<SpriteFont>(@"Menus/Color Picker/InputTextBoxFont"); // Is not the right font!

            //var test = new StackPanel()
            //{
            //    Orientation = Orientation.Vertical,
            //    Items =
            //    {
            //        new Label("Hello les gens")
            //    }
            //};

            //guiSystem = new GuiSystem(viewportAdapter, guiRenderer);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            fpsCounter.Update(gameTime);
            state = GetLitteralState();

            PlayerCoordinates = Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Camera.ScreenPosition);
            ChunkCoordinates = CubeChunkHelper.FindBaseCoordinates(PlayerCoordinates);

            int margin = 2;
            coordinatesDrawPosition = new Vector2(margin, margin);
            fpsDrawPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, margin);

            base.Update(gameTime);

            string GetLitteralState()
            {
                switch (Nocubeless.CurrentState)
                {
                    case NocubelessState.Playing:
                        return "Playing";
                    case NocubelessState.Editing:
                        return "Editing world";
                    case NocubelessState.ColorPicking:
                        return "Picking a color...";
                    default:
                        return "Unknown";
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            fpsCounter.Draw(gameTime);

            Nocubeless.SpriteBatch.DrawString(font, 
                "Player coordinates:\n" + PlayerCoordinates.ToString() + 
                "\nIn chunk: " + ChunkCoordinates.ToString() +
                "\nPreviewable cube coordinates:\n" + ((Nocubeless.CubeWorld.PreviewableCube is null) ? "{None}" : Nocubeless.CubeWorld.PreviewableCube.Coordinates.ToString()) +
                "\n\nCurrent state: " + state,
                coordinatesDrawPosition, Color.Black);

            Nocubeless.SpriteBatch.DrawString(font,
                "FPS: " + fpsCounter.FramesPerSecond.ToString(CultureInfo.CurrentCulture),
                fpsDrawPosition, Color.Black);

            base.Draw(gameTime);
        }
    }
}
