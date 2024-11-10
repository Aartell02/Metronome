﻿using Metronome.Controllers;
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
            Presets presets = new();
            MetronomeModel model = new(presets.GetByName("Default"));
            SCInterface view = new();
            ViewController controller = new(model,view, presets);
            view.SetController(controller);
            Console.CancelKeyPress += (sender, e) =>
            {
                ViewController.Close();
                Environment.Exit(0);
            };
            controller.Run();
            presets.SavePresets();
        }
    }
}
