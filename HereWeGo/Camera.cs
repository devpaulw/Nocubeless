using OpenTK;

namespace HereWeGo
{
    public partial class Camera
    {
        private float _fov = MathHelper.PiOver3;

        public float Fov {
            get => MathHelper.RadiansToDegrees(_fov);
            set => _fov = MathHelper.DegreesToRadians(value);
        }
        public float AspectRatio { get; set; }

        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;

        public Vector3 Position { get; set; }
        public Vector3 Front => _front;
        public Vector3 Up => _up;

        public Camera(Vector3 position, float aspectRatio)
        {
            AspectRatio = aspectRatio;
            Position = position;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Front, Up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            const float zNear = 0.05f, zFar = 50f;
            return Matrix4.CreatePerspectiveFieldOfView(_fov,
                AspectRatio,
                zNear, zFar);
        }
    }
}
