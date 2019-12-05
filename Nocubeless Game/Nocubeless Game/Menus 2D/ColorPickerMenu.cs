﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class ColorPickerMenu : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D menuBaseTexture;
        private Rectangle menuBaseDestination;
        private bool show;

        public bool Show { // color picking from gamestate instead, deprecate that
            get {
                return show;
            }
            set {
                show = value;
            }
        }

        public GameState ActualGameState { get; set; }

        public ColorPickerMenu(IGameApp gameApp) : base(gameApp.Instance)
        {
            ActualGameState = gameApp.ActualState;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menuBaseTexture = Game.Content.Load<Texture2D>(@"Menus/Color Picker/Base");
            menuBaseDestination = new Rectangle(Game.GraphicsDevice.Viewport.Width / 2 - menuBaseTexture.Width / 2, 
                Game.GraphicsDevice.Viewport.Height / 2 - menuBaseTexture.Height / 2, 
                menuBaseTexture.Width, menuBaseTexture.Height);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if (show)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, 
                    GraphicsDevice.BlendState,
                    null,
                    GraphicsDevice.DepthStencilState,
                    GraphicsDevice.RasterizerState);

                spriteBatch.Draw(menuBaseTexture, menuBaseDestination, Color.Black);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
