using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metronome.Models
{

    public class Model
    {
        public int BPM = 120;

        public bool IsRunning = false;
        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }


}
