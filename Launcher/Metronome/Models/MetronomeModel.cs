using System.Timers;

namespace Metronome.Models
{

    public class MetronomeModel
    {
        public System.Timers.Timer Timer { get; set; }
        public int BPM { get; set; }
        public int Beats { get; set; }
        public int BeatCounter { get; set; }
        public int FirstBeatSound { get; set; }
        public int BeatSound { get; set; }
        public MetronomeModel(Preset _preset) 
        {
            this.Timer = new System.Timers.Timer();
            this.Timer.Elapsed += Timer_Elapsed;
            SetPreset(_preset);
        }
        public void SetPreset(Preset preset)
        {
            this.BPM = preset.Bpm;
            this.Timer.Interval = 60000 / this.BPM;
            this.Beats = preset.Beats;
            this.FirstBeatSound = preset.Fbs;
            this.BeatSound = preset.Bs; 
        }

        public void UpdateBPM(int bpm)
        {
            this.BPM = bpm;
            this.Timer.Interval = 60000 / this.BPM;
        }
        public void UpdateSound((int FBS,int BS) _sound) 
        {
            this.FirstBeatSound = _sound.FBS;
            this.BeatSound = _sound.BS;
        }
        public void UpdateBeats(int b) => this.Beats = b;
        public void TimerStart()
        {
            this.Timer.Start();
        }
        public void TimerStop() {
            this.Timer.Stop();
        }
        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.BeatCounter == 0) System.Console.Beep(this.FirstBeatSound, 300);
            else System.Console.Beep(this.BeatSound, 300);
            this.BeatCounter++;
            this.BeatCounter = this.BeatCounter % this.Beats;

        }
    }


}
