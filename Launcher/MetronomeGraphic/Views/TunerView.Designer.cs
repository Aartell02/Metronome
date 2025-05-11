using MetronomeGraphic.Controllers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MetronomeGraphic.Views
{
    public partial class TunerView : Form
    {
        private Label noteLabel;
        private Label centLabel;
        private Panel indicatorPanel;
        private Timer refreshTimer;
        private Button backButton; 


        public void CustomInitializeComponent()
        {
            this.Text = "Stroik Chromatyczny";
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            noteLabel = new Label
            {
                Text = "Nuta: --",
                Location = new System.Drawing.Point(150, 30),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 20, FontStyle.Bold)
            };
            this.Controls.Add(noteLabel);

            centLabel = new Label
            {
                Text = "Cent: 0",
                Location = new System.Drawing.Point(150, 80),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 16)
            };
            this.Controls.Add(centLabel);

            indicatorPanel = new Panel
            {
                Size = new System.Drawing.Size(20, 20),
                BackColor = Color.Red,
                Location = new System.Drawing.Point(190, 150) 
            };
            this.Controls.Add(indicatorPanel);

            Panel indicatorTrack = new Panel
            {
                Size = new System.Drawing.Size(200, 5),
                BackColor = Color.Gray,
                Location = new System.Drawing.Point(100, 160)
            };
            this.Controls.Add(indicatorTrack);

            refreshTimer = new Timer { Interval = 100 }; 
            refreshTimer.Tick += (s, e) => UpdateView();
            refreshTimer.Start();
            backButton = new Button
            {
                Text = "Cofnij",
                Location = new System.Drawing.Point(150, 220),
                Size = new System.Drawing.Size(100, 40),
                Font = new System.Drawing.Font("Arial", 12)
            };
            backButton.Click += BackButton_Click;
            this.Controls.Add(backButton);
        }


        private void UpdateView()
        {
            float currentNote = _controller.GetDetectedFrequency();
            float centDeviation = currentNote - _controller.GetClosestFrequency(); 

            noteLabel.Text = $"Nuta: {currentNote}";

            centLabel.Text = $"Cent: {centDeviation}";

            UpdateIndicatorPosition(Convert.ToInt32(centDeviation));
        }


        private void UpdateIndicatorPosition(int centDeviation)
        {
            int centerX = 190; 
            int maxMovement = 100; 
            int newX = centerX + (centDeviation * maxMovement / 50);

            newX = Math.Max(100, Math.Min(newX, 300)); 

            indicatorPanel.Location = new System.Drawing.Point(newX, indicatorPanel.Location.Y);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
            _mainView.Show(); 
        }
    }
}
