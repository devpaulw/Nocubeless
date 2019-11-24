using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class DrawableCubes : DrawableGameComponent
    {
        private readonly List<Cube> toDraw;
        private readonly ModelMeshPart cubeMeshPart; // Store rendering attributes
        private Matrix scale;
        private readonly CubeEffect effect;

        public Camera Camera { get; set; }
        public float Height { get; }

        public DrawableCubes(IGameApp game, float cubesHeight) : base(game.Instance)
        {
            Height = cubesHeight;
            Camera = game.Camera;

            toDraw = new List<Cube>();
            cubeMeshPart = Cube.LoadModel(GraphicsDevice);
            scale = Matrix.CreateScale(Height);
            effect = new CubeEffect(Game.Content.Load<Effect>("CubeEffect"));
        }

        public void Add(Cube cube)
        {
            toDraw.Add(cube);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Cube cube in toDraw)
            {
                DrawCube(cube);
            }

            base.Draw(gameTime);
        }

        public void DrawCube(Cube cube)
        {
            Matrix translation = Matrix.CreateTranslation(cube.Position.GetScenePosition(Height));

            effect.World = scale * translation;
            effect.View = Camera.ViewMatrix;
            effect.Projection = Camera.ProjectionMatrix;
            effect.Color = cube.Color;

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
        public bool IsFreeSpace(CubeCoordinate position)
        {
            foreach (Cube cube in toDraw)
                if (cube.Position.X == position.X &&
                    cube.Position.Y == position.Y &&
                    cube.Position.Z == position.Z)
                    return false;

            return true;
        }
    }
}
