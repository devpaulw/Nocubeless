﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    public static class Program
    {
        static void Main()
        {
            using (var app = new NocubelessApp())
            {
                app.Run();
            }
        }
    }
}
