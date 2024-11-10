using System.Threading.Channels;
using System.Timers;
using Metronome.Models;
using Metronome.Views;
namespace Metronome.Controllers
{
    public class ViewController
    {
        private MetronomeModel _model;
        private SCInterface _view;
        private Presets _presets;

        public ViewController(MetronomeModel model, SCInterface view, Presets presets) {
            this._model = model;
            this._view = view;
            this._presets = presets;
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
        public static void Close()
        {
            SCInterface.ShowClosePrompt();
        }
    }
}
