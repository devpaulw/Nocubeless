using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class PickerCube : GameComponent
    {
        private readonly CubeColor[] cubeColors;
        private readonly CubeDrawer cubeDrawer;
        private readonly EffectMatrices effectMatrices;

        public float Height { get; set; }

        public PickerCube(Game game, float height) : base(game)
        {
            Height = height;

            CreateColors(out cubeColors);
            cubeDrawer = new CubeDrawer(Game, Height); // BUG: lol bug

            #region Effect Matrices Set-up
            var fov = MathHelper.PiOver2;
            const float zNear = 0.1f, zFar = 100.0f;
            var position = new Vector3(0.0f, 0.0f, -5.0f);
            var target = new Vector3(0.0f, 0.0f, 0.0f);
            var up = Vector3.UnitY;
            effectMatrices = new EffectMatrices(
                Matrix.CreatePerspectiveFieldOfView(fov, Game.GraphicsDevice.Viewport.AspectRatio, zNear, zFar),
                Matrix.CreateLookAt(position, target, up),
                Matrix.CreateWorld(Vector3.Zero, -Vector3.UnitZ, up));
            #endregion
        }

        public void Draw(Vector2 position) // DOLATER: Put position as a constructior member instead! // And Moreover, the position is not coherent to the 2d representation
        {
            float cubeRatio = 1.0f / Height / 2.0f; ;

            for (int x = 0; x < 0b1000; x++)
            {
                for (int y = 0; y < 0b1000; y++)
                {
                    for (int z = 0; z < 0b1000; z++)
                    {
                        cubeDrawer.Draw( // DESIGN:! Why should I remake cubeRatio concept?
                            new Vector3(position.X + ((x - 3.5f) / cubeRatio), 
                            position.Y + ((y - 3.5f) / cubeRatio),
                            (z - 3.5f) / cubeRatio),
                            cubeColors[x + (y * 0b1000) + (z * 0b1000000)].ToVector3(),
                            effectMatrices); // not right order
                    }
                }
            }

        }

        public void RotateX(float axis) // rotation with origin should be respected
        {
            effectMatrices.World *= Matrix.CreateRotationX(MathHelper.ToRadians(axis));
        }

        public void RotateY(float axis)
        {
            effectMatrices.World *= Matrix.CreateRotationY(MathHelper.ToRadians(axis));
        }

        public void RotateZ(float axis)
        {
            effectMatrices.World *= Matrix.CreateRotationZ(MathHelper.ToRadians(axis));
        }

        public void Unsqueeze(float strength)
        {
            Height += strength; // Have to be proportional
        } 

        private void CreateColors(out CubeColor[] createdCubeColors)
        {
            createdCubeColors = new CubeColor[0b1000 * 0b1000 * 0b1000];

            for (int x = 0; x < 0b1000; x++)
            {
                for (int y = 0; y < 0b1000; y++)
                {
                    for (int z = 0; z < 0b1000; z++)
                    {
                        CubeColor newColor = new CubeColor(x, y, z);
                        createdCubeColors[x + (y * 0b1000) + (z * 0b1000000)] = newColor;
                    }
                }
            }

        }
    }
}
