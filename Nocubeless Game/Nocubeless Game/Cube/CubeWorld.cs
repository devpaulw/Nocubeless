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
    internal class CubeWorld : NocubelessDrawableComponent
    {
        private readonly ModelMeshPart cubeMeshPart; // Store rendering attributes
        private readonly Matrix cubeScale;
        private readonly List<Cube> drawingCubes;
        private Cube previewableCube;

        public WorldSettings Settings { get; set; }
        public CubeEffect Effect { get; }

        public Cube NextCube { get; set; }

        public float SceneCubeRatio { 
            get {
                return 1.0f / Settings.HeightOfCubes / 2.0f;
            }
        }

        public CubeWorld(Nocubeless nocubeless) : base(nocubeless)
        {
            Settings = Nocubeless.Settings.World; // Is not correct for the long term
            Effect = new CubeEffect(Game.Content.Load<Effect>("CubeEffect")); // DESIGN: Content better handler

            cubeMeshPart = Cube.LoadModel(Game.GraphicsDevice);
            cubeScale = Matrix.CreateScale(Settings.HeightOfCubes);
            drawingCubes = new List<Cube>();

            /*TEST*/ LayCube(new Cube(Color.DarkBlue, new CubeWorldCoordinates(0, 0, -21))); // TestCube
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

        public void BreakCube(CubeWorldCoordinates position)
        {
            if (position == null)
                return;

            for (int i = 0; i < drawingCubes.Count; i++)
                if (drawingCubes[i].Position.Equals(position))
                    drawingCubes.RemoveAt(i);
        }

        public void PreviewCube(Cube cube)
        {
            previewableCube = cube;
        }

        public bool IsFreeSpace(CubeWorldCoordinates position)
        {
            foreach (Cube cube in drawingCubes)
                if (cube.Position.Equals(position))
                    return false;

            return true;
        }

        public Vector3 GetCubeScenePosition(CubeWorldCoordinates cubePosition) // DESIGN: To Move Place
        {
            return cubePosition.ToVector3() * 2.0f * Settings.HeightOfCubes;
        }

        private void DrawCube(Cube cube, float transparency = 1.0f)
        {
            Vector3 cubeScenePosition = GetCubeScenePosition(cube.Position);
            Matrix translation = Matrix.CreateTranslation(cubeScenePosition);
            Matrix world = cubeScale * translation;

            Effect.View = Nocubeless.Camera.ViewMatrix;
            Effect.Projection = Nocubeless.Camera.ProjectionMatrix;
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
