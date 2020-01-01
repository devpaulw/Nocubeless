using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class CubeDrawer : GameComponent
    {
        private readonly ModelMeshPart cubeMeshPart; // Store rendering attributes
        private readonly CubeEffect cubeEffect;
        private readonly Matrix cubeScale;

        public float HeightOfCubes { get; set; }

        public CubeDrawer(Game game, float heightOfCubes) : base(game)
        {
            HeightOfCubes = heightOfCubes;

            cubeMeshPart = Cube.LoadModel(Game.GraphicsDevice);
            cubeEffect = new CubeEffect(Game); /*DESIGN: Content better handler, not here*/
            cubeScale = Matrix.CreateScale(heightOfCubes);
        }

        public void Draw(Vector3 position, Vector3 color, EffectMatrices effectMatrices, float transparency = 1.0f)
        {
            Matrix translation = Matrix.CreateTranslation(position);
            Matrix world = cubeScale * translation * effectMatrices.World;

            cubeEffect.View = effectMatrices.View;
            cubeEffect.Projection = effectMatrices.Projection;
            cubeEffect.World = world;

            cubeEffect.Color = color;
            cubeEffect.Alpha = transparency;

            Game.GraphicsDevice.SetVertexBuffer(cubeMeshPart.VertexBuffer);
            Game.GraphicsDevice.Indices = cubeMeshPart.IndexBuffer;

            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game.GraphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList,
                0,
                cubeMeshPart.StartIndex,
                cubeMeshPart.PrimitiveCount);
            }
        }
    }
}
