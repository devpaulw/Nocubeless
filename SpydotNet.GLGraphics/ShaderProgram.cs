using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace SpydotNet.GLGraphics
{
    public class ShaderOpener
    {
        public static string ReadSource(string shaderPath)
        {
            string shaderSource;

            using (StreamReader reader = new StreamReader(shaderPath, Encoding.ASCII))
                shaderSource = reader.ReadToEnd();

            return shaderSource;
        }
    }

    public class ShaderProgram : ThreadBoundDisposable
    {
        public int Id { get; protected set; }

        public ShaderProgram(string vss, string fss)
        {
            Id = CreateProgram(vss, fss);
        }

        protected static int CreateProgram(string vss, string fss)
        {
            int vertexShader, fragmentShader;

            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vss);

            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fss);

            GL.CompileShader(vertexShader);

            string infoLogVert = GL.GetShaderInfoLog(vertexShader);
            if (!System.String.IsNullOrEmpty(infoLogVert))
                Console.WriteLine(infoLogVert);

            GL.CompileShader(fragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);

            if (!System.String.IsNullOrEmpty(infoLogFrag))
                Console.WriteLine(infoLogFrag);

            int program = GL.CreateProgram();

            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);

            GL.LinkProgram(program);

            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            return program;
        }

        public void Use()
        {
            GL.UseProgram(Program);
        }

        protected override void GCUnmanagedFinalize()
        {
            GL.DeleteProgram(Id);
        }
    }
}