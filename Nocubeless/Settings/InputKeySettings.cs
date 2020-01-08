using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class InputKeySettings
    {
        public Keys MoveForward { get; set; }
        public Keys MoveBackward { get; set; }
        public Keys MoveLeft { get; set; }
        public Keys MoveRight { get; set; }
        public Keys MoveUpward { get; set; }
        public Keys MoveDown { get; set; }
        public Keys Run { get; set; }
        public Keys SwitchLayBreak { get; set; }
        public Keys ShowColorPicker { get; set; }
        public Keys Zoom { get; set; }
        public Keys SwitchMode { get; set; }

        public static InputKeySettings DefaultUSEnglish {
            get {
                return new InputKeySettings
                {
                    MoveForward = Keys.W,
                    MoveLeft = Keys.A,
                    MoveBackward = Keys.S,
                    MoveRight = Keys.D,
                    MoveUpward = Keys.Space,
                    MoveDown = Keys.LeftShift,
                    Run = Keys.Q,
                    SwitchLayBreak = Keys.X,
                    Zoom = Keys.V,
                    ShowColorPicker = Keys.C,
                    SwitchMode = Keys.E
                };
            }
        }

        public static InputKeySettings DefaultFrench
        {
            get {
                return new InputKeySettings
                {
                    MoveForward = Keys.Z,
                    MoveLeft = Keys.Q,
                    MoveBackward = Keys.S,
                    MoveRight = Keys.D,
                    MoveUpward = Keys.Space,
                    MoveDown = Keys.LeftShift,
                    Run = Keys.A,
                    Zoom = Keys.V,
                    SwitchLayBreak = Keys.X,
                    ShowColorPicker = Keys.C,
                    SwitchMode = Keys.E
                };
            }
        }
    }
}
