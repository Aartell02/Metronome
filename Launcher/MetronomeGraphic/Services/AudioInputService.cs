using MetronomeGraphic.Models;
using NAudio.Wave;
using System;


namespace MetronomeGraphic.Services
{
    public class AudioInputService
    {
        private readonly WaveInEvent waveIn;
        private readonly TunerModel _tuner;
        private readonly int sampleRate = 44100;

        public event Action FrequencyUpdated;

        public AudioInputService(TunerModel tunerModel)
        {
            _tuner = tunerModel;

            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(sampleRate, 1)
            };

            waveIn.DataAvailable += OnDataAvailable;
        }

        public void Start() => waveIn.StartRecording();
        public void Stop() => waveIn.StopRecording();

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            float[] samples = new float[e.BytesRecorded / 2];
            for (int i = 0; i < samples.Length; i++)
                samples[i] = BitConverter.ToInt16(e.Buffer, i * 2) / 32768f;

            float frequency = SignalProcessor.CalculateFundamentalFrequency(samples, sampleRate);

            if (frequency > 0)
            {
                _tuner.UpdateFrequency(frequency);
                FrequencyUpdated?.Invoke();
            }
        }
    }
}
