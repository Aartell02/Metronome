using Spectre.Console;
using Metronome.Controllers;
using Metronome.Models;
using System;

namespace Metronome.Views
{
    public class SCInterface
    {
        
        public void Display(int BPM)
        {

            int metrum1 = 4;
            int metrum2 = 4;
            var table = new Table().Centered();
            var layout = new Layout("Root").SplitRows(
                new Layout("Top"));

                while (true)
            {

                var startStop = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Wybierz opcję:")
                    .AddChoices("Start", "Stop"));

                if (startStop == "Start")
                {
                    AnsiConsole.MarkupLine("[yellow]![/]");

                    AnsiConsole.Live(layout).Start(ctx =>
                    {
                        while (true)
                        {
                            var canvas = new Canvas(metrum1 * 2 - 1, 1);
                            layout["Top"].Update(new Panel(
                            Align.Center(
                                canvas)).Expand());
                            for (int i = 0; i < metrum1 * 2 - 1; i += 2)
                            {

                                canvas.SetPixel(i, 0, Color.White);
                                ctx.Refresh();
                                if (i == 0) Console.Beep(1500, 200);
                                else Console.Beep(1000, 200);
                                Thread.Sleep(60000 / BPM);
                            }
                        }
                    });

                }
                else if (startStop == "Stop")
                {
                    AnsiConsole.MarkupLine("[yellow]![/]");
                }

            }
        }
        public void ShowStartPrompt()
        {
            AnsiConsole.MarkupLine("[green]Metronom został uruchomiony.[/]");
        }
        public void ShowStopPrompt()
        {
            AnsiConsole.MarkupLine("[red]Metronom został zatrzymany.[/]");
        }

        public void ShowBPM(int bpm)
        {
            AnsiConsole.MarkupLine($"BPM: [yellow]{bpm}[/]");
        }

        public int GetUserInputBPM()
        {
            return AnsiConsole.Ask<int>("Podaj BPM (liczba całkowita): ");
        }
    }

}
