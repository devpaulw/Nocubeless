using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class GameSettings
    {
        public GraphicsSettings Graphics { get; set; }
        public SongSettings Song { get; set; }
        public InputKeySettings InputKeys { get; set; }
        public CameraSettings Camera { get; set; }
        public WorldSettings World { get; set; }
        public CubeHandlerSettings CubeHandler { get; set; }


        public static GameSettings Default {
            get {
                return new GameSettings
                {
                    Graphics = GraphicsSettings.Default,
                    Song = SongSettings.Default,
                    InputKeys = InputKeySettings.DefaultFrench,
                    Camera = CameraSettings.Default,
                    World = WorldSettings.Default,
                    CubeHandler = CubeHandlerSettings.Default
                };
            }
        }
    }
}
