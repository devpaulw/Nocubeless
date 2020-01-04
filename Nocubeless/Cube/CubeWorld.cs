using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nocubeless
{
    class CubeWorld
    {
        public CubeWorldSettings Settings { get; }
        public ICubeWorldHandler Handler { get; }
        public List<CubeChunk> LoadedChunks { get; private set; } // Do a chunk collection 
        public Cube PreviewableCube { get; private set; }

        public CubeWorld(CubeWorldSettings settings, ICubeWorldHandler handler)
        {
            Settings = settings;
            Handler = handler;
            LoadedChunks = new List<CubeChunk>();
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
            PreviewableCube = cube;
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

        public void LoadChunk(Coordinates chunkCoordinates)
        {
            var gotChunk = GetChunkAt(chunkCoordinates);
            if (gotChunk != null)
                LoadedChunks.Add(gotChunk);
            else
                LoadedChunks.Add(new CubeChunk(chunkCoordinates)); // TODO: ForceGetChunkAt
        }

        public void UnloadChunk(CubeChunk chunk)
        {
            TrySetChunk(chunk);
            LoadedChunks.Remove(chunk);
        }

        public CubeChunk TakeChunkAt(Coordinates chunkCoordinates)
        {
            return (from chunk in LoadedChunks
                    where chunk.Coordinates.Equals(chunkCoordinates)
                    select chunk).FirstOrDefault();
        }

        // BBMSG i think Cube (or Coordinates) should know how to convert themself to Graphics (a method cube.GetGraphicsCoordinates or coordinates.ToGraphics)
        // BBMSG moreover i think the cubes should know their height (it's overkill to store in all cubes the same height but maybe we can make a static Cube.Size initialized by CubeWorld)
        public Vector3 GetGraphicsCubePosition(Coordinates cubePosition) // cube position in graphics representation.
        {
            return cubePosition.ToVector3() / GetGraphicsCubeRatio();
        }
        public Coordinates GetCoordinatesFromGraphics(Vector3 position)
        {
            return (position * GetGraphicsCubeRatio()).ToCubeCoordinate();
        }
        public float GetGraphicsCubeRatio() // how much is a cube smaller/bigger in the graphics representation?
        {
            return 1.0f / (Settings.HeightOfCubes * 2.0f);
        }

        private CubeChunk GetChunkAt(Coordinates coordinates)
        {
            return Handler.GetChunkAt(coordinates);
        }

        private void TrySetChunk(CubeChunk chunk)
        {
            if (!chunk.IsEmpty() // optimized, if we try to set an empty chunk it will not really write it
                || Handler.ChunkExistsAt(chunk.Coordinates)) // if the chunk already exists, set it anyway because it would not be overwrote
            {
                Handler.SetChunk(chunk);
            }
        }
    }
}