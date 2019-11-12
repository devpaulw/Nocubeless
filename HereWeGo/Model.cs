using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace HereWeGo
{
    abstract class Model
    {
        private const int VectorSize = 3; // Vector3 number of values
        private const VertexAttribPointerType VertexType = VertexAttribPointerType.Float;
        private const bool Normalized = false; // Vertices values are not 0;255
        private const int Stride = 12; // Vector3 Stride (Vector3.SizeInBytes equivalent)
        private const BeginMode DrawingMode = BeginMode.Triangles;
        private const DrawElementsType ElementsType = DrawElementsType.UnsignedShort;

        protected virtual int VerticesCount { get; }

        protected virtual Vector3[] Vertices { get; }
        protected virtual short[] Indices { get; }

        public int VertexArrayObject { get; protected set; }
        public int VertexBufferObject { get; protected set; }
        public int IndexBufferObject { get; protected set; }

        public virtual void Load() =>
            GL.BindVertexArray(VertexArrayObject);

        public virtual void Draw() =>
            GL.DrawElements(DrawingMode, VerticesCount, ElementsType, 0);

        public virtual void Unload() =>
            GL.BindVertexArray(0);

        protected void SetupVertexArrayObject(int vertexLocation)
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, // Attrib 
                VectorSize, // size : how many float values of the vec?
                VertexType,
                Normalized,
                Stride, // stride : how many bytes between vertices?
                0);
        }
        protected void SetupVertexBufferObject()
        {
            // Vertex Buffer Data Set-up
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer,
                Vertices.Length * Vector3.SizeInBytes, // size : how many bytes is the full vertices array?
                Vertices,
                BufferUsageHint.StaticDraw); // TO-DO: Send a constant, how to do that?
        }
        protected void SetupIndexBufferObject()
        {
            // Index Buffer Data Set-up
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer,
                Indices.Length * sizeof(short),
                Indices,
                BufferUsageHint.StaticDraw);
        }
    }
}
