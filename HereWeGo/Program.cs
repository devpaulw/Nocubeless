namespace HereWeGo
{
    class Program
    {
        static void Main()
        {
            int width = 1280, height = 720;
            string title = "Nocubeless";
            double framerate = 200.0d;
            using (Game game = new Game(width, height, title, false))
            {
                game.VSync = OpenTK.VSyncMode.Off; // Ugly otherwise, find a solution whenever
                game.Run(framerate);
            }
        }
    }
}