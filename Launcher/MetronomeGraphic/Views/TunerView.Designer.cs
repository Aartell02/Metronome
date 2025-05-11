using MetronomeGraphic.Controllers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MetronomeGraphic.Views
{
    public partial class TunerView : Form
    {
        private Label frequencyLabel;
        private Label noteLabel;
        private Panel indicatorPanel;
        private Timer refreshTimer;
        private Button backButton;

        public void CustomInitializeComponent()
        {
            this.Text = "Stroik Chromatyczny";
            this.ClientSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            frequencyLabel = new Label
            {
                Text = "Częstotliwość: -- Hz",
                Location = new Point(100, 30),
                AutoSize = true,
                Font = new Font("Arial", 16)
            };
            this.Controls.Add(frequencyLabel);

            noteLabel = new Label
            {
                Text = "Nuta: --",
                Location = new Point(150, 80),
                AutoSize = true,
                Font = new Font("Arial", 24, FontStyle.Bold)
            };
            this.Controls.Add(noteLabel);

            indicatorPanel = new Panel
            {
                Size = new Size(20, 20),
                BackColor = Color.Red,
                Location = new Point(190, 150)
            };
            this.Controls.Add(indicatorPanel);

            Panel indicatorTrack = new Panel
            {
                Size = new Size(200, 5),
                BackColor = Color.Gray,
                Location = new Point(100, 160)
            };
            this.Controls.Add(indicatorTrack);

            backButton = new Button
            {
                Text = "Cofnij",
                Location = new Point(150, 220),
                Size = new Size(100, 40),
                Font = new Font("Arial", 12)
            };
            backButton.Click += BackButton_Click;
            this.Controls.Add(backButton);

            refreshTimer = new Timer { Interval = 100 };
            refreshTimer.Tick += (s, e) => UpdateView();
            refreshTimer.Start();
        }

        private void UpdateView()
        {
            float frequency = _controller.GetDetectedFrequency();
            string note = _controller.GetClosestString();

            if (frequency <= 0 || string.IsNullOrWhiteSpace(note)) return;

            frequencyLabel.Text = $"Częstotliwość: {frequency:F2} Hz";
            noteLabel.Text = $"Nuta: {note}";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            refreshTimer.Stop();
            _controller.StopTuner();
            this.Close();
            _mainView.Show();
        }
    }
}

