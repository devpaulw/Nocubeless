using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    [Obsolete("Useless")]
    class ShallowCubeWorldHandler : ICubeWorldHandler
    {
        private readonly List<CubeChunk> chunks;

        public ShallowCubeWorldHandler()
        {
            chunks = new List<CubeChunk>();
        }

        public CubeChunk GetChunkAt(WorldCoordinates coordinates)
        {
            var gotChunk = (from chunk in chunks
                            where chunk.Coordinates.Equals(coordinates)
                            select chunk).FirstOrDefault();

            if (gotChunk != null) // shallow copy
            {
                var newChunk = new CubeChunk(gotChunk.Coordinates);
                for (int i = 0; i < CubeChunk.TotalSize; i++)
                    newChunk[i] = gotChunk[i];
                return newChunk;
            }
            else
                return null;
        }

        public void SetChunk(CubeChunk chunk)
        {
            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i].Coordinates.Equals(chunk.Coordinates))
                    chunks.RemoveAt(i);
            }

            chunks.Add(chunk);
        }

        public bool ChunkExistsAt(WorldCoordinates coordinates)
        {
            var gotChunk = (from chunk in chunks
                            where chunk.Coordinates.Equals(coordinates)
                            select chunk).FirstOrDefault();

            if (gotChunk == null)
                return false;
            else return true;
        }
    }
}
