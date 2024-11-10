using Metronome.Controllers;
using Metronome.Models;
using Metronome.Views;
using System.Reflection.Metadata;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Metronome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IOModel model = new(new MetronomeModel());
            SCInterface view = new SCInterface();
            ViewController controller = new ViewController(model, view);
            view.SetController(controller);
            Console.CancelKeyPress += (sender, e) =>
            {
                ViewController.Close();
                Environment.Exit(0);
            };
            controller.Run();
        }

    }
}
