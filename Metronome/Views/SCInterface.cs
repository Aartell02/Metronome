using Spectre.Console;
using Metronome.Controllers;
using Metronome.Models;

namespace Metronome.Views
{
    public class SCInterface
    {
        private Model? _model;

        public SCInterface(Model model)
        {
            _model = model;
        }

        public void Display()
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine($"Tempo: {_model.BPM}");
            if (_model.IsRunning)
            {
                AnsiConsole.WriteLine("[green]Metronom działa[/]");
            }
            else
            {
                AnsiConsole.WriteLine("[red]Metronom zatrzymany[/]");
            }
        }
    }

}
