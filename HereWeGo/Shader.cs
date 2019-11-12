using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;
using System.Text;

namespace HereWeGo
{
    abstract class Shader
    {
        protected const string ShadersDirectory = "../../Shaders/";

        public int Program { get; protected set; }

        protected Shader(string vertexFile, string fragmentFile)
        {
            string vertexSource = GetSourceFromPath(ShadersDirectory + vertexFile);
            string fragmentSource = GetSourceFromPath(ShadersDirectory + fragmentFile);
            Program = CreateProgram(vertexSource, fragmentSource);
        }

        public void Use()
        {
            GL.UseProgram(Program);
        }

        public void SetMatrix4Uniform(string uniformName, Matrix4 matrix)
        {
            int uniformLocation = GL.GetUniformLocation(Program, uniformName);
            GL.UniformMatrix4(uniformLocation, false, ref matrix);
        }

        public void SetColor4Uniform(string uniformName, Color4 color)
        {
            int uniformLocation = GL.GetUniformLocation(Program, uniformName);
            GL.Uniform4(uniformLocation, color);
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Program);

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            // I bypass it because don't know how to do otherwise, please find a solution whenever
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
            GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        }
        ~Shader()
        {
            GL.DeleteProgram(Program);
        }

        protected static int CreateProgram(string vertexSource, string fragmentSource)
        {
            int vertexShader, fragmentShader;

            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexSource);

            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentSource);

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
        protected static string GetSourceFromPath(string shaderPath)
        {
            string shaderSource;

            using (StreamReader reader = new StreamReader(shaderPath, Encoding.ASCII))
                shaderSource = reader.ReadToEnd();

            return shaderSource;
        }
    }
}