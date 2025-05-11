using System.Timers;
using System.Windows.Forms;
using MetronomeGraphic.Controllers;

namespace MetronomeGraphic.Models
{

    public class MetronomeModel
    {
        public System.Timers.Timer timer = new System.Timers.Timer();
        public int BPM;
        public int beats;
        public int beatCounter = 0;
        public int firstBeatSound;
        public int beatSound;
        public MetronomeModel(Preset _preset)
        {
            SetPreset(_preset);
        }
        public void SetPreset(Preset _preset)
        {
            this.BPM = _preset.bpm;
            this.timer.Interval = 60000 / this.BPM;
            this.beats = _preset.beats;
            this.firstBeatSound = _preset.fbs;
            this.beatSound = _preset.bs;
        }

        public void UpdateBPM(int bpm)
        {
            this.BPM = bpm;
            this.timer.Interval = 60000 / this.BPM;
        }
        public void UpdateSound((int FBS, int BS) _sound)
        {
            this.firstBeatSound = _sound.FBS;
            this.beatSound = _sound.BS;
        }
        public void UpdateBeats(int b) => this.beats = b;
        public void TimerStart()
        {
            this.timer.Start();
        }
        public void TimerStop()
        {
            this.timer.Stop();
        }
    }
}