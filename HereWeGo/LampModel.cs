using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeGo
{
    class LampModel : Model
    {
        private Matrix4 modelTransformations;

        private Model _onModel;
        public Model OnModel {
            get {
                return _onModel;
            }
            set {
                _onModel = value;
                VertexBufferObject = _onModel.VertexBufferObject;
                IndexBufferObject = _onModel.IndexBufferObject;
                SetupVertexArrayObject(LampShader.VertexPositionLocation);
            }
        }

        public LampShader Shader { get; }

        public Vector3 Position { get; set; }
        public Color4 LightColor { get; set; }

        public LampModel(Model onModel, Color4 lightColor, Vector3 position)
        {
            LightColor = lightColor;
            Position = position;

            Shader = new LampShader(); 
            VertexArrayObject = GL.GenVertexArray();
            OnModel = onModel;

            modelTransformations = Matrix4.Identity;
            modelTransformations *= Matrix4.CreateScale(0.2f); // TO-DO: 0.2f float must be a constant elsewhere!
            modelTransformations *= Matrix4.CreateTranslation(Position);
        }

        public override void Draw()
        {
            OnModel.Draw();
        }

        public Matrix4 GetModelMatrix()
        {
            return modelTransformations;
        }
    }
}