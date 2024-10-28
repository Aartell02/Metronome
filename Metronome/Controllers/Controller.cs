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
            while (true)
            {
                _view.Display();
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        _model.BPM++;
                        break;
                    case ConsoleKey.DownArrow:
                        _model.BPM--;
                        break;
                    case ConsoleKey.Spacebar:
                        _model.IsRunning = !_model.IsRunning;
                        if (_model.IsRunning)
                        {
                            Task.Run(() => RunMetronom());
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
        private void RunMetronom()
        {
            while (_model.IsRunning)
            {

                int czasUderzenia = 60000 / _model.BPM;

                AnsiConsole.WriteLine("[bold blue]Tick[/]");

                Thread.Sleep(czasUderzenia);

                AnsiConsole.WriteLine("[bold gray]Tock[/]");

                Thread.Sleep(czasUderzenia);
            }
        }

    }
}
