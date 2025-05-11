using Metronome.Controllers;
using Metronome.Models;
using Metronome.Views;
namespace Metronome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PresetRepository presets = new();
            MetronomeModel model = new(presets.GetByName("Default"));
            ConsoleView view = new();
            ConsoleViewController controller = new(model,view, presets);
            view.SetController(controller);
            Console.CancelKeyPress += (sender, e) =>
            {
                ConsoleViewController.Close();
                Environment.Exit(0);
            };
            controller.Run();
            presets.SavePresets();
        }
    }
}
