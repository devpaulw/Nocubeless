using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThereWeGetAnEngine
{
    class CubeRenderer
    {
        private readonly GraphicsDevice _graphicsDevice; // Where to draw
        private readonly ModelMeshPart modelMeshPart; // Store rendering attributes

        public CubeRenderer(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
           modelMeshPart = LoadCubeModel(_graphicsDevice);
        }
        
        public void Draw(Camera camera, Color color, Effect effect)
        {
            //BasicEffect effect = new BasicEffect(_graphicsDevice);

            effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);
            effect.Parameters["View"].SetValue(camera.ViewMatrix);
            effect.Parameters["World"].SetValue(Matrix.CreateScale(0.1f) * camera.WorldMatrix);

            effect.Parameters["Color"].SetValue(color.ToVector3());

            //effect.LightingEnabled = false;
            //effect.VertexColorEnabled = true;

            //effect.Projection = camera.ProjectionMatrix;
            //effect.View = camera.ViewMatrix;
            //effect.World = Matrix.CreateScale(0.1f) * camera.WorldMatrix;

            //effect.Alpha = 1.0f;
            //effect.AmbientLightColor = color.ToVector3();
            //effect.DiffuseColor = color.ToVector3();
            //effect.EmissiveColor = color.ToVector3();
            //effect.FogColor = color.ToVector3();
            //effect.SpecularColor = color.ToVector3();



            _graphicsDevice.Clear(Color.Purple);

            _graphicsDevice.SetVertexBuffer(modelMeshPart.VertexBuffer);
            _graphicsDevice.Indices = modelMeshPart.IndexBuffer;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList,
                0,
                modelMeshPart.StartIndex,
                modelMeshPart.PrimitiveCount);
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
