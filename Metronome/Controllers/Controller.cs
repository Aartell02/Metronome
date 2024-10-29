using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metronome.Models;
using Metronome.Views;
using Spectre.Console;
namespace Metronome.Controllers
{
    public class Controller
    {
        private Model _model;
        private SCInterface _view;
        public Controller(Model model, SCInterface view) {
            this._model = model;
            this._view = view;
        }
        public void Start()
        {
            int bpm = _view.GetUserInputBPM();
            _model.BPM = bpm;

            Console.Clear();
            _view.ShowBPM(bpm);
            _model.Start();
            _view.ShowStartPrompt();
            _view.Display(bpm); 
        }
        public void Stop()
        {
            _model.Stop();
            _view.ShowStopPrompt();
        }

    }
}
