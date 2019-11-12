using OpenTK;
using OpenTK.Graphics;

namespace HereWeGo
{
    class Scene
    {
        private readonly DrawableCubes cubes;

        public Camera PlayerCamera { get; set; }

        public Scene(ref Camera playerCamera, float cubesHeight)
        {
            PlayerCamera = playerCamera;

            cubes = new DrawableCubes(cubesHeight, PlayerCamera);
        }

        public void LayCube(Cube newCube)
        {
            cubes.Add(newCube);
        }

        public void Draw()
        {
            cubes.DrawThem();
        }

        public void Dispose()
        {
            cubes.Dispose();
        }
    }
}