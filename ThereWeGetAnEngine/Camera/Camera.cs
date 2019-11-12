using OpenTK;
using System.Collections.Generic;

namespace ThereWeGetAnEngine
{
    public class Camera : ICamera
    {
        private float radiansFov = MathHelper.PiOver3; // Pi/3 or a 60 degrees Fov default value

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(radiansFov);
            set => radiansFov = MathHelper.DegreesToRadians(value);
        }
        public float AspectRatio { get; set; }
            = 1.0F;

        public Vector3 Position { get; set; }
             = Vector3.Zero;
        public Vector3 Front { get; set; }
             = -Vector3.UnitZ;
        public Vector3 Up { get; set; }
             = Vector3.UnitY;

        private List<GameComponent> gameComponentsObservers;

        public Camera(Scene sceneInSight)
        {

        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Front, Up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            const float zNear = 0.05f, zFar = 50f;
            return Matrix4.CreatePerspectiveFieldOfView(radiansFov,
                AspectRatio,
                zNear, zFar);
        }
    }
}
