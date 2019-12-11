using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class PickerCubeDrawer : GameComponent
    {
        private CubeColor[] cubeColors;
        private CubeDrawer cubeDrawer;
        private EffectMatrices effectMatrices;

        public float Height { get; set; }

        public PickerCubeDrawer(Game game, float height) : base(game)
        {
            Height = height;

            CreateColors(out cubeColors);
            cubeDrawer = new CubeDrawer(Game, 0.1f); // BUG: lol bug

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

        public void Draw(Vector2 position)
        {
            /*tmp*/ ;

            float cubeRatio = CubeWorld.GetGraphicsCubeRatio(Height);

            for (int x = 0; x < 0b1000; x++)
            {
                for (int y = 0; y < 0b1000; y++)
                {
                    for (int z = 0; z < 0b1000; z++)
                    {
                        cubeDrawer.Draw(
                            new Vector3(position.X + (x / cubeRatio), 
                            position.Y + (y / cubeRatio), 
                            z / cubeRatio),

                            cubeColors[x + (y * 0b1000) + (z * 0b1000 * 0b1000)].ToVector3(),
                            effectMatrices); // not right order
                    }
                }
            }

        }

        public void Rotate(float xAxis, float yAxis, float zAxis)
        {
            effectMatrices.World *=
                Matrix.CreateRotationX(MathHelper.ToRadians(xAxis)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(yAxis)) *
                Matrix.CreateRotationZ(MathHelper.ToRadians(zAxis));
        }

        public void Unsqueeze(float strength)
        {
            Height += strength; // Have to be proportional
            /*tmp*/ if (Height > 0.35f)
            {
                Height = 0.1f;
            }
                
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
                        createdCubeColors[x + (y * 0b1000) + (z * 0b1000 * 0b1000)] = newColor;
                    }
                }
            }
        }
    }
}
