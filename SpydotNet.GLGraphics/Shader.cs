using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace SpydotNet.GLGraphics
{
    public class Shader
    {
        public int Id { get; private set; }
        public ShaderType Type { get; private set; }
        public int Length { get; private set; }

        public Shader(ShaderType type, string code)
        {
            int len = 0;

            Id = GL.CreateShader(type);
            Type = type;

            GL.ShaderSource(Id, 1, new[] { code }, ref len);
            GL.CompileShader(Id);

            int success;
            GL.GetShader(Id, ShaderParameter.CompileStatus, out success);

            if (success == 0)
            {
                throw new Exception("Error copiling Shader:\n" + GL.GetShaderInfoLog(Id));
            }

            Length = len;
        }
    }
}
