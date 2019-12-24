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
        private readonly CubeChunkDrawer chunkDrawer;
        private readonly CubeDrawer cubeDrawer; // for the previewable cube

        public List<CubeChunk> LoadedChunks { get; private set; } // Do a chunk collection 
        public Cube PreviewableCube { get; private set; }

        public CubeWorldScene(Nocubeless nocubeless) : base(nocubeless)
        {
            LoadedChunks = new List<CubeChunk>();

            chunkDrawer = new CubeChunkDrawer(Nocubeless, Nocubeless.CubeWorld.Settings.HeightOfCubes);
            cubeDrawer = new CubeDrawer(Nocubeless, Nocubeless.CubeWorld.Settings.HeightOfCubes);
        }

        public override void Update(GameTime gameTime)
        {
            LoadChunks(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Camera.Position));

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (CubeChunk chunk in LoadedChunks)
            {
                DrawChunk(chunk);
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

        private void LoadChunks(Coordinates playerCoordinates) // it's the big cube algorithm
        {
            int viewingCubes = CubeChunk.Size * Nocubeless.Settings.Graphics.ChunkViewDistance;
            int min = -(viewingCubes / 2);
            int max = viewingCubes / 2;

            var mentionedChunks = new List<Coordinates>();

            for (int x = min; x < max; x += CubeChunk.Size)
            {
                for (int y = min; y < max; y += CubeChunk.Size)
                {
                    for (int z = min; z < max; z += CubeChunk.Size)
                    { // explore chunks to load
                        var chunkCoordinates = CubeChunk.Helper.FindBaseCoordinates(new Coordinates(playerCoordinates.X + x, playerCoordinates.Y + y, playerCoordinates.Z + z));
                        mentionedChunks.Add(chunkCoordinates);

                        var requestedChunks = from chunk in LoadedChunks
                                              where chunk.Coordinates.Equals(chunkCoordinates)
                                              select chunk;

                        if (requestedChunks.Count() == 0)
                        {
                            var gotChunk = Nocubeless.CubeWorld.GetChunkAt(chunkCoordinates);
                            LoadedChunks.Add(gotChunk);
                        }
                    }
                }
            }

            for (int i = 0; i < LoadedChunks.Count; i++) // explore chunks to unload
            {
                if (!mentionedChunks.Contains(LoadedChunks[i].Coordinates))
                    LoadedChunks.RemoveAt(i);
            }
        }

        private void DrawChunk(CubeChunk chunk)
        {
            Vector3 position = Nocubeless.CubeWorld.GetGraphicsCubePosition(chunk.Coordinates);
            float gaps = 1 / Nocubeless.CubeWorld.GetGraphicsCubeRatio(); // DESIGN: don't do 1 / x

            EffectMatrices effectMatrices =
                new EffectMatrices(Nocubeless.Camera.ProjectionMatrix,
                Nocubeless.Camera.ViewMatrix,
                Matrix.Identity);

            chunkDrawer.Draw(chunk, position, gaps, effectMatrices);
        }
        private void DrawCube(Cube cube, float transparency = 1.0f)
        {
            Vector3 cubeScenePosition = Nocubeless.CubeWorld.GetGraphicsCubePosition(cube.Coordinates);

            EffectMatrices effectMatrices =
                new EffectMatrices(Nocubeless.Camera.ProjectionMatrix,
                Nocubeless.Camera.ViewMatrix,
                Matrix.Identity);

            cubeDrawer.Draw(cubeScenePosition, cube.Color.ToVector3(), effectMatrices, transparency);
        }

        public static class Helper
        {
            public static Coordinates GetTargetedCube(Camera camera, CubeWorld cubeWorld, int maxLayingDistance) // is not 100% trustworthy, and is not powerful, be careful
            {
                Vector3 checkPosition = camera.Position * cubeWorld.GetGraphicsCubeRatio();

                Coordinates actualPosition = null;
                Coordinates convertedCheckPosition;

                const int checkIntensity = 100;
                float checkIncrement = (float)maxLayingDistance / checkIntensity;

                for (int i = 0; i < checkIntensity; i++)
                { // in World, is free space
                    checkPosition += camera.Front * checkIncrement; // increment check zone
                    convertedCheckPosition = checkPosition.ToCubeCoordinate();

                    if (convertedCheckPosition != actualPosition)
                    {
                        if (!cubeWorld.IsFreeSpace(convertedCheckPosition)) // check if it's a free space
                            return convertedCheckPosition;
                    }

                    actualPosition = convertedCheckPosition;
                }

                return null;
            }
            public static Coordinates GetTargetedNewCube(Camera camera, CubeWorld cubeWorld, int maxLayingDistance) // is not 100% trustworthy, and is not powerful, be careful
            {
                Vector3 checkPosition = camera.Position * cubeWorld.GetGraphicsCubeRatio();

                Coordinates oldPosition = null;
                Coordinates actualPosition = null;
                Coordinates convertedCheckPosition;

                const int checkIntensity = 100;
                float checkIncrement = (float)maxLayingDistance / checkIntensity;

                for (int i = 0; i < checkIntensity; i++)
                { // in World, is free space
                    checkPosition += camera.Front * checkIncrement; // increment check zone
                    convertedCheckPosition = checkPosition.ToCubeCoordinate();

                    if (convertedCheckPosition != actualPosition) // perf maintainer
                    {
                        if (oldPosition != null && !cubeWorld.IsFreeSpace(convertedCheckPosition)) // check if it's a free space
                            return oldPosition;
                        else if (actualPosition != null) // or accept the new checkable position (or exit if actualPosition wasn't initialized)
                            oldPosition = actualPosition;
                    }

                    actualPosition = convertedCheckPosition;
                }

                return actualPosition;
            }
        }
    }
}