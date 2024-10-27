using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metronome.Classes
{
    public class Settings
    {
        private int bpm { get; set; } 
        private bool isRunning { get; set; }
        public Settings() { 
            this.bpm = 120;
            this.isRunning = false;
        }
    }
}
