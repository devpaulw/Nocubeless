using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;

namespace HereWeGo
{
    class LightingShader : Shader
    {
        private const string VSFile = "shader.vert", FSFile = "lighting.frag"; // TO-DO: not polymorphism, fix design
        private const string ModelName = "model", ViewName = "view", ProjectionName = "projection"; // TO-DO: Find a way to link these common variables for design
        private const string ColorName = "cubecolor", LightColorName = "lightcolor";
        public const int VertexPositionLocation = 0; // Cube Position Attrib Location in the Cube Shader

        private readonly int modelLocation, viewLocation, projectionLocation;
        private readonly int colorLocation, lightColorLocation;

        public LightingShader() : base(VSFile, FSFile)
        {
            modelLocation = GL.GetUniformLocation(Program, ModelName);
            viewLocation = GL.GetUniformLocation(Program, ViewName);
            projectionLocation = GL.GetUniformLocation(Program, ProjectionName);
            projectionLocation = GL.GetUniformLocation(Program, ProjectionName);
            colorLocation = GL.GetUniformLocation(Program, ColorName);
            lightColorLocation = GL.GetUniformLocation(Program, LightColorName);
        }

        public void SetModelUniform(Matrix4 model) =>
            GL.UniformMatrix4(modelLocation, false, ref model);
        public void SetViewUniform(Matrix4 view) =>
            GL.UniformMatrix4(viewLocation, false, ref view);
        public void SetProjectionUniform(Matrix4 projection) =>
            GL.UniformMatrix4(projectionLocation, false, ref projection);

        public void SetModelColorUniform(Color4 color) =>
            GL.Uniform4(colorLocation, color);
        public void SetLightColorUniform(Color4 color) =>
            GL.Uniform4(lightColorLocation, color);
    }
}
