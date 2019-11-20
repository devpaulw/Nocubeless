using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class GameInputKeys
    {
        public Keys MoveForward { get; set; }
        public Keys MoveBackward { get; set; }
        public Keys MoveLeft { get; set; }
        public Keys MoveRight { get; set; }
        public Keys MoveUpward { get; set; }
        public Keys MoveDown { get; set; }
        public Keys Run { get; set; }

        public static GameInputKeys DefaultUSEnglish {
            get {
                return new GameInputKeys
                {
                    MoveForward = Keys.W,
                    MoveLeft = Keys.A,
                    MoveBackward = Keys.S,
                    MoveRight = Keys.D,
                    MoveUpward = Keys.Space,
                    MoveDown = Keys.LeftShift,
                    Run = Keys.Q
                };
            }
        }

        public static GameInputKeys DefaultFrench
        {
            get {
                return new GameInputKeys
                {
                    MoveForward = Keys.Z,
                    MoveLeft = Keys.Q,
                    MoveBackward = Keys.S,
                    MoveRight = Keys.D,
                    MoveUpward = Keys.Space,
                    MoveDown = Keys.LeftShift,
                    Run = Keys.A
                };
            }
        }
    }
}
