using System.Numerics;

namespace Metronome.Services
{
    public static class SignalProcessor
    {
        public static float CalculateFundamentalFrequency(float[] samples, int sampleRate)
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

            return maxIndex * sampleRate / (float)n;
        }

        public static void FFT(Complex[] buffer)
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
