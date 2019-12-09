using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nocubeless
{
    class CubeWorldDrawer : NocubelessDrawableComponent
    {
        private readonly ModelMeshPart cubeMeshPart; // Store rendering attributes
        private CubeEffect effect;
        private readonly Matrix cubeScale;

        public CubeWorldDrawer(Nocubeless nocubeless) : base(nocubeless)
        {
            cubeMeshPart = Cube.LoadModel(Game.GraphicsDevice);
            effect = new CubeEffect(Game.Content.Load<Effect>("CubeEffect")); // DESIGN: Content better handler
            cubeScale = Matrix.CreateScale(Nocubeless.CubeWorld.Settings.HeightOfCubes);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Cube cube in Nocubeless.CubeWorld.LoadedCubes)
            {
                DrawCube(cube);
            }

            base.Draw(gameTime);
        }

        protected void DrawCube(Cube cube, float transparency = 1.0f)
        {
            Vector3 cubeScenePosition = Nocubeless.CubeWorld.GetCubeScenePosition(cube.Position);
            Matrix translation = Matrix.CreateTranslation(cubeScenePosition);
            Matrix world = cubeScale * translation;

            effect.View = Nocubeless.Camera.ViewMatrix;
            effect.Projection = Nocubeless.Camera.ProjectionMatrix;
            effect.World = world;
            effect.Color = cube.Color;
            effect.Alpha = transparency;

            GraphicsDevice.SetVertexBuffer(cubeMeshPart.VertexBuffer);
            GraphicsDevice.Indices = cubeMeshPart.IndexBuffer;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList,
                0,
                cubeMeshPart.StartIndex,
                cubeMeshPart.PrimitiveCount);
            }
        }
    }
}
