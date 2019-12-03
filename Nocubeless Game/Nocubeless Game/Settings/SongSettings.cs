using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocubeless
{
    class SongSettings
    {
        public bool MusicEnabled { get; set; }

        public static SongSettings Default {
            get {
                return new SongSettings
                {
                    MusicEnabled = false
                };
            }
        }
    }
}
