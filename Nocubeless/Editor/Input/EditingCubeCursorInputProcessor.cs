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
        public EditingCubeCursorInputProcessor(Nocubeless nocubeless) : base(nocubeless)
        {
        }

        public override void Process()
        {
            CubeCoordinates direction = new CubeCoordinates(0, 0, 0);

            if (Input.WasJustPressed(Nocubeless.Settings.Keys.MoveRight))
                direction = new CubeCoordinates(Nocubeless.Camera.Right);
            else if (Input.WasJustPressed(Nocubeless.Settings.Keys.MoveLeft))
                direction = new CubeCoordinates(-Nocubeless.Camera.Right);
            else if (Input.WasJustPressed(Nocubeless.Settings.Keys.MoveForward))
                direction = new CubeCoordinates(Nocubeless.Camera.Front);
            else if (Input.WasJustPressed(Nocubeless.Settings.Keys.MoveBackward))
                direction = new CubeCoordinates(-Nocubeless.Camera.Front);
            else if (Input.WasJustPressed(Nocubeless.Settings.Keys.MoveUpward))
                direction = new CubeCoordinates(Nocubeless.Camera.Up);
            else if (Input.WasJustPressed(Nocubeless.Settings.Keys.MoveDown))
                direction = new CubeCoordinates(-Nocubeless.Camera.Up);

            Nocubeless.CubeWorld.PreviewableCube.Coordinates += direction;

            Cube cubeToLay = new Cube(Nocubeless.Player.NextColorToLay /*TODO: Change with a kind of Editing/NextColorToLay*/, Nocubeless.CubeWorld.PreviewableCube.Coordinates);
            Nocubeless.CubeWorld.PreviewCube(cubeToLay);

            if (Input.WasRightMouseButtonJustPressed())
            {
                Nocubeless.CubeWorld.LayCube(cubeToLay);
            }
        }
    }
}
