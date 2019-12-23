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
    class CubeWorld
    {
        public CubeWorldSettings Settings { get; }
        public ICubeWorldHandler Handler { get; }

        public CubeWorld(CubeWorldSettings settings, ICubeWorldHandler handler)
        {
            Settings = settings;
            Handler = handler;
        }

        public void LayCube(Cube cube)
        {
            var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(cube.Coordinates);

            var updatedChunk = Handler.GetChunkAt(chunkCoordinates);

            int cubePositionInChunk = CubeChunkHelper.GetPositionFromCoordinates(cube.Coordinates);

            updatedChunk[cubePositionInChunk] = cube.Color;

            Handler.SetChunk(updatedChunk);
        }

        public void BreakCube(Coordinates coordinates)
        {
            var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(coordinates);

            var updatedChunk = Handler.GetChunkAt(chunkCoordinates);

            int cubePositionInChunk = CubeChunkHelper.GetPositionFromCoordinates(coordinates);

            updatedChunk[cubePositionInChunk] = new CubeColor(0, 0, 0);

            Handler.SetChunk(updatedChunk);
        }

        public CubeChunk GetChunkAt(Coordinates coordinates)
        {
            var gotChunk = Handler.GetChunkAt(coordinates);

            if (gotChunk == null) //create one if it does not exist
            {
                var newCreatedChunk = new CubeChunk(coordinates);
                Handler.SetChunk(newCreatedChunk);

                return Handler.GetChunkAt(coordinates);
            }

            return gotChunk;
        }

        public void LayChunk(CubeChunk chunk)
        { // TODO: Is that useless?
            Handler.SetChunk(chunk);
        }

        public bool IsFreeSpace(Coordinates coordinates)
        {
            var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(coordinates);

            var gotChunk = Handler.GetChunkAt(chunkCoordinates);

            int cubePositionInChunk = CubeChunkHelper.GetPositionFromCoordinates(coordinates);

            if (!gotChunk[cubePositionInChunk].Equals(new CubeColor(0, 0, 0)))
                return false;

            return true;
        }

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
            return 1.0f / Settings.HeightOfCubes / 2.0f;
        }
    }
}
