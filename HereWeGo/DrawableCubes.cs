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
    class DrawableCubes
    {
        private float _height;
        public float Height {
            get => _height;
            set {
                _height = value;
                Scale = Matrix4.CreateScale(value);
            }
        }

        private readonly List<Cube> toDraw;

        private readonly CubeModel cubeModel;
        #region Model Transform
        public static Matrix4 Translation { get; set; } = Matrix4.Identity;
        public static Matrix4 Scale { get; set; } = Matrix4.Identity;
        #endregion

        private readonly LampModel lampModel;

        public Camera PlayerCamera { get; set; }

        public DrawableCubes(float height, Camera playerCamera)
        {
            Height = height;
            PlayerCamera = playerCamera;

            toDraw = new List<Cube>(); // Init the ToDraw list
            cubeModel = new CubeModel();
            lampModel = new LampModel(cubeModel,
                new Color4(0.25f, 0.25f, 0.25f, 1.0f),
                new Vector3(1.2f, 1.0f, 2.0f)); // TO-DO: It's Temporary, Create a Lamp Children with preconfigured lamp lighting, GL
        }

        public void Add(Cube cube)
        {
            toDraw.Add(cube);
        }

        public void DrawThem()
        {
            

            foreach (Cube cubeToDraw in toDraw)
            {
                cubeModel.Shader.Use();
                Translation = cubeToDraw.Position.CreateSceneTranslation(_height);
                Matrix4 model = Scale * Translation;
                cubeModel.Shader.SetModelUniform(model); // TODO: Perhaps store the model somewhere and especially view to send that one time we don't care to send a constant each frame.
                cubeModel.Shader.SetModelColorUniform(cubeToDraw.Color);
                cubeModel.Shader.SetViewUniform(PlayerCamera.GetViewMatrix());
                cubeModel.Shader.SetProjectionUniform(PlayerCamera.GetProjectionMatrix());
                cubeModel.Shader.SetLightColorUniform(lampModel.LightColor); // WTF: WHAT THE BLAZES IS THAT, Why a model can give us this var?
                cubeModel.Load();
                cubeModel.Draw();

                //Lamp.LightUp(cubeModel, camera); : PSEUDO CODE
                lampModel.Shader.Use();
                lampModel.Shader.SetModelUniform(lampModel.GetModelMatrix()); // TODO: Find a solution to set that is the common shader, with also
                lampModel.Shader.SetViewUniform(PlayerCamera.GetViewMatrix());
                lampModel.Shader.SetProjectionUniform(PlayerCamera.GetProjectionMatrix());
                lampModel.Load();
                lampModel.Draw();
            }
        }

        public void Dispose()
        {
            cubeModel.Shader.Dispose();
            lampModel.Shader.Dispose();
        }
    }
}
