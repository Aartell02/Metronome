using Metronome.Models;
using Metronome.Views;
namespace Metronome.Controllers
{
    public class ConsoleViewController
    {
        private MetronomeModel _model;
        private ConsoleView _view;
        private PresetRepository _presets;
        private TunerModel _tuner;
        public ConsoleViewController(MetronomeModel model, ConsoleView view, PresetRepository presets) {
            this._model = model;
            this._view = view;
            this._presets = presets;
            this._tuner = new TunerModel();
        }
        public void Run() => _view.Display();
        public void Timer_Start() => _model.TimerStart();
        public void Timer_Stop() => _model.TimerStop();
        public void SetBBPM((int bpm, int beats) t)
        {
            SetBPM(t.bpm);
            SetBeats(t.beats);
        }
        public int GetBPM() => _model.BPM;
        public void SetBPM(int BPM) => _model.UpdateBPM(BPM);
        public int GetBeats() => _model.Beats;
        public void SetBeats(int beats) => _model.UpdateBeats(beats);
        public (int, int) GetSound() => (_model.FirstBeatSound, _model.BeatSound);
        public void SetSound((int, int) sound) => _model.UpdateSound(sound);
        public bool AddPreset(string name) => _presets.AddPreset(new Preset(name, _model.BPM, _model.Beats, _model.FirstBeatSound, _model.BeatSound));
        public PresetRepository GetPresets() => _presets;
        public Preset GetPresetByName(string name) => _presets.GetByName(name);
        public void LoadPreset(string name) => _model.SetPreset(_presets.GetByName(name));
        public void DeletePreset(string name)
        {
            var preset = _presets.GetByName(name);
            if (preset != null)
                _presets.Remove(preset);
        }
        public void OverwritePreset(string name)
        {
            DeletePreset(name);
            AddPreset(name);
        }
        //TUNER
        public void StartTuner() => _tuner.Start();
        public void StopTuner() => _tuner.Stop();
        public string GetClosestString() => _tuner.ClosestString;
        public float GetDetectedFrequency() => _tuner.DetectedFrequency;
        public float GetClosestFrequency() => _tuner.ClosestFrequency;
        public static void Close() => ConsoleView.ShowClosePrompt();
    }
}
