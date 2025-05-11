using MetronomeGraphic.Models;
using MetronomeGraphic.Views;
using MetronomeGraphic.Services;
using System.Timers;
using System.Windows.Forms;
namespace MetronomeGraphic.Controllers
{
    public class ViewController
    {
        private MetronomeModel model;
        private ActivityView view; 
        private PresetRepositoryModel presets;
        private TunerModel tuner;
        private AudioInputService audioService;
        public ViewController(MetronomeModel model, PresetRepositoryModel presets) {
            this.model = model;
            this.presets = presets;
            this.tuner = new TunerModel();
            this.audioService = new AudioInputService(tuner);
            model.BeatOccurred += isFirst => MetronomBeep(isFirst);
        }
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(this.view = new ActivityView(this));
        }
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
            view.UpdateBeatDots(); 
            if (model.BeatCounter == 0) System.Console.Beep(model.FirstBeatSound, 300);
            else System.Console.Beep(model.BeatSound, 300);

        }
        //PRESETS
        public bool AddPreset(string name) => presets.AddPreset(new Preset(name, model.BPM, model.Beats, model.FirstBeatSound, model.BeatSound));
        public PresetRepositoryModel GetPresets() => presets;
        public Preset GetPresetByName(string name) => presets.GetByName(name);
        public void LoadPreset(string name) => model.SetPreset(presets.GetByName(name));
        public void DeletePreset(string name) => presets.Remove(presets.GetByName(name));
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
        public int GetCurrentBeat() => model.BeatCounter;
        
    }
}
