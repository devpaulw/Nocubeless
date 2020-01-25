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
        bool shouldBreakCube = false;

        public EditingCubeCursorInputProcessor(Nocubeless nocubeless) : base(nocubeless)
        {

        }

        public override void Process()
        {
            var direction = new CubeCoordinates(0, 0, 0);

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

            if (Input.WasJustPressed(Nocubeless.Settings.Keys.ToggleLayBreak))
            {
                shouldBreakCube = !shouldBreakCube;
            }

            var cubeToLay = new Cube(Nocubeless.Player.NextColorToLay /*TODO: Change with a kind of Editing/NextColorToLay*/, Nocubeless.CubeWorld.PreviewableCube.Coordinates); // Make the Editor Cube class
            Nocubeless.CubeWorld.PreviewCube(cubeToLay);

            if (Input.WasRightMouseButtonJustPressed() && !shouldBreakCube)
            {
                Nocubeless.CubeWorld.LayCube(cubeToLay);
            }

            if (Input.WasLeftMouseButtonJustPressed() && shouldBreakCube)
            {
                Nocubeless.CubeWorld.BreakCube(Nocubeless.CubeWorld.PreviewableCube.Coordinates);
            }

            if (Input.WasMiddleMouseButtonJustPressed() && shouldBreakCube)
            {
                Pick();
            }

            if (Input.WasJustPressed(Nocubeless.Settings.Keys.SetPreviewableCubeAtTheFront))
            {
                Nocubeless.CubeWorld.PreviewableCube.Coordinates = Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Camera.Target);
            }
        }

        void Pick()
        {
            // TODO: Put in  separated another class
            var targetCubeCoordinates = Nocubeless.CubeWorld.PreviewableCube.Coordinates;
            var targetCubeColor = Nocubeless.CubeWorld.GetCubeColorAt(targetCubeCoordinates);

            if (targetCubeColor != null)
                Nocubeless.Player.NextColorToLay = targetCubeColor;
        }
    }
}
