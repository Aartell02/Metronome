using Spectre.Console;
using Metronome.Controllers;
using System.Data;
using System.Timers;


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
                AnsiConsole.Write(new Panel(new FigletText($"BPM: {_controller.GetBPM()}    Beats: {_controller.GetBeats()}").LeftJustified()));
                var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices($"{startstop}", "BPM", "Tuner", "Settings", "Close"));
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
                else if (menu == "Tuner") OptionTunerAsync();
                else if (menu == "Settings") OptionSettings();
                else if (menu == "Close") return;
            }
        }
        public void OptionTunerAsync()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Panel(new FigletText($"Tuner").Centered()));
            _controller.StartTuner();
            AnsiConsole.Live(new Panel($"[bold]Closest frequency:[/] {_controller._tuner.ClosestFrequency} \n[bold]Closest string:[/] {_controller._tuner.ClosestString}\n[bold]Detected frequency:[/] {_controller._tuner.DetectedFrequency}"))
            .Start(ctx =>
            {
                while(true)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(intercept: true);
                        if (key.Key == ConsoleKey.Escape)
                            break;
                    }
                        ctx.UpdateTarget(new Panel($"[bold]Closest frequency:[/] {_controller._tuner.ClosestFrequency} \n[bold]Closest string:[/] {_controller._tuner.ClosestString}\n[bold]Detected frequency:[/] {_controller._tuner.DetectedFrequency}"));
                }
            });
            _controller.StopTuner();
        }

        public static async Task<(int,int)> OptionBPM((int,int) bbpm )
        {
            (int _bpm, int _beats) _t = bbpm;
            AnsiConsole.Write("| Press Enter to save | Press Escape to leave |\n| Press Up/Down Arrow to increase/decrease | Press Right/Left Arrow to increase/decrease |\n");
            var panel = new Panel($"[bold]Current BPM:[/] {_t._bpm}    Right/Left \n[bold]Current Beats:[/] {_t._beats}    Up/Down ");
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
                            if (_t._bpm < 190) _t._bpm++;
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
                    .AddChoices("Beat Sound", "Save Preset", "Load Preset", "Delete Preset", "Return"));
                if (menu == "Beat Sound") _controller.SetSound(Sound(_controller.GetSound()).Result);
                else if (menu == "Save Preset") OptionSavePreset();
                else if (menu == "Load Preset") OptionLoadPreset();
                else if (menu == "Delete Preset") OptionDeletePreset();
                else if (menu == "Return") return;
            }
        }
        public static async Task<(int, int)> Sound((int,int) sound)
        {
            (int _FB, int _B) _t = sound;
            AnsiConsole.Write("| Press Enter to save | Press Escape to leave |\n| Press Up/Down Arrow to increase/decrease | Press Right/Left Arrow to increase/decrease |\n");
            var panel = new Panel($"[bold]Current First Beat Hz:[/] {_t._FB}    Right/Left\n[bold]Current Beat Hz:[/] {_t._B}    Up/Down");
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
            var name = AnsiConsole.Prompt(new TextPrompt<string>("Please enter preset [green]name[/] to save:").PromptStyle("cyan"));
            if (!_controller.AddPreset(name)) AnsiConsole.WriteLine("Preset added succesfuly");
            else
            {
                AnsiConsole.Clear();
                var option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Name is already taken. Do you want to overwrite existing preset?")
                    .AddChoices("Yes", "No"));
                if (option == "Yes") _controller.OverwritePreset(name);
            }
        }
        public void OptionLoadPreset()
        {
            string[] names = _controller.GetPresets().Select(p => p.name).Concat(new[] { "Return" }).ToArray();
            while (true)
            {
                AnsiConsole.Clear();
                string pre = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(names));
                if (pre != "Return") _controller.LoadPreset(pre);
                return;
            }
        }
        public void OptionDeletePreset()
        {
            string[] names = _controller.GetPresets().Select(p => p.name).Concat(new[] { "Return" }).ToArray();
            while (true)
            {
                AnsiConsole.Clear();
                string pre = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(names.Skip(1)));
                if (pre != "Return")
                {
                    AnsiConsole.Clear();
                    var option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Are you sure you want to delete this preset?").AddChoices("Yes", "No"));
                    if (option == "Yes")
                    {
                        _controller.DeletePreset(pre);
                        names = _controller.GetPresets().Select(p => p.name).Concat(new[] { "Return" }).ToArray();
                    }
                }
                else if( pre == "Return") return;
            }
        }
        public static void ShowClosePrompt()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("\n[red]Bye! [/]");
        }
    }
}
