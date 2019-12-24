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
            var chunkCoordinates = CubeChunk.Helper.FindBaseCoordinates(cube.Coordinates);

            var updatedChunk = Handler.GetChunkAt(chunkCoordinates);

            int cubePositionInChunk = CubeChunk.Helper.GetPositionFromCoordinates(cube.Coordinates);

            updatedChunk[cubePositionInChunk] = cube.Color;

            Handler.SetChunk(updatedChunk);
        }

        public void BreakCube(Coordinates coordinates)
        {
            var chunkCoordinates = CubeChunk.Helper.FindBaseCoordinates(coordinates);

            var updatedChunk = Handler.GetChunkAt(chunkCoordinates);

            int cubePositionInChunk = CubeChunk.Helper.GetPositionFromCoordinates(coordinates);

            updatedChunk[cubePositionInChunk] = CubeColor.Empty;

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

        public bool IsFreeSpace(Coordinates coordinates) // TO-OPTIMIZE
        {
            var chunkCoordinates = CubeChunk.Helper.FindBaseCoordinates(coordinates);

            var gotChunk = Handler.GetChunkAt(chunkCoordinates);

            int cubePositionInChunk = CubeChunk.Helper.GetPositionFromCoordinates(coordinates);

            if (!gotChunk[cubePositionInChunk].Equals(CubeColor.Empty))
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
