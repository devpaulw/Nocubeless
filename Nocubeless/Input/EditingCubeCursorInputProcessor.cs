using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class EditingCubeCursorInputProcessor : InputProcessor
    {
        public CubeCoordinates direction;

        public EditingCubeCursorInputProcessor(Nocubeless nocubeless) : base(nocubeless)
        {
            direction = new CubeCoordinates(0, 1, 0);
        }

        public override void Process()
        {
            if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveRight))
                direction = new CubeCoordinates(-1, 0, 0);
            else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveLeft))
                direction = new CubeCoordinates(1, 0, 0);
            else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveUpward))
                direction = new CubeCoordinates(0, 0, 1);
            else if (Input.CurrentKeyboardState.IsKeyDown(Nocubeless.Settings.Keys.MoveBackward))
                direction = new CubeCoordinates(0, 0, -1);
            else
                direction = new CubeCoordinates(0, 1, 0);

            int scrollWheelMovement = Input.GetScrollWheelMovement();

            if (scrollWheelMovement != 0)
            {
                scrollWheelMovement /= Math.Abs(scrollWheelMovement);

                Nocubeless.CubeWorld.PreviewableCube.Coordinates += direction * scrollWheelMovement;
            }

            Cube cubeToLay = new Cube(Nocubeless.Player.NextColorToLay /*TODO: Change with a kind of Editing/NextColorToLay*/, Nocubeless.CubeWorld.PreviewableCube.Coordinates);
            Nocubeless.CubeWorld.PreviewCube(cubeToLay);

            if (Input.WasRightMouseButtonJustPressed())
            {
                Nocubeless.CubeWorld.LayCube(cubeToLay);
            }
        }
    }
}
