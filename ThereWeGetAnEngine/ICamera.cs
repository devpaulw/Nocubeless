using OpenTK;

namespace ThereWeGetAnEngine
{
    public interface ICamera
    {
        float Fov { get; set; }
        float AspectRatio { get; set; }
        Vector3 Position { get; set; }
        Vector3 Front { get; set; }
        Vector3 Up { get; set; }
    }
}
