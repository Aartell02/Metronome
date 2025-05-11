using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MetronomeGraphic.Views
{
    public partial class ActivityView : Form
    {
        private Label bpmLabel;
        private Label beatsLabel;
        private Timer increaseBPMTimer;
        private Timer decreaseBPMTimer;
        private Timer increaseBeatsTimer;
        private Timer decreaseBeatsTimer;
        private List<Panel> beatDots;

        public void CustomInitializeComponent()
        {
            this.Text = "Metronom";
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            beatDots = new List<Panel>();
            CreateBeatDots();

            bpmLabel = new Label
            {
                Text = $"BPM: {_controller.GetBPM()}",
                Location = new System.Drawing.Point(150, 60),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 14)
            };
            this.Controls.Add(bpmLabel);

            Button decreaseBPMButton = new Button
            {
                Text = "-",
                Location = new System.Drawing.Point(50, 60),
                Size = new System.Drawing.Size(40, 40),
                Font = new System.Drawing.Font("Arial", 14)
            };
            decreaseBPMButton.MouseDown += DecreaseBPMButton_MouseDown;
            decreaseBPMButton.MouseUp += BPMButton_MouseUp;
            this.Controls.Add(decreaseBPMButton);

            Button increaseBPMButton = new Button
            {
                Text = "+",
                Location = new System.Drawing.Point(300, 60),
                Size = new System.Drawing.Size(40, 40),
                Font = new System.Drawing.Font("Arial", 14)
            };
            increaseBPMButton.MouseDown += IncreaseBPMButton_MouseDown;
            increaseBPMButton.MouseUp += BPMButton_MouseUp;
            this.Controls.Add(increaseBPMButton);

            beatsLabel = new Label
            {
                Text = $"Beats: {_controller.GetBeats()}",
                Location = new System.Drawing.Point(150, 100),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 14)
            };
            this.Controls.Add(beatsLabel);

            Button decreaseBeatsButton = new Button
            {
                Text = "-",
                Location = new System.Drawing.Point(50, 100),
                Size = new System.Drawing.Size(40, 40),
                Font = new System.Drawing.Font("Arial", 14)
            };
            decreaseBeatsButton.MouseDown += DecreaseBeatsButton_MouseDown;
            decreaseBeatsButton.MouseUp += BeatsButton_MouseUp;
            this.Controls.Add(decreaseBeatsButton);

            Button increaseBeatsButton = new Button
            {
                Text = "+",
                Location = new System.Drawing.Point(300, 100),
                Size = new System.Drawing.Size(40, 40),
                Font = new System.Drawing.Font("Arial", 14)
            };
            increaseBeatsButton.MouseDown += IncreaseBeatsButton_MouseDown;
            increaseBeatsButton.MouseUp += BeatsButton_MouseUp;
            this.Controls.Add(increaseBeatsButton);

            Button startStopButton = new Button
            {
                Text = "START",
                Location = new System.Drawing.Point(50, 150),
                Size = new System.Drawing.Size(100, 40)
            };
            startStopButton.Click += StartStopButton_Click;
            this.Controls.Add(startStopButton);

            Button tunerButton = new Button
            {
                Text = "Stroik",
                Location = new System.Drawing.Point(240, 150),
                Size = new System.Drawing.Size(100, 40)
            };
            tunerButton.Click += TunerButton_Click;
            this.Controls.Add(tunerButton);

            Button settingsButton = new Button
            {
                Text = "Ustawienia",
                Location = new System.Drawing.Point(50, 210),
                Size = new System.Drawing.Size(290, 40)
            };
            settingsButton.Click += SettingsButton_Click;
            this.Controls.Add(settingsButton);

            increaseBPMTimer = new Timer { Interval = 150 };
            increaseBPMTimer.Tick += (s, e) => IncreaseBPM();

            decreaseBPMTimer = new Timer { Interval = 150 };
            decreaseBPMTimer.Tick += (s, e) => DecreaseBPM();

            increaseBeatsTimer = new Timer { Interval = 150 };
            increaseBeatsTimer.Tick += (s, e) => IncreaseBeats();

            decreaseBeatsTimer = new Timer { Interval = 150 };
            decreaseBeatsTimer.Tick += (s, e) => DecreaseBeats();

        }

        // BPM Buttons
        private void IncreaseBPMButton_MouseDown(object sender, MouseEventArgs e)
        {
            IncreaseBPM();
            increaseBPMTimer.Start();
        }

        private void DecreaseBPMButton_MouseDown(object sender, MouseEventArgs e)
        {
            
            DecreaseBPM();
            decreaseBPMTimer.Start();
        }

        private void BPMButton_MouseUp(object sender, MouseEventArgs e)
        {
            increaseBPMTimer.Stop();
            decreaseBPMTimer.Stop();
        }

        private void IncreaseBPM()
        {
            if (_controller.GetBPM() < 190)
            {
                _controller.SetBPM(_controller.GetBPM() + 1);
                bpmLabel.Text = $"BPM: {_controller.GetBPM()}";
            }
        }

        private void DecreaseBPM()
        {
            if (_controller.GetBPM() > 40)
            {
                _controller.SetBPM(_controller.GetBPM() - 1);
                bpmLabel.Text = $"BPM: {_controller.GetBPM()}";
            }
        }

        private void IncreaseBeatsButton_MouseDown(object sender, MouseEventArgs e)
        {
            IncreaseBeats();
            increaseBeatsTimer.Start();
        }

        private void DecreaseBeatsButton_MouseDown(object sender, MouseEventArgs e)
        {
            DecreaseBeats();
            decreaseBeatsTimer.Start();
        }

        private void BeatsButton_MouseUp(object sender, MouseEventArgs e)
        {
            increaseBeatsTimer.Stop();
            decreaseBeatsTimer.Stop();
            CreateBeatDots();
        }

        private void IncreaseBeats()
        {
            if (_controller.GetBeats() < 12)
            {
                _controller.SetBeats(_controller.GetBeats() + 1);
                beatsLabel.Text = $"Beats: {_controller.GetBeats()}";
            }
        }

        private void DecreaseBeats()
        {
            if (_controller.GetBeats() > 1)
            {
                _controller.SetBeats(_controller.GetBeats() - 1);
                beatsLabel.Text = $"Beats: {_controller.GetBeats()}";
            }
        }


        private void StartStopButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "START")
            {
                button.Text = "STOP";
                _controller.Timer_Start();
            }
            else
            {
                button.Text = "START";
                _controller.Timer_Stop();
            }
        }
        private void CreateBeatDots()
        {
            foreach (var dot in beatDots)
            {
                this.Controls.Remove(dot);
            }
            beatDots.Clear();

            int beats = _controller.GetBeats();
            int startX = 50;
            int startY = 20;
            int spacing = 300/beats;

            for (int i = 0; i < beats; i++)
            {
                Panel dot = new Panel
                {
                    Size = new System.Drawing.Size(20, 20),
                    Location = new System.Drawing.Point(startX + (i * spacing), startY),
                    BackColor = Color.Gray
                };
                beatDots.Add(dot);
                this.Controls.Add(dot);
            }
        }
        public void UpdateView()
        {
            bpmLabel.Text = $"BPM: {_controller.GetBPM()}";
            beatsLabel.Text = $"Beats: {_controller.GetBeats()}";
            CreateBeatDots();
        }
        public void UpdateBeatDots()
        {
            for (int i = 0; i < beatDots.Count; i++)
            {
                beatDots[i].BackColor = i == _controller.GetCurrentBeat() ? Color.Red : Color.Gray;
            }
        }

        private void TunerButton_Click(object sender, EventArgs e)
        {
            TunerView tunerview = new TunerView(_controller, this);
            tunerview.Show();
            this.Hide();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingView settingview = new SettingView(_controller, this);
            int newX = this.Location.X - settingview.Width - 10;
            int newY = this.Location.Y;
            if (newX < 0) newX = 0;
            settingview.StartPosition = FormStartPosition.Manual;
            settingview.Location = new System.Drawing.Point(newX, newY);
            settingview.Show();
        }

    }
}


