using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    static class TextInput
    {
        public static string Text { get; set; } = string.Empty;

        public static void Read()
        {
            foreach (Keys pressedKey in Input.CurrentKeyboardState.GetPressedKeys())
            {
                if (Input.OldKeyboardState.IsKeyUp(pressedKey))
                {
                    switch (pressedKey) // DESIGN: should be extern!
                    {
                        case Keys.Back:
                            if (Text.Length > 0) Text = Text.Remove(Text.Length - 1);
                            break;
                        case Keys.Space:
                            Text += ' ';
                            break;

                        case Keys.D0:
                        case Keys.NumPad0:
                            Text += '0';
                            break;
                        case Keys.D1:
                        case Keys.NumPad1:
                            Text += '1';
                            break;
                        case Keys.D2:
                        case Keys.NumPad2:
                            Text += '2';
                            break;
                        case Keys.D3:
                        case Keys.NumPad3:
                            Text += '3';
                            break;
                        case Keys.D4:
                        case Keys.NumPad4:
                            Text += '4';
                            break;
                        case Keys.D5:
                        case Keys.NumPad5:
                            Text += '5';
                            break;
                        case Keys.D6:
                        case Keys.NumPad6:
                            Text += '6';
                            break;
                        case Keys.D7:
                        case Keys.NumPad7:
                            Text += '7';
                            break;
                        case Keys.D8:
                        case Keys.NumPad8:
                            Text += '8';
                            break;
                        case Keys.D9:
                        case Keys.NumPad9:
                            Text += '9';
                            break;

                        default:
                            Text += pressedKey.ToString();
                            break;
                    }
                }
            }
        }
    }
}
