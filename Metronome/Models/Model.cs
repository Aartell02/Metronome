using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metronome.Models
{
    public class Model
    {
        private int bPM;
        public int BPM {  get; set; }
        private bool isRunning;
        public bool IsRunning { get { return isRunning; } set { isRunning = value; } }
    }

}
