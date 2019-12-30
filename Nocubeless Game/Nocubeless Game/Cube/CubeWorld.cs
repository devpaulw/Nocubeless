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
            var gotChunk = Handler.GetChunkAt(coordinates);

            if (gotChunk == null) // create one if it does not exist
            {
                var newCreatedChunk = new CubeChunk(coordinates);
                Handler.SetChunk(newCreatedChunk);

                return Handler.GetChunkAt(coordinates);
            }

            return gotChunk;
        }

        public void SetChunk(CubeChunk chunk)
        {
            Handler.SetChunk(chunk);
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
