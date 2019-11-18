﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public class Cube
    {
        public Coordinate Position { get; set; }
        public Color Color { get; set; } 

        public Cube(Color color, Coordinate position)
        {
            Position = position;
            Color = color;
        }
    }
}
