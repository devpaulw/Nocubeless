using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SpydotNet.GLGraphics
{
    public class VertexBuffer
    {
        public int Id { get; private set; }
        public int Count { get; private set; }

        public VertexBuffer()
        {
            Id = GL.GenBuffer();
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Id);
        }

        public void SetData<T>(T[] data, BufferUsageHint hint) where T : struct
        {
            int size = BlittableValueType.StrideOf(data) * data.Length;

            Count = data.Length;

            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size), data, hint);
        }

        public void Render(PrimitiveType type, IndexBuffer indexBuffer, int count)
        {
            indexBuffer.Bind();

            GL.DrawElements(type, indexBuffer.Count, indexBuffer.Type, IntPtr.Zero);
        }
    }
}
