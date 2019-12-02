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
        public Camera Camera { get; set; }

        public World(IGameApp game, CubeEffect effect, Camera camera) : base(game.Instance)
        {
            Settings = game.Settings.World;
            Effect = effect;
            Camera = camera;

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
                DrawCube(previewableCube, 0.5f);

            base.Draw(gameTime);
        }

        public void LayCube(Cube cube)
        {
            drawingCubes.Add(cube);
        }

        public void LayPreviewedCube()
        {
            LayCube(previewableCube);
        }

        public void BreakCube(CubeCoordinate position)
        {
            for (int i = 0; i < drawingCubes.Count; i++)
            {
                if (drawingCubes[i].Position.X == position.X &&  // DESIGN: Make that way cleaner, an object equals override
                    drawingCubes[i].Position.Y == position.Y &&
                    drawingCubes[i].Position.Z == position.Z)
                {
                    drawingCubes.RemoveAt(i);
                }
            }
        }

        public void PreviewCube(Cube cube)
        {
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

        private void DrawCube(Cube cube, float transparency = 1.0f)
        {
            Vector3 cubeScenePosition = cube.Position.GetScenePosition(Settings.HeightOfCubes);
            Matrix translation = Matrix.CreateTranslation(cubeScenePosition);
            Matrix world = scale * translation;

            Effect.View = Camera.ViewMatrix;
            Effect.Projection = Camera.ProjectionMatrix;
            Effect.World = world;
            Effect.Color = cube.Color;
            Effect.Alpha = transparency;

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
