using System.Threading.Channels;
using System.Timers;
using Metronome.Models;
using Metronome.Views;
namespace Metronome.Controllers
{
    public class ViewController
    {
        private Model _model;
        private SCInterface _view;

        public ViewController(Model model, SCInterface view) {
            this._model = model;
            this._view = view;
            _model.timer.Elapsed += Timer_Elapsed;
        }
        public void Run() {
            _view.Display();

        }
        public void Timer_Start()
        {
            _model.timer.Start();
            Console.Clear();;
            
        }
        public void Timer_Stop()
        {
            _model.timer.Stop();
        }
        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            System.Console.Beep(500,300);
        }
        public void Close() {
            SCInterface.ShowClosePrompt();
        }
        public int GetBPM() => _model.BPM;

        public void SetBPM(int BPM) => _model.UpdateBPM(BPM); 

    }
}
