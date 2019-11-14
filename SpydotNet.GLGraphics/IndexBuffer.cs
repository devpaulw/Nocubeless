using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SpydotNet.GLGraphics
{
    public class IndexBuffer
    {
        public int Id { get; private set; }
        public DrawElementsType Type { get; set; }
        public int Count { get; private set; }

        public IndexBuffer(DrawElementsType type)
        {
            Id = GL.GenBuffer();

            Type = type;
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Id);
        }

        public void SetData<T>(T[] data, BufferUsageHint hint) where T : struct
        {
            Count = data.Length;

            Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(data.Length * GetSizeInBytes()), data, hint);
        }

        public int GetSizeInBytes()
        {
            switch (Type)
            {
                case DrawElementsType.UnsignedByte:
                    return 1;
                case DrawElementsType.UnsignedShort:
                    return 2;
                case DrawElementsType.UnsignedInt:
                    return 4;
            }

            return 0;
        }
    }
}
