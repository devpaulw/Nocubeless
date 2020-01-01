﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.ViewportAdapters;

namespace Nocubeless
{
    class CoordinatesDisplayer : NocubelessDrawableComponent
    {
        //private GuiSystem guiSystem;
        private SpriteFont font;
        private Vector2 drawPosition;

        public Coordinates PlayerCoordinates { get; private set; } // DOLATER: This fonction, should be in a specific Player kind class

        public CoordinatesDisplayer(Nocubeless nocubeless) : base(nocubeless) { }

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
            PlayerCoordinates = Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Camera.Position);

            int margin = 2;
            drawPosition = new Vector2(margin, margin);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Nocubeless.SpriteBatch.DrawString(font, 
                "Coordinates:\n" + PlayerCoordinates.ToString(), 
                drawPosition, Color.Black);

            base.Draw(gameTime);
        }
    }
}
