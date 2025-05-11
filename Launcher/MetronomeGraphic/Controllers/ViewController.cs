using MetronomeGraphic.Models;
using MetronomeGraphic.Views;
using NAudio.Wave;
using System.Threading;
using System;
using System.Timers;
using System.Windows.Forms;
namespace MetronomeGraphic.Controllers
{
    public class ViewController
    {
        private MetronomeModel _model;
        private ActivityView _view; //zmiana zmiennej
        private Presets _presets;
        private GuitarTuner _tuner;
        public ViewController(MetronomeModel model, Presets presets) {
            this._model = model;
            this._presets = presets;
            this._tuner = new GuitarTuner();
            _model.timer.Elapsed += Timer_Elapsed;
        }
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(this._view = new ActivityView(this));
        }
        public void Timer_Start() {
            _model.TimerStart();
         }
        public void Timer_Stop() => _model.TimerStop();
        public void SetBBPM((int bpm, int beats) t)
        {
            SetBPM(t.bpm);
            SetBeats(t.beats);
        }
        public int GetBPM() => _model.BPM;
        public void SetBPM(int BPM) => _model.UpdateBPM(BPM);
        public int GetBeats() => _model.beats;
        public void SetBeats(int beats) => _model.UpdateBeats(beats);
        public (int, int) GetSound() => (_model.firstBeatSound, _model.beatSound);
        public void SetSound((int, int) sound) => _model.UpdateSound(sound);
        public bool AddPreset(string name) => _presets.AddPreset(new Preset(name, _model.BPM, _model.beats, _model.firstBeatSound, _model.beatSound));
        public Presets GetPresets() => _presets;
        public Preset GetPresetByName(string name) => _presets.GetByName(name);
        public void LoadPreset(string name) => _model.SetPreset(_presets.GetByName(name));
        public void DeletePreset(string name) => _presets.Remove(_presets.GetByName(name));
        public void OverwritePreset(string name)
        {
            DeletePreset(name);
            AddPreset(name);
        }
        public void StartTuner() => _tuner.Start();
        public void StopTuner() => _tuner.Stop();
        public string GetClosestString() => _tuner.ClosestString;
        public float GetDetectedFrequency() => _tuner.DetectedFrequency;
        public float GetClosestFrequency() => _tuner.ClosestFrequency;
        //public static void Close() => _view.ShowClosePrompt(); // niepotrzebne
        ////
        public int GetCurrentBeat() => _model.beatCounter;
        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _view.UpdateBeatDots(); // Animacja
            if (_model.beatCounter == 0) System.Console.Beep(_model.firstBeatSound, 300);
            else System.Console.Beep(_model.beatSound, 300);
            _model.beatCounter++;
            _model.beatCounter = _model.beatCounter % _model.beats;

        }
    }
}
