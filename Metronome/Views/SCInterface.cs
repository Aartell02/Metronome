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
            string startstop = "Start";
            while (true)
            {
                AnsiConsole.Clear();
                var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title($"BPM: {_controller.GetBPM()}\nBeats: {_controller.GetBeats()}\nChoose option:")
                    .AddChoices($"{startstop}", "BPM", "Beats", "Settings", "Close"));
                if (menu == "BPM") _controller.SetBPM(OptionBPM(_controller.GetBPM()).Result);
                else if (menu == "Beats") _controller.SetBeats(OptionBeats(_controller.GetBeats()).Result);
                else if (menu == "Start")
                {
                    _controller.Timer_Start();
                    startstop = "Stop";
                }
                else if (menu == "Stop") 
                { 
                    _controller.Timer_Stop();
                    startstop = "Start";
                }
                else if (menu == "Settings") OptionSettings();
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
        public static async Task<int> OptionBeats(int beats)
        {
            int _beats = beats;
            AnsiConsole.Write("| Press Enter to save | Press Escape to leave |\n| Press Up/Down Arrow to increase/decrease | Press Right/Left Arrow to increase/decrease |\n");
            var panel = new Panel($"[bold]Current Beats:[/] {_beats}");
            panel.Expand();

            int finalBeats = await AnsiConsole.Live(panel).StartAsync(async ctx =>
            {
                ctx.UpdateTarget(panel);
                while (true)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (_beats < 8) _beats++;
                            break;
                        case ConsoleKey.DownArrow:
                            if (_beats > 1) _beats--;
                            break;
                        case ConsoleKey.Enter:
                            return _beats;
                        case ConsoleKey.Escape:
                            return beats;
                        default:
                            break;
                    }
                    panel = new Panel($"[bold]Current Beats:[/] {_beats}");
                    panel.Expand();
                    ctx.UpdateTarget(panel);
                }
            });
            return finalBeats;
        }
        public void OptionSettings()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices("Beat Sound", "Return"));
                if (menu == "Beat Sound") _controller.SetSound(Sound(_controller.GetSound()).Result);
                else if (menu == "Return") return;
            }
        }
        public static async Task<(int, int)> Sound((int,int) sound)
        {
            (int _FB, int _B) _t = sound;
            AnsiConsole.Write("| Press Enter to save | Press Escape to leave | Press Up/Down Arrow to increase/decrease |\n");
            var panel = new Panel($"[bold]Current First Beat Hz:[/] {_t._FB}\n[bold]Current Beat Hz:[/] {_t._B}");
            panel.Expand();
            (int _FB, int _B) tf = await AnsiConsole.Live(panel).StartAsync(async ctx =>
            {
                ctx.UpdateTarget(panel);
                while (true)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (_t._FB < 4000) _t._FB +=10;
                            break;

                        case ConsoleKey.LeftArrow:
                            if (_t._FB > 100) _t._FB -=10;
                            break;
                        case ConsoleKey.UpArrow:
                            if (_t._B < 4000) _t._B += 10;
                            break;
                        case ConsoleKey.DownArrow:
                            if (_t._B > 100) _t._B -= 10;
                            break;
                        case ConsoleKey.Enter:
                            return _t;
                        case ConsoleKey.Escape:
                            return sound;
                        default:
                            break;
                    }
                    panel = new Panel($"[bold]Current First Beat Hz:[/] {_t._FB}\n[bold]Current Beat Hz:[/] {_t._B}");
                    panel.Expand();
                    ctx.UpdateTarget(panel);
                }
            });
            return tf;
        }

        public static void ShowClosePrompt()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[red]Bye! [/] :waving_hand:");
        }
    }
}
