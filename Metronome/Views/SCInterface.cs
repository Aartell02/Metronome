using Spectre.Console;
using Metronome.Controllers;
using System;
using System.Reflection.PortableExecutable;
using System.Media;
using System.Diagnostics;
using System.Timers;
using System.Data;
using System.Threading.Channels;

namespace Metronome.Views
{
    public class SCInterface
    {
        private ViewController _controller;
        public void SetController(ViewController controller)
        {
            _controller = controller;
        }
        public void Display()
        {
            while (true)
            {
                AnsiConsole.Clear();

                var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Wybierz opcję:")
                    .AddChoices("BPM", "Start", "Stop", "Close"));
                if (menu == "BPM") _controller.SetBPM(OptionBPM(_controller.GetBPM()).Result);
                else if (menu == "Start")
                {
                    _controller.Timer_Start();
                }
                else if (menu == "Stop")
                {
                    _controller.Timer_Stop();
                    OptionClose();
                }
                else if (menu == "Close") return;
            }
        }
        public static async Task<int> OptionBPM(int bpm)
        {
            int _bpm = bpm;
            AnsiConsole.Write("| Press Enter to save | Press Escape to leave | Press Up/Down Arrow to increase/decrease |\n");
            var panel = new Panel($"[bold]Current BPM:[/] {_bpm}");
            panel.Expand();

            int finalBpm = await AnsiConsole.Live(panel).StartAsync(async ctx =>
            {
                ctx.UpdateTarget(panel);
                while (true)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (_bpm < 240) _bpm++;
                            break;

                        case ConsoleKey.DownArrow:
                            if (_bpm > 20) _bpm--;
                            break;

                        case ConsoleKey.Enter:
                            return _bpm;
                        case ConsoleKey.Escape:
                            return bpm;
                        default:
                            break;
                    }
                    panel = new Panel($"[bold]Current BPM:[/] {_bpm}");
                    panel.Expand();
                    ctx.UpdateTarget(panel);
                }
            });
            return finalBpm;
        }
        public static void OptionClose() { }


        public static void ShowClosePrompt()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[red]Bye! [/] :waving_hand:");
        }


        
    }
}
