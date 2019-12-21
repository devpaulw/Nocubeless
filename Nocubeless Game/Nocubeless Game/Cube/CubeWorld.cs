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
        private readonly List<CubeChunk> chunks;

        public CubeWorldSettings Settings { get; set; }

        public CubeWorld(CubeWorldSettings settings)
        {
            Settings = settings; // Is not correct for the long term

            chunks = new List<CubeChunk>();
        }

        public void LayCube(Cube cube)
        {
            var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(cube.Coordinates);

            for (int i = 0; i < chunks.Count; i++) // DESIGN
            {
                if (chunks[i].Coordinates.Equals(chunkCoordinates))
                {
                    int cubePositionInChunk = CubeChunkHelper.GetPositionFromCoordinates(cube.Coordinates);
                    chunks[i][cubePositionInChunk] = cube.Color;
                }
            }
        }

        public void BreakCube(Coordinates coordinates)
        {
            var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(coordinates);

            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i].Coordinates.Equals(chunkCoordinates))
                {
                    var cubePositionInChunk = CubeChunkHelper.GetPositionFromCoordinates(coordinates);
                    chunks[i][cubePositionInChunk] = new CubeColor(0, 0, 0);

                    break;
                }
            }
        }

        public CubeChunk GetChunkAt(Coordinates coordinates)
        {
            var requestedChunks = from chunk in chunks
                                  where chunk.Coordinates.Equals(coordinates)
                                  select chunk;

            if (requestedChunks.Count() != 0)
            {
                return requestedChunks.FirstOrDefault();
            }
            else
            {
                var newCreatedChunk = new CubeChunk(coordinates);
                chunks.Add(newCreatedChunk);

                return newCreatedChunk;
            }
        }

        public bool IsFreeSpace(Coordinates coordinates)
        {
            var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(coordinates);

            for (int i = 0; i < chunks.Count; i++) // DESIGN
            {
                if (chunks[i].Coordinates.Equals(chunkCoordinates))
                {
                    int cubePositionInChunk = CubeChunkHelper.GetPositionFromCoordinates(coordinates);

                    if (!chunks[i][cubePositionInChunk].Equals(new CubeColor(0, 0, 0)))
                        return false;

                }
            }

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
