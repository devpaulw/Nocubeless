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
    class CubeWorldProcessor : NocubelessDrawableComponent
    {
        private readonly CubeChunkDrawer chunkDrawer;
        private readonly CubeDrawer cubeDrawer; // for the previewable cube

        public CubeWorldProcessor(Nocubeless nocubeless) : base(nocubeless)
        {
            chunkDrawer = new CubeChunkDrawer(Nocubeless, Nocubeless.CubeWorld.Settings.HeightOfCubes);
            cubeDrawer = new CubeDrawer(Nocubeless, Nocubeless.CubeWorld.Settings.HeightOfCubes);
        }

        public override void Update(GameTime gameTime)
        {
            LoadChunks(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Camera.Position)); // DESIGN, A Player class?
            UnloadFarChunks(Nocubeless.CubeWorld.GetCoordinatesFromGraphics(Nocubeless.Camera.Position));

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (CubeChunk chunk in Nocubeless.CubeWorld.LoadedChunks)
                DrawChunk(chunk);

            if (Nocubeless.CubeWorld.PreviewableCube != null) // if break mode is off
                DrawCube(Nocubeless.CubeWorld.PreviewableCube, 0.5f);

            base.Draw(gameTime);
        }


        private void LoadChunks(Coordinates playerCoordinates) // it's the big cube algorithm
        {
            int viewingCubes = CubeChunk.Size * Nocubeless.Settings.Graphics.ChunkViewDistance;
            int min = -(viewingCubes / 2);
            int max = viewingCubes / 2;

            for (int x = min; x < max; x += CubeChunk.Size)
            {
                for (int y = min; y < max; y += CubeChunk.Size)
                {
                    for (int z = min; z < max; z += CubeChunk.Size)
                    { // explore chunks to load
                        var chunkCoordinates = CubeChunk.Helper.FindBaseCoordinates(new Coordinates(playerCoordinates.X + x, playerCoordinates.Y + y, playerCoordinates.Z + z));

                        var tookChunk = Nocubeless.CubeWorld.TakeChunkAt(chunkCoordinates);

                        if (tookChunk == null)
                            Nocubeless.CubeWorld.LoadChunk(chunkCoordinates);
                    }
                }
            }
        }

        void UnloadFarChunks(Coordinates playerCoordinates) // TODO: no need this argument now
        {
            int viewingCubes = CubeChunk.Size * Nocubeless.Settings.Graphics.ChunkViewDistance;
            int min = -(viewingCubes / 2);
            int max = viewingCubes / 2;

            for (int i = 0; i < Nocubeless.CubeWorld.LoadedChunks.Count; i++)
            {
                var playerMinCoordinates = CubeChunk.Helper.FindBaseCoordinates(new Coordinates(playerCoordinates.X + min, playerCoordinates.Y + min, playerCoordinates.Z + min));
                var playerMaxCoordinates = CubeChunk.Helper.FindBaseCoordinates(new Coordinates(playerCoordinates.X + max, playerCoordinates.Y + max, playerCoordinates.Z + max));

                if (Nocubeless.CubeWorld.LoadedChunks[i].Coordinates < playerMinCoordinates 
                    || Nocubeless.CubeWorld.LoadedChunks[i].Coordinates > playerMaxCoordinates)
                {
                    var gotChunk = Nocubeless.CubeWorld.TakeChunkAt(Nocubeless.CubeWorld.LoadedChunks[i].Coordinates);
                    Nocubeless.CubeWorld.UnloadChunk(gotChunk);
                }
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            for (int i = 0; i < Nocubeless.CubeWorld.LoadedChunks.Count; i++) // save each chunk before closing
                Nocubeless.CubeWorld.UnloadChunk(Nocubeless.CubeWorld.LoadedChunks[i]);

            base.Dispose(disposing);
        }

        private void DrawChunk(CubeChunk chunk)
        {
            Vector3 position = Nocubeless.CubeWorld.GetGraphicsCubePosition(chunk.Coordinates);
            float gaps = 1 / Nocubeless.CubeWorld.GetGraphicsCubeRatio(); // DESIGN: don't do 1 / x

            EffectMatrices effectMatrices =
                new EffectMatrices(Nocubeless.Camera.ProjectionMatrix,
                Nocubeless.Camera.ViewMatrix,
                Matrix.Identity);

            chunkDrawer.Draw(ref chunk, position, gaps, effectMatrices);
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
    }
}
