using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class World : DrawableGameComponent
    {
        private readonly ModelMeshPart cubeMeshPart; // Store rendering attributes
        private readonly List<Cube> toDraw;
        private readonly Matrix scale;

        public CubeEffect Effect { get; }
        public WorldSettings Settings { get; }

        public World(IGameApp game, CubeEffect effect) : base(game.Instance)
        {
            Settings = game.Settings.World;
            Effect = effect;

            cubeMeshPart = Cube.LoadModel(Game.GraphicsDevice);
            toDraw = new List<Cube>();
            scale = Matrix.CreateScale(Settings.HeightOfCubes);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Cube cube in toDraw)
            {
                DrawCube(cube);
            }

            base.Draw(gameTime);
        }

        public void LayCube(Cube cube)
        {
            toDraw.Add(cube);
        }

        public void BreakCube(CubeCoordinate position)
        {
            
        }

        public void PreviewCube(CubeCoordinate position)
        {
            Color cubeColor = Color.PaleVioletRed;
            Cube previewCube = new Cube(cubeColor, position);

            DrawCube(previewCube);
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

        private void DrawCube(Cube cube)
        {
            Vector3 cubeScenePosition = cube.Position.GetScenePosition(Settings.HeightOfCubes);
            Matrix translation = Matrix.CreateTranslation(cubeScenePosition);

            Effect.World = scale * translation;
            Effect.Color = cube.Color;

            GraphicsDevice.SetVertexBuffer(cubeMeshPart.VertexBuffer);
            GraphicsDevice.Indices = cubeMeshPart.IndexBuffer;

            foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
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
