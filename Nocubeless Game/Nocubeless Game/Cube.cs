using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class Cube
    {
        public CubeCoordinate Position { get; set; }
        public Color Color { get; set; }

        public Cube(Color color, CubeCoordinate position)
        {
            Position = position;
            Color = color;
        }

        public static ModelMeshPart LoadModel(GraphicsDevice graphicsDevice)
        {
            const int primitiveCount = 36;

            VertexPositionNormal[] vertices = new VertexPositionNormal[]
            {
                new VertexPositionNormal(new Vector3(-1.0f, -1.0f,  1.0f), Vector3.Zero),
                new VertexPositionNormal(new Vector3(1.0f, -1.0f,  1.0f), Vector3.Zero),
                new VertexPositionNormal(new Vector3(1.0f,  1.0f,  1.0f), Vector3.Zero),
                new VertexPositionNormal(new Vector3(-1.0f,  1.0f,  1.0f), Vector3.Zero),
                // back
                new VertexPositionNormal(new Vector3(-1.0f, -1.0f, -1.0f), Vector3.Zero),
                new VertexPositionNormal(new Vector3(1.0f, -1.0f, -1.0f), Vector3.Zero),
                new VertexPositionNormal(new Vector3(1.0f,  1.0f, -1.0f), Vector3.Zero),
                new VertexPositionNormal(new Vector3(-1.0f,  1.0f, -1.0f), Vector3.Zero)
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

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormal), vertices.Length, BufferUsage.WriteOnly); ;
            //vertexBuffer.SetData(vertices);

            indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);

            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal = new Vector3(0, 0, 0);

            for (int i = 0; i < indices.Length / 3; i++)
            {
                Vector3 firstvec = vertices[indices[i * 3 + 1]].Position - vertices[indices[i * 3]].Position;
                Vector3 secondvec = vertices[indices[i * 3]].Position - vertices[indices[i * 3 + 2]].Position;
                Vector3 normal = Vector3.Cross(firstvec, secondvec);
                normal.Normalize();
                vertices[indices[i * 3]].Normal += normal;
                vertices[indices[i * 3 + 1]].Normal += normal;
                vertices[indices[i * 3 + 2]].Normal += normal;
            }

            vertexBuffer.SetData(vertices);

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
