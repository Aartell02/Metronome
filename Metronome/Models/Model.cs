using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Metronome.Models
{

    public class Model
    {
        public System.Timers.Timer timer;
        public int BPM;

        public bool IsRunning = false;
        public Model() {
            timer = new System.Timers.Timer();
            BPM = 120;
            timer.Interval = 60000 / BPM;
        }
        public void Start()
        {
            IsRunning = true;
            
        }

        public void Stop()
        {
            IsRunning = false;
        }
        public void UpdateBPM(int bpm)
        {
            BPM = bpm;
            timer.Interval = 60000 / BPM;
        }
    }


}
