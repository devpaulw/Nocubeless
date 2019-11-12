using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace HereWeGo
{
    class CubeModel : Model
    {
        protected override int VerticesCount => 36;

        protected override Vector3[] Vertices => new Vector3[]
        {
            new Vector3(-1.0f, -1.0f,  1.0f),
            new Vector3(1.0f, -1.0f,  1.0f),
            new Vector3(1.0f,  1.0f,  1.0f),
            new Vector3(-1.0f,  1.0f,  1.0f),
            // back
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(1.0f,  1.0f, -1.0f),
            new Vector3(-1.0f,  1.0f, -1.0f)
        };
        protected override short[] Indices => new short[]
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

        public LightingShader Shader { get; }

        public CubeModel()
        {
            Shader = new LightingShader();

            VertexBufferObject = GL.GenBuffer();
            SetupVertexBufferObject();

            IndexBufferObject = GL.GenBuffer();
            SetupIndexBufferObject();

            VertexArrayObject = GL.GenVertexArray();
            SetupVertexArrayObject(LightingShader.VertexPositionLocation);
        }

        //public void Draw()
        //{
        //    GL.BindVertexArray(VertexArrayObject);
        //    GL.DrawElements(BeginMode.Triangles, VerticesCount, DrawElementsType.UnsignedShort, 0);
        //}
    }
}
