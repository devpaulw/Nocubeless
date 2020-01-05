using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public class CubeWorldSaveHandler : ICubeWorldHandler
    {
        public string FilePath { get; }

        public CubeWorldSaveHandler(string filePath)
        {
            #region File Path assignment and trying it exists
            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            FilePath = filePath;
            #endregion
        }

        public CubeChunk GetChunkAt(WorldCoordinates coordinates)
        {
            var dataOffset = GetChunkDataOffset(coordinates);

            if (dataOffset == -1)
                return null;
            else
            {
                var readChunk = new CubeChunk(coordinates);
                ReadChunkData(ref readChunk, (int)dataOffset);
                return readChunk;
            }
        }

        public void SetChunk(CubeChunk chunk)
        {
            if (chunk == null)
                throw new NullReferenceException();

            var dataOffset = GetChunkDataOffset(chunk.Coordinates);

            if (dataOffset == -1) // if the file doesn't contain a chunk at these coordinates, add it
                AddChunk(chunk);
            else // else, replace at the offset in the file
                ReplaceChunk(chunk, (int)dataOffset);
        }

        public bool ChunkExistsAt(WorldCoordinates coordinates)
        {
            var dataOffset = GetChunkDataOffset(coordinates);
            if (dataOffset == -1)
                return false;
            else return true;
        }

        #region Private Statements
        private void AddChunk(CubeChunk chunk)
        {
            var stream = File.Open(FilePath, FileMode.Append);

            using (var writer = new BinaryWriter(stream))
            {
                WriteChunkCoordinates(chunk.Coordinates, writer);
                WriteChunkData(chunk, writer);
            }

        }

        private void ReplaceChunk(CubeChunk chunk, int offset)
        {
            var stream = File.Open(FilePath, FileMode.Open);
            stream.Seek(offset, SeekOrigin.Begin);

            using (var writer = new BinaryWriter(stream))
            {
                WriteChunkData(chunk, writer);
            }
        }

        private void ReadChunkData(ref CubeChunk chunk, int dataOffset)
        {
            var stream = File.OpenRead(FilePath);
            stream.Seek((int)dataOffset, SeekOrigin.Begin);

            using (var reader = new BinaryReader(stream))
            {
                for (int i = 0; i < CubeChunk.TotalSize; i++)
                {
                    int r = reader.Read(),
                        g = reader.Read(),
                        b = reader.Read();

                    if (Convert.ToChar(r) == 'N')
                        chunk[i] = null;
                    else
                        chunk[i] = new CubeColor(r, g, b);
                }
            }
        }

        private static void WriteChunkCoordinates(WorldCoordinates chunkCoordinates, BinaryWriter writer)
        {
            writer.Write(chunkCoordinates.X);
            writer.Write(chunkCoordinates.Y);
            writer.Write(chunkCoordinates.Z);
        }

        private static void WriteChunkData(CubeChunk chunk, BinaryWriter writer)
        {
            for (int i = 0; i < CubeChunk.TotalSize; i++)
            {
                if (chunk[i] != null)
                {
                    writer.Write((byte)chunk[i].Red);
                    writer.Write((byte)chunk[i].Green);
                    writer.Write((byte)chunk[i].Blue);
                }
                else
                    writer.Write("NNN".ToCharArray());
            }
        }

        private int GetChunkDataOffset(WorldCoordinates chunkCoordinates)
        {
            int dataSize = CubeChunk.TotalSize * 3;

            var stream = File.OpenRead(FilePath);

            using (var reader = new BinaryReader(stream))
            {
                while (stream.Position < stream.Length)
                {
                    var foundCoordinates = new WorldCoordinates(reader.ReadInt32(),
                        reader.ReadInt32(),
                        reader.ReadInt32());

                    if (foundCoordinates == chunkCoordinates)
                        return (int)stream.Position;

                    stream.Seek(dataSize, SeekOrigin.Current); // jump to the next coordinates
                }

            }

            return -1; // no offset found
        }
        #endregion
    }
}