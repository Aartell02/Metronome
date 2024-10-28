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
            Model model = new Model();
            SCInterface view = new SCInterface(model);
            Controller controller = new Controller(model, view);

            controller.Start();

        }
    }
}
