using System;
using System.Linq;
using System.Numerics;
using NAudio.Wave;

namespace MetronomeGraphic.Models
{
    public class TunerModel
    {
        public float DetectedFrequency { get; private set; }
        public string ClosestString { get; private set; }
        public float ClosestFrequency { get; private set; }

        private static readonly float[] GuitarFrequencies = { 82.41f, 110.00f, 146.83f, 196.00f, 246.94f, 329.63f };
        private static readonly string[] GuitarStrings = { "E2", "A2", "D3", "G3", "B3", "E4" };

        public void UpdateFrequency(float frequency)
        {
            DetectedFrequency = frequency;
            ClosestFrequency = GuitarFrequencies.OrderBy(f => Math.Abs(f - frequency)).First();
            ClosestString = GuitarStrings[Array.IndexOf(GuitarFrequencies, ClosestFrequency)];
        }
    }
}
