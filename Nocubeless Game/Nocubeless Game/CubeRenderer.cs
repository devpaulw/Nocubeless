using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class CubeRenderer // TODO: Deprecate that class, use World (WITH CubeComposite) and cube classes, you'll see that works!
    {
        private readonly ModelMeshPart cubeMeshPart; // Store rendering attributes

        private GraphicsDevice GraphicsDevice { get; } // Where to draw
        private CubeEffect CubeEffect { get; }

        public float Height { get; set; }

        public CubeRenderer(GraphicsDevice graphicsDevice, CubeEffect cubeEffect, float cubesHeight)
        {
            GraphicsDevice = graphicsDevice;
            CubeEffect = cubeEffect;

            Height = cubesHeight;

           cubeMeshPart = LoadCubeModel(GraphicsDevice);
        }
        
        public void Draw(Camera camera, Cube cube)
        {
            Matrix scale = Matrix.CreateScale(Height);
            Matrix translation = Matrix.CreateTranslation(0.2f * cube.Position.X, 
                0.2f * cube.Position.Y, 
                0.2f * cube.Position.Z); // TEMP! We're waiting for CubeRenderer new organization.

            CubeEffect.World = scale * translation;
            CubeEffect.View = camera.ViewMatrix;
            CubeEffect.Projection = camera.ProjectionMatrix;
            CubeEffect.Color = cube.Color;

            GraphicsDevice.SetVertexBuffer(cubeMeshPart.VertexBuffer);
            GraphicsDevice.Indices = cubeMeshPart.IndexBuffer;

            foreach (EffectPass pass in CubeEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList,
                0,
                cubeMeshPart.StartIndex,
                cubeMeshPart.PrimitiveCount);
            }
        }

        private static ModelMeshPart LoadCubeModel(GraphicsDevice graphicsDevice)
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

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPosition), vertices.Length, BufferUsage.WriteOnly); ;
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
