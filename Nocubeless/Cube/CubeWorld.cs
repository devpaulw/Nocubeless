using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nocubeless
{
	class CubeWorld
	{
		public CubeWorldSettings Settings { get; }
		public ICubeWorldHandler Handler { get; }
		public List<CubeChunk> LoadedChunks { get; private set; } // Do a chunk collection 
		public Cube PreviewableCube { get; private set; }

		public CubeWorld(CubeWorldSettings settings, ICubeWorldHandler handler)
		{
			Settings = settings;
			Handler = handler;
			LoadedChunks = new List<CubeChunk>();
		}

		public void LayCube(Cube cube)
		{
			var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(cube.Coordinates);

			var tookChunk = TakeChunkAt(chunkCoordinates);

			int cubePositionInChunk = CubeChunkHelper.GetIndexFromCoordinates(cube.Coordinates);

			tookChunk[cubePositionInChunk] = cube.Color;
		}

		public void BreakCube(CubeCoordinates coordinates)
		{
			var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(coordinates);

			var tookChunk = TakeChunkAt(chunkCoordinates);

			int cubePositionInChunk = CubeChunkHelper.GetIndexFromCoordinates(coordinates);

			tookChunk[cubePositionInChunk] = null;
		}

		public CubeColor GetCubeColorAt(CubeCoordinates coordinates) // TODO: Redundance fix
		{
			var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(coordinates);

			var tookChunk = TakeChunkAt(chunkCoordinates);

			int cubePositionInChunk = CubeChunkHelper.GetIndexFromCoordinates(coordinates);

			return tookChunk[cubePositionInChunk];
		}

		public void PreviewCube(Cube cube)
		{
			PreviewableCube = cube;
		}

		public bool IsFreeSpace(CubeCoordinates coordinates) // TO-OPTIMIZE
		{
			var chunkCoordinates = CubeChunkHelper.FindBaseCoordinates(coordinates);

			var gotChunk = (from chunk in LoadedChunks
							where chunk.Coordinates == chunkCoordinates
							select chunk).FirstOrDefault();

			if (gotChunk == null) // don't try to check in a not loaded chunk, or it will crash
				return false;

			int cubePositionInChunk = CubeChunkHelper.GetIndexFromCoordinates(coordinates);

			if (!(gotChunk[cubePositionInChunk] == null))
				return false;

			return true;
		}

		public void LoadChunk(CubeCoordinates chunkCoordinates)
		{
			var gotChunk = GetChunkAt(chunkCoordinates);
			if (gotChunk != null)
				LoadedChunks.Add(gotChunk);
			else
				LoadedChunks.Add(new CubeChunk(chunkCoordinates)); // TODO: ForceGetChunkAt
		}

		public void UnloadChunk(CubeChunk chunk)
		{
			TrySetChunk(chunk);
			LoadedChunks.Remove(chunk);
		}

		public CubeChunk TakeChunkAt(CubeCoordinates chunkCoordinates)
		{
			return (from chunk in LoadedChunks
					where chunk.Coordinates == chunkCoordinates
					select chunk).FirstOrDefault();
		}

		public Vector3 GetGraphicsCubePosition(CubeCoordinates cubePosition) // cube position in graphics representation.
		{
			return cubePosition.ToVector3() / GetGraphicsCubeRatio();
		}
		// TMP
		public CubeCoordinates GetTruncatedCoordinatesFromGraphics(Vector3 position)
		{
			return CubeCoordinates.FromTruncated(position * GetGraphicsCubeRatio());
		}
		public CubeCoordinates GetCoordinatesFromGraphics(Vector3 position)
		{
			return new CubeCoordinates(position * GetGraphicsCubeRatio());
		}
		public float GetGraphicsCubeRatio() // how much is a cube smaller/bigger in the graphics representation?
		{
			return 1.0f / (Settings.HeightOfCubes * 2.0f);
		}

		private CubeChunk GetChunkAt(CubeCoordinates coordinates)
		{
			return Handler.GetChunkAt(coordinates);
		}

		private void TrySetChunk(CubeChunk chunk)
		{
			if (!chunk.IsEmpty() // optimized, if we try to set an empty chunk it will not really write it
				|| Handler.ChunkExistsAt(chunk.Coordinates)) // if the chunk already exists, set it anyway because it would not be overwrote
			{
				Handler.SetChunk(chunk);
			}
		}
	}
}