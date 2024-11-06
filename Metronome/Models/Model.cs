﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Metronome.Models
{

    public class ModelMetronome
    {
        public System.Timers.Timer timer;
        public int BPM;
        public int beats;
        public int beatCounter;
        public (int firstBeatSound, int beatSound) sound;
        public ModelMetronome() {
            BPM = 120;
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 60000 / BPM;
            beats = 4;
            beatCounter = 0;
            sound.firstBeatSound = 800;
            sound.beatSound = 400;

        }
        public void UpdateBPM(int bpm)
        {
            BPM = bpm;
            timer.Interval = 60000 / BPM;
        }
        public void UpdateSound((int FBS,int BS) _sound) 
        { 
            sound.firstBeatSound = _sound.FBS;
            sound.beatSound = _sound.BS;
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
            if (beatCounter == 0) System.Console.Beep(sound.firstBeatSound, 300);
            else System.Console.Beep(sound.beatSound, 300);
            beatCounter++;
            beatCounter = beatCounter % beats;

        }
    }


}
