using OpenTK.Graphics;

namespace HereWeGo
{
    struct Cube
    {
        public Color4 Color { get; set; }
        public Coordinate Position { get; set; }

        public Cube(Color4 color, Coordinate position)
        {
            Color = color;
            Position = position; 
        }

        // Deprecated :
        //public static Cube Zero {
        //    get {
        //        return new Cube(new Color4(0, 0, 0, 0),
        //            new Coordinate(0, 0, 0));
        //    }
        //}
    }
}