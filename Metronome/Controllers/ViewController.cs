using System.Threading.Channels;
using System.Timers;
using Metronome.Models;
using Metronome.Views;
namespace Metronome.Controllers
{
    public class ViewController
    {
        private ModelMetronome _model;
        private SCInterface _view;

        public ViewController(ModelMetronome model, SCInterface view) {
            this._model = model;
            this._view = view;
        }
        public void Run() => _view.Display();
        public void Timer_Start() => _model.TimerStart();
        public void Timer_Stop() => _model.TimerStop();
        public int GetBPM() => _model.BPM;
        public int GetBeats() => _model.beats;
        public (int,int) GetSound() => _model.sound;
        public void SetBPM(int BPM) => _model.UpdateBPM(BPM);
        public void SetBeats(int beats) => _model.UpdateBeats(beats);
        public void SetSound((int, int) sound) => _model.UpdateSound(sound);
        public static void Close() => SCInterface.ShowClosePrompt();
    }
}
