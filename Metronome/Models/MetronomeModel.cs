using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Metronome.Models
{

    public class MetronomeModel
    {
        public System.Timers.Timer timer = new System.Timers.Timer();
        public int BPM;
        public int beats;
        public int beatCounter = 0;
        public int firstBeatSound;
        public int beatSound;
        public MetronomeModel() {
            BPM = 120;
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 60000 / BPM;
            beats = 4;
            firstBeatSound = 800;
            beatSound = 400;
        }
        public MetronomeModel(int bpm, int b, int fbs, int bs)
        {
            BPM = bpm;
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 60000 / BPM;
            beats = b;
            firstBeatSound = fbs;
            beatSound = bs;
        }
        public void UpdateBPM(int bpm)
        {
            BPM = bpm;
            timer.Interval = 60000 / BPM;
        }
        public void UpdateSound((int FBS,int BS) _sound) 
        { 
            firstBeatSound = _sound.FBS;
            beatSound = _sound.BS;
        }
        public void UpdateBeats(int b) => beats = b;
        public void TimerStart()
        {
            timer.Start();
        }
        public void TimerStop() {
            timer.Stop();
        }
        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (beatCounter == 0) System.Console.Beep(firstBeatSound, 300);
            else System.Console.Beep(beatSound, 300);
            beatCounter++;
            beatCounter = beatCounter % beats;

        }
    }


}
