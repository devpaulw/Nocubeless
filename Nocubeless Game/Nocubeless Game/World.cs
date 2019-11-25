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
        private readonly Matrix scale;
        private readonly List<Cube> drawingCubes;
        private Cube previewableCube;

        public CubeEffect Effect { get; }
        public WorldSettings Settings { get; }

        public World(IGameApp game, CubeEffect effect) : base(game.Instance)
        {
            Settings = game.Settings.World;
            Effect = effect;

            cubeMeshPart = Cube.LoadModel(Game.GraphicsDevice);
            drawingCubes = new List<Cube>();
            scale = Matrix.CreateScale(Settings.HeightOfCubes);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Cube cube in drawingCubes)
            {
                DrawCube(cube);
            }
            if (previewableCube != null) 
                DrawCube(previewableCube);

            base.Draw(gameTime);
        }

        public void LayCube(Cube cube)
        {
            drawingCubes.Add(cube);
        }

        public void BreakCube(CubeCoordinate position)
        {
            for (int i = 0; i < drawingCubes.Count; i++)
            {
                if(drawingCubes[i].Position.X == position.X &&  // DESIGN: Make that way cleaner
                    drawingCubes[i].Position.Y == position.Y &&
                    drawingCubes[i].Position.Z == position.Z)
                {
                    drawingCubes.RemoveAt(i);
                }
            }
        }

        public void PreviewCube(CubeCoordinate position)
        {
            if (position == null)
                previewableCube = null;

            Color color = Color.PaleVioletRed;
            Cube cube = new Cube(color, position);

            previewableCube = cube;
        }

        public bool IsFreeSpace(CubeCoordinate position)
        {
            foreach (Cube cube in drawingCubes)
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
