
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nocubeless
{
    class TextBox : NocubelessDrawableComponent
    {
        private readonly Texture2D texture;
        private readonly Vector3 notFocusedAdditionalColor;

        public string Text { get; set; }

        public bool IsFocused { get; private set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Position { get; set; }
        public Color BackColor { get; set; }
        public Color BorderColor { get; set; }
        public int BorderSize { get; set; }
        public Color TextColor { get; set; }
        public SpriteFont TextFont { get; set; }
        public int MaxLength { get; set; }
        public bool UnlimitedLength { get; set; }

        public TextBox(Nocubeless nocubeless, 
            int width, int height, 
            Vector2 position, 
            Color backColor, Color borderColor,
            int borderSize,
            Color textColor, SpriteFont textFont,
            bool unlimitedLength, int maxLength = 0)
            : base(nocubeless)
        {
            Width = width;
            Height = height;
            Position = position;
            BackColor = backColor;
            BorderColor = borderColor;
            BorderSize = borderSize;
            TextColor = textColor;
            TextFont = textFont;
            UnlimitedLength = unlimitedLength;
            MaxLength = maxLength;

            Text = string.Empty;

            IsFocused = false;

            notFocusedAdditionalColor = new Vector3(0.8f);

            #region Texture Set-Up
            texture = new Texture2D(GraphicsDevice, Width, Height);
            var colorData = ConstructColorData();
            texture.SetData(colorData);
            #endregion
        }

        public override void Update(GameTime gameTime)
        {
            // test if the TextBox is pointed
            if (Input.CurrentMouseState.LeftButton == ButtonState.Pressed
                    && Input.OldMouseState.LeftButton == ButtonState.Released)
            {
                if (IsPointed(Input.CurrentMouseState.Position.ToVector2()))
                {
                    IsFocused = true;
                    texture.SetData(ConstructColorData()); // reconstruct colors
                    TextInput.Text = Text;
                }
                else
                {
                    IsFocused = false;
                    texture.SetData(ConstructColorData());
                }
            }

            if (IsFocused)
            {
                TextInput.Read();
                if (!UnlimitedLength && TextInput.Text.Length > MaxLength) // when too much characters have been typed
                    TextInput.Text = TextInput.Text.Remove(TextInput.Text.Length - 1);
                Text = TextInput.Text;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Nocubeless.SpriteBatch.Draw(texture, Position, Color.White);

            var adaptedPosition = AdaptPosition(Position);
            Nocubeless.SpriteBatch.DrawString(TextFont, Text, adaptedPosition, TextColor);

            base.Draw(gameTime);
        }

        private bool IsPointed(Vector2 pointerPosition)
        {
            if (pointerPosition.X > Position.X && pointerPosition.X < Position.X + Width
                && pointerPosition.Y > Position.Y && pointerPosition.Y < Position.Y + Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Vector2 AdaptPosition(Vector2 position)
        {
            var adaptedPosition = new Vector2(position.X + BorderSize, position.Y);
            return adaptedPosition;
        }

        private Color[] ConstructColorData()
        {
            var constructedColorData = new Color[texture.Width * texture.Height];

            ConstructBack(ref constructedColorData);
            ConstructBorders(ref constructedColorData);

            if (!IsFocused)
            {
                for (int i = 0; i < constructedColorData.Length; i++)
                {
                    constructedColorData[i] = new Color(constructedColorData[i].ToVector3() * notFocusedAdditionalColor);
                }
            } // an overlay when the textbox is focused.

            return constructedColorData;
        }
        private void ConstructBack(ref Color[] colorData)
        {
            for (int i = 0; i < colorData.Length; i++)
                colorData[i] = BackColor;
        }
        private void ConstructBorders(ref Color[] colorData)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if ((x >= 0 && x < BorderSize) // right
                        || (y >= 0 && y < BorderSize)
                        || (x >= Width - BorderSize && x <= Width)
                        || (y >= Height - BorderSize && y <= Height))
                    {
                        colorData[x + (Width * y)] = BorderColor;
                    }
                }
            }
        }
    }
}