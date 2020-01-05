using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class Program
    {
        static void Main()
        {
            using (var app = new Nocubeless())
            {
                app.Run();
            }
        }
    }
}
