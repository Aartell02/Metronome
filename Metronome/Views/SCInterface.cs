using Spectre.Console;
using Metronome.Controllers;
using System;
using System.Reflection.PortableExecutable;
using System.Media;
using System.Diagnostics;
using System.Timers;
using System.Data;
using System.Threading.Channels;
using static System.Net.Mime.MediaTypeNames;

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
                    .AddChoices($"{startstop}", "BPM", "Settings", "Close"));
                if (menu == "BPM")_controller.SetBBPM(OptionBPM((_controller.GetBPM(), _controller.GetBeats())).Result);
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
        public static async Task<(int,int)> OptionBPM((int,int) bbpm )
        {
            (int _bpm, int _beats) _t = bbpm;
            AnsiConsole.Write("| Press Enter to save | Press Escape to leave |\n| Press Up/Down Arrow to increase/decrease | Press Right/Left Arrow to increase/decrease |\n");
            var panel = new Panel($"[bold]Current BPM:[/] {_t._bpm}\n[bold]Current Beats:[/] {_t._beats}");
            panel.Expand();

            (int,int) finalBpm = await AnsiConsole.Live(panel).StartAsync(async ctx =>
            {
                ctx.UpdateTarget(panel);
                while (true)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (_t._bpm < 240) _t._bpm++;
                            break;

                        case ConsoleKey.LeftArrow:
                            if (_t._bpm > 20) _t._bpm--;
                            break;
                        case ConsoleKey.UpArrow:
                            if (_t._beats < 8) _t._beats++;
                            break;

                        case ConsoleKey.DownArrow:
                            if (_t._beats > 1) _t._beats--;
                            break;
                        case ConsoleKey.Enter:
                            return _t;
                        case ConsoleKey.Escape:
                            return bbpm;
                        default:
                            break;
                    }
                    panel = new Panel($"[bold]Current BPM:[/] {_t._bpm}\n[bold]Current Beats:[/] {_t._beats}");
                    panel.Expand();
                    ctx.UpdateTarget(panel);
                }
            });
            return finalBpm;
        }
        public void OptionSettings()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices("Beat Sound", "Return"));
                if (menu == "Beat Sound") _controller.SetSound(Sound(_controller.GetSound()).Result);
                else if (menu == "Save Preset") OptionSavePreset();
                else if (menu == "Load Preset") OptionLoadPreset();
                else if (menu == "Return") return;
            }
        }
        public static async Task<(int, int)> Sound((int,int) sound)
        {
            (int _FB, int _B) _t = sound;
            AnsiConsole.Write("| Press Enter to save | Press Escape to leave |\n| Press Up/Down Arrow to increase/decrease | Press Right/Left Arrow to increase/decrease |\n");
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

        public void OptionSavePreset()
        {
            _controller.SavePreset(AnsiConsole.Prompt(new TextPrompt<string>("Please enter preset [green]name[/] to save:").PromptStyle("cyan")));
        }
        public void OptionLoadPreset()
        {

        }
        public static void ShowClosePrompt()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[red]Bye! [/] :waving_hand:");
        }
    }
}
