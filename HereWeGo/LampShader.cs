using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;
using OpenTK;

namespace HereWeGo
{
    class LampShader : Shader
    {
        private const string VSFile = "shader.vert", FSFile = "shader.frag";
        private const string ModelName = "model", ViewName = "view", ProjectionName = "projection"; // TO-DO: Create A Dictionary for these values
        private readonly int modelLocation, viewLocation, projectionLocation;

        public const int VertexPositionLocation = 0; // Cube Position Attrib Location in the Cube Shader

        public LampShader() : base(VSFile, FSFile)
        {
            modelLocation = GL.GetUniformLocation(Program, ModelName);
            viewLocation = GL.GetUniformLocation(Program, ViewName);
            projectionLocation = GL.GetUniformLocation(Program, ProjectionName);
        }

        public void SetModelUniform(Matrix4 model) =>
            GL.UniformMatrix4(modelLocation, false, ref model);
        public void SetViewUniform(Matrix4 view) =>
            GL.UniformMatrix4(viewLocation, false, ref view);
        public void SetProjectionUniform(Matrix4 projection) =>
            GL.UniformMatrix4(projectionLocation, false, ref projection);
    }
}