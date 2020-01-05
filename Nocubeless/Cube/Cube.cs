using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
	class Cube
	{
		public static float size = 0.01f;
		public WorldCoordinates Coordinates { get; set; }
		// Maybe but I'm not sure because it's a conflict with the floating Position
		// BBMSG ANSWER I think we should have a convention in our project by naming all coordinates of the world WorldPosition (or something like that you can propose another name) and all graphics coordinates something like GraphicsCoordinates or ScreenCoordinates
		// BBMSG what's the real difference between WorldCoordinates and Vector3, why having a integer coordinates is important ? (is that to guarantee greater limits ?),
		// because i'm thinking if it would be better to not move the position of the camera anymore and let cubes be translated to sscreen coordinates based on the position of the Player class
		public CubeColor Color { get; set; }


		public Cube(CubeColor color, WorldCoordinates position)
		{
			Coordinates = position;
			Color = color;
		}

		public static ModelMeshPart LoadModel(GraphicsDevice graphicsDevice)
		{
			const int primitiveCount = 36;

			VertexPosition[] vertices = new VertexPosition[]
			{
				new VertexPosition(new Vector3(-1.0f, -1.0f,  1.0f)),
				new VertexPosition(new Vector3(1.0f, -1.0f,  1.0f)),
				new VertexPosition(new Vector3(1.0f,  1.0f,  1.0f)),
				new VertexPosition(new Vector3(-1.0f,  1.0f,  1.0f)),
                // back
                new VertexPosition(new Vector3(-1.0f, -1.0f, -1.0f)),
				new VertexPosition(new Vector3(1.0f, -1.0f, -1.0f)),
				new VertexPosition(new Vector3(1.0f,  1.0f, -1.0f)),
				new VertexPosition(new Vector3(-1.0f,  1.0f, -1.0f))
			};
			short[] indices = new short[primitiveCount]
			{
                // front
                0, 1, 2,
				2, 3, 0,
                // right
                1, 5, 6,
				6, 2, 1,
                // back
                7, 6, 5,
				5, 4, 7,
                // left
                4, 0, 3,
				3, 7, 4,
                // bottom
                4, 5, 1,
				1, 0, 4,
                // top
                3, 2, 6,
				6, 7, 3
			};

			VertexBuffer vertexBuffer;
			IndexBuffer indexBuffer;

			ModelMeshPart modelMeshPart;

			vertexBuffer = new VertexBuffer(graphicsDevice, VertexPosition.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly); ;
			vertexBuffer.SetData(vertices);

			indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
			indexBuffer.SetData(indices);

			modelMeshPart = new ModelMeshPart()
			{
				IndexBuffer = indexBuffer,
				PrimitiveCount = primitiveCount,
				StartIndex = 0,
				VertexBuffer = vertexBuffer
			};

			return modelMeshPart;
		}
	}
}
