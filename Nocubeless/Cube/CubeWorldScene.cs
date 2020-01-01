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

        public CubeWorld World { get; }
        public List<CubeChunk> LoadedChunks { get; private set; } // Do a chunk collection 
        public Cube PreviewableCube { get; private set; }

        public CubeWorldScene(Nocubeless nocubeless) : base(nocubeless)
        {
            World = Nocubeless.CubeWorld;
            LoadedChunks = new List<CubeChunk>();

            chunkDrawer = new CubeChunkDrawer(Nocubeless, World.Settings.HeightOfCubes);
            cubeDrawer = new CubeDrawer(Nocubeless, World.Settings.HeightOfCubes);
        }

        public override void Update(GameTime gameTime)
        {
            LoadChunks(World.GetCoordinatesFromGraphics(Nocubeless.Camera.Position)); // DESIGN, A Player class?
            UnloadFarChunks(World.GetCoordinatesFromGraphics(Nocubeless.Camera.Position));

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (CubeChunk chunk in LoadedChunks)
                DrawChunk(chunk);

            if (PreviewableCube != null) // if break mode is off
                DrawCube(PreviewableCube, 0.5f);

            base.Draw(gameTime);
        }

        public void LayCube(Cube cube)
        {
            var chunkCoordinates = CubeChunk.Helper.FindBaseCoordinates(cube.Coordinates);

            var tookChunk = TakeChunkAt(chunkCoordinates);

            int cubePositionInChunk = CubeChunk.Helper.GetPositionFromCoordinates(cube.Coordinates);

            tookChunk[cubePositionInChunk] = cube.Color;
        }

        public void BreakCube(Coordinates coordinates)
        {
            var chunkCoordinates = CubeChunk.Helper.FindBaseCoordinates(coordinates);

            var tookChunk = TakeChunkAt(chunkCoordinates);

            int cubePositionInChunk = CubeChunk.Helper.GetPositionFromCoordinates(coordinates);

            tookChunk[cubePositionInChunk] = null;
        }

        public void PreviewCube(Cube cube)
        {
            PreviewableCube = cube; //
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

                        var tookChunk = TakeChunkAt(chunkCoordinates);

                        if (tookChunk == null)
                            LoadChunk(chunkCoordinates);
                    }
                }
            }
        }

        void UnloadFarChunks(Coordinates playerCoordinates)
        {
            int viewingCubes = CubeChunk.Size * Nocubeless.Settings.Graphics.ChunkViewDistance;
            int min = -(viewingCubes / 2);
            int max = viewingCubes / 2;

            for (int i = 0; i < LoadedChunks.Count; i++)
            {
                var playerMinCoordinates = CubeChunk.Helper.FindBaseCoordinates(new Coordinates(playerCoordinates.X + min, playerCoordinates.Y + min, playerCoordinates.Z + min));
                var playerMaxCoordinates = CubeChunk.Helper.FindBaseCoordinates(new Coordinates(playerCoordinates.X + max, playerCoordinates.Y + max, playerCoordinates.Z + max));

                if (LoadedChunks[i].Coordinates < playerMinCoordinates || LoadedChunks[i].Coordinates > playerMaxCoordinates)
                {
                    var gotChunk = TakeChunkAt(LoadedChunks[i].Coordinates);
                    UnloadChunk(gotChunk);
                }
            }
        }

        void LoadChunk(Coordinates chunkCoordinates)
        {
            var gotChunk = World.GetChunkAt(chunkCoordinates);
            if (gotChunk != null)
                LoadedChunks.Add(gotChunk);
            else
                LoadedChunks.Add(new CubeChunk(chunkCoordinates)); // TODO: ForceGetChunkAt
        }

        void UnloadChunk(CubeChunk chunk)
        {
            World.TrySetChunk(chunk);
            LoadedChunks.Remove(chunk);
        }

        public bool IsFreeSpace(Coordinates coordinates) // TO-OPTIMIZE
        {
            var chunkCoordinates = CubeChunk.Helper.FindBaseCoordinates(coordinates);

            var gotChunk = (from chunk in LoadedChunks
                            where chunk.Coordinates.Equals(chunkCoordinates)
                            select chunk).FirstOrDefault();

            if (gotChunk == null) // don't try to check in a not loaded chunk, or it will crash
                return false;

            int cubePositionInChunk = CubeChunk.Helper.GetPositionFromCoordinates(coordinates);

            if (!(gotChunk[cubePositionInChunk] == null))
                return false;

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            for (int i = 0; i < LoadedChunks.Count; i++) // save each chunk before closing
                UnloadChunk(LoadedChunks[i]);

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

        private CubeChunk TakeChunkAt(Coordinates chunkCoordinates)
        {
            return (from chunk in LoadedChunks
                    where chunk.Coordinates.Equals(chunkCoordinates)
                    select chunk).FirstOrDefault();
        }
    }
}