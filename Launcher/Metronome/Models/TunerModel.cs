using System;
using System.Linq;
using System.Numerics;
using NAudio.Wave;
using Spectre.Console;

namespace Metronome.Models
{
    public class TunerModel
    {
        public float[] GuitarFrequencies = { 82.41f, 110.00f, 146.83f, 196.00f, 246.94f, 329.63f };
        public string[] GuitarStrings = { "E2", "A2", "D3", "G3", "B3", "E4" };

        public float DetectedFrequency { get; private set; }
        public string ClosestString { get; private set; }
        public float ClosestFrequency { get; private set; }
        public WaveInEvent waveIn;

        public event Action FrequencyDetected;

        public TunerModel()
        {
            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(44100, 1)
            };
            waveIn.DataAvailable += OnDataAvailable;
        }

        private void MatchFrequencyToGuitarString(float frequency)
        {
            ClosestFrequency = GuitarFrequencies.OrderBy(f => Math.Abs(f - frequency)).First();
            ClosestString = GuitarStrings[Array.IndexOf(GuitarFrequencies, ClosestFrequency)];
        }

        public void Start()
        {
            waveIn.StartRecording();
        }

        public void Stop()
        {
            waveIn.StopRecording();
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            float[] samples = new float[e.BytesRecorded / 2];
            for (int i = 0; i < samples.Length; i++)
                samples[i] = BitConverter.ToInt16(e.Buffer, i * 2) / 32768f;

            float frequency = CalculateFundamentalFrequency(samples, 44100);
            if (frequency > 0)
            {
                DetectedFrequency = frequency;
                MatchFrequencyToGuitarString(frequency);
                FrequencyDetected?.Invoke();
            }
        }

        public float CalculateFundamentalFrequency(float[] samples, int sampleRate)
        {
            int n = samples.Length;
            Complex[] complexSamples = samples.Select(s => new Complex(s, 0)).ToArray();

            FFT(complexSamples);

            double maxMagnitude = 0;
            int maxIndex = 0;
            for (int i = 0; i < n / 2; i++)
            {
                double magnitude = complexSamples[i].Magnitude;
                if (magnitude > maxMagnitude)
                {
                    maxMagnitude = magnitude;
                    maxIndex = i;
                }
            }

            float fundamentalFrequency = maxIndex * sampleRate / n;
            return fundamentalFrequency;
        }

        public void FFT(Complex[] buffer)
        {
            int n = buffer.Length;
            if (n <= 1) return;

            Complex[] even = new Complex[n / 2];
            Complex[] odd = new Complex[n / 2];
            for (int i = 0; i < n / 2; i++)
            {
                even[i] = buffer[i * 2];
                odd[i] = buffer[i * 2 + 1];
            }

            FFT(even);
            FFT(odd);

            for (int k = 0; k < n / 2; k++)
            {
                Complex t = Complex.Exp(-2.0 * Math.PI * Complex.ImaginaryOne * k / n) * odd[k];
                buffer[k] = even[k] + t;
                buffer[k + n / 2] = even[k] - t;
            }
        }
    }
}
