using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nocubeless
{
    class CubeWorldScene : NocubelessDrawableComponent
    {
        private readonly ModelMeshPart cubeMeshPart; // Store rendering attributes
        private CubeEffect effect;
        private readonly Matrix cubeScale;

        public List<Cube> LoadedCubes { get; private set; }
        public Cube PreviewableCube { get; private set; }

        public CubeWorldScene(Nocubeless nocubeless) : base(nocubeless)
        {
            //LoadedCubes = new List<Cube>();
            LoadedCubes = Nocubeless.CubeWorld.XCHEATGETCUBESDIRECTLY; // temp

            cubeMeshPart = Cube.LoadModel(Game.GraphicsDevice);
            effect = new CubeEffect(Game.Content.Load<Effect>("CubeEffect")); // DESIGN: Content better handler
            cubeScale = Matrix.CreateScale(Nocubeless.CubeWorld.Settings.HeightOfCubes);
        }

        public override void Update(GameTime gameTime)
        {
            // DOLATER: Chunk loading system

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Cube cube in LoadedCubes)
            {
                DrawCube(cube);
            }

            if (PreviewableCube != null) // if break mode is off
                DrawCube(PreviewableCube, 0.5f);

            base.Draw(gameTime);
        }

        public void PreviewCube(Cube cube)
        {
            PreviewableCube = cube;
        }
        public void LayPreviewedCube() // optional func
        {
            Nocubeless.CubeWorld.LayCube(PreviewableCube);
        }

        public bool IsFreeSpace(CubeWorldCoordinates position)
        {
            // DOLATER: to link with get cube
            foreach (Cube cube in LoadedCubes)
                if (cube.Position.Equals(position))
                    return false;

            return true;
        }

        public CubeWorldCoordinates GetTargetedCube() // Is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = Nocubeless.Camera.Position * Nocubeless.CubeWorld.GetSceneCubeRatio();

            CubeWorldCoordinates actualPosition = null;
            CubeWorldCoordinates convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)Nocubeless.Settings.CubeHandler.MaxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Nocubeless.Camera.Front * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition)
                {
                    if (!IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
                        return convertedCheckPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return null;
        }
        public CubeWorldCoordinates GetTargetedNewCube() // Is not 100% trustworthy, and is not powerful, be careful
        {
            Vector3 checkPosition = Nocubeless.Camera.Position * Nocubeless.CubeWorld.GetSceneCubeRatio();

            CubeWorldCoordinates oldPosition = null;
            CubeWorldCoordinates actualPosition = null;
            CubeWorldCoordinates convertedCheckPosition;

            const int checkIntensity = 100;
            float checkIncrement = (float)Nocubeless.Settings.CubeHandler.MaxLayingDistance / checkIntensity;

            for (int i = 0; i < checkIntensity; i++)
            { // In World, is free space
                checkPosition += Nocubeless.Camera.Front * checkIncrement; // Increment check zone
                convertedCheckPosition = checkPosition.ToCubeCoordinate();

                if (convertedCheckPosition != actualPosition) // Perf maintainer
                {
                    if (oldPosition != null && !IsFreeSpace(convertedCheckPosition)) // Check if it's a free space
                        return oldPosition;
                    else if (actualPosition != null) // Or accept the new checkable position (or exit if actualPosition wasn't initialized)
                        oldPosition = actualPosition;
                }

                actualPosition = convertedCheckPosition;
            }

            return actualPosition;
        }

        private void DrawCube(Cube cube, float transparency = 1.0f)
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
