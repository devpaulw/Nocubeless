﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public interface ICubeWorldHandler
    {
        CubeChunk GetChunkAt(CubeCoordinates coordinates);
        void SetChunk(CubeChunk chunk);
        bool ChunkExistsAt(CubeCoordinates coordinates);
    }
}
