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

        public CubeChunk GetChunkAt(Coordinates coordinates)
        {
            return Handler.GetChunkAt(coordinates);
        }

        public void TrySetChunk(CubeChunk chunk)
        {
            if (!chunk.IsEmpty() // optimized, if we try to set an empty chunk it will not really write it
                || Handler.ChunkExistsAt(chunk.Coordinates)) // if the chunk already exists, set it anyway because it would not be overwrote
            {
                Handler.SetChunk(chunk);
            }
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
