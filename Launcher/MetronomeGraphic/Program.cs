using MetronomeGraphic.Controllers;
using MetronomeGraphic.Models;
using MetronomeGraphic.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetronomeGraphic
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            PresetRepositoryModel presets = new PresetRepositoryModel();
            MetronomeModel model = new MetronomeModel(presets.GetByName("Default"));
            ViewController controller = new ViewController(model, presets);
            controller.Run();
            presets.SavePresets();
        }
    }
}
