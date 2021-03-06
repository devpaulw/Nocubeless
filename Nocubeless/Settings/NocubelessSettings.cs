﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    internal class NocubelessSettings
    {
        public GraphicsSettings Graphics { get; set; }
        public SongSettings Song { get; set; }
        public InputKeySettings Keys { get; set; }
        public CameraSettings Camera { get; set; }
        public CubeWorldSettings CubeWorld { get; set; }
        public CubeHandlerSettings CubeHandler { get; set; }
        public EditingModeSettings EditingMode { get; set; }


        public static NocubelessSettings Default {
            get {
                return new NocubelessSettings
                {
                    Graphics = GraphicsSettings.Default,
                    Song = SongSettings.Default,
                    Keys = InputKeySettings.DefaultFrench,
                    Camera = CameraSettings.Default,
                    CubeWorld = CubeWorldSettings.Default,
                    CubeHandler = CubeHandlerSettings.Default,
                    EditingMode = EditingModeSettings.Default
                };
            }
        }
    }
}
