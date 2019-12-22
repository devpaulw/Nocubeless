using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    interface ICubeWorldHandler
    {
        CubeChunk GetChunkAt(Coordinates coordinates);
        void SetChunk(CubeChunk chunk);
    }
}
