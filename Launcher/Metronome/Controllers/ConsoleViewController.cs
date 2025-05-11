using Metronome.Models;
using Metronome.Services;
using Metronome.Views;
namespace Metronome.Controllers
{
    public class ConsoleViewController
    {
        private MetronomeModel model;
        private ConsoleView view;
        private PresetRepositoryModel presets;
        private TunerModel tuner;
        private AudioInputService audioService;
        public ConsoleViewController(MetronomeModel model, ConsoleView view, PresetRepositoryModel presets) {
            this.model = model;
            this.view = view;
            this.presets = presets;
            this.tuner = new TunerModel();
            this.audioService = new AudioInputService(tuner);
            model.BeatOccurred += isFirst => MetronomBeep(isFirst);
 
        }
        public void Run() => view.Display();
        //METRONOME
        public void Timer_Start() => model.TimerStart();
        public void Timer_Stop() => model.TimerStop();
        public void SetBBPM((int bpm, int beats) t)
        {
            SetBPM(t.bpm);
            SetBeats(t.beats);
        }
        public int GetBPM() => model.BPM;
        public void SetBPM(int BPM) => model.UpdateBPM(BPM);
        public int GetBeats() => model.Beats;
        public void SetBeats(int beats) => model.UpdateBeats(beats);
        public (int, int) GetSound() => (model.FirstBeatSound, model.BeatSound);
        public void SetSound((int, int) sound) => model.UpdateSound(sound);
        private void MetronomBeep(bool isFirst)
        {
            int freq = isFirst ? model.FirstBeatSound : model.BeatSound;
            Console.Beep(freq, 300);
        }

        //PRESET
        public bool AddPreset(string name) => presets.AddPreset(new Preset(name, model.BPM, model.Beats, model.FirstBeatSound, model.BeatSound));
        public PresetRepositoryModel GetPresets() => presets;
        public Preset GetPresetByName(string name) => presets.GetByName(name);
        public void LoadPreset(string name) => model.SetPreset(presets.GetByName(name));
        public void DeletePreset(string name)
        {
            var preset = presets.GetByName(name);
            if (preset != null)
                presets.Remove(preset);
        }
        public void OverwritePreset(string name)
        {
            DeletePreset(name);
            AddPreset(name);
        }
        //TUNER
        public void StartTuner() => audioService.Start();
        public void StopTuner() => audioService.Stop();
        public string GetClosestString() => tuner.ClosestString;
        public float GetDetectedFrequency() => tuner.DetectedFrequency;
        public float GetClosestFrequency() => tuner.ClosestFrequency;
        //EXIT
        public static void Close() => ConsoleView.ShowClosePrompt();
    }
}
