using System;
using System.Timers;
using System.Windows.Forms;
using MetronomeGraphic.Controllers;

namespace MetronomeGraphic.Models
{

    public class MetronomeModel
    {
        private System.Timers.Timer Timer { get; set; }
        public int BPM { get; set; }
        public int Beats { get; set; }
        public int BeatCounter { get; set; }
        public int FirstBeatSound { get; set; }
        public int BeatSound { get; set; }

        public event Action<bool> BeatOccurred;
        public MetronomeModel(Preset _preset)
        {
            this.Timer = new System.Timers.Timer();
            this.Timer.Elapsed += OnTimer_Elapsed;
            SetPreset(_preset);
        }
        public void SetPreset(Preset preset)
        {
            this.BPM = preset.Bpm;
            this.Beats = preset.Beats;
            this.FirstBeatSound = preset.Fbs;
            this.BeatSound = preset.Bs;
            UpdateTimerInterval();
        }

        public void UpdateBPM(int bpm)
        {
            this.BPM = bpm;
            UpdateTimerInterval();
        }
        public void UpdateSound((int FBS, int BS) _sound)
        {
            this.FirstBeatSound = _sound.FBS;
            this.BeatSound = _sound.BS;
        }
        public void UpdateBeats(int b) => this.Beats = b;
        public void TimerStart()
        {
            this.BeatCounter = 0;
            this.Timer.Start();
        }
        public void TimerStop() => this.Timer.Stop();
        public void OnTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool isFirstBeat = BeatCounter == 0;
            BeatOccurred?.Invoke(isFirstBeat);
            BeatCounter = (BeatCounter + 1) % Beats;

        }
        private void UpdateTimerInterval() => Timer.Interval = 60000 / BPM;
        public void Dispose() => Timer?.Dispose();
    }
}
