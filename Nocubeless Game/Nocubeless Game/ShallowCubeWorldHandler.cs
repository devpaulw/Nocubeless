using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class ShallowCubeWorldHandler : ICubeWorldHandler
    {
        private readonly List<CubeChunk> chunks;

        public ShallowCubeWorldHandler()
        {
            chunks = new List<CubeChunk>();
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
                return null;
            }
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
    }
}
