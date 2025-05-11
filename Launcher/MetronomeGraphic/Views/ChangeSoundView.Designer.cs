using System.Collections.Generic;
using System.Windows.Forms;

namespace MetronomeGraphic.Views
{
    partial class ChangeSoundView
    {
        private Label fbsLabel;
        private Label fbLabel;
        private Timer increaseFBSTimer;
        private Timer decreaseFBSTimer;
        private Timer increaseFBTimer;
        private Timer decreaseFBTimer;
        public void InitializeComponent()
        {
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            fbsLabel = new Label
            {
                Text = $"First Beat Sound: {_controller.GetSound().Item1}",
                Location = new System.Drawing.Point(100, 60),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 14)
            };
            this.Controls.Add(fbsLabel);

            Button decreaseFBSButton = new Button
            {
                Text = "-",
                Location = new System.Drawing.Point(50, 60),
                Size = new System.Drawing.Size(40, 40),
                Font = new System.Drawing.Font("Arial", 14)
            };
            decreaseFBSButton.MouseDown += DecreaseFBSButton_MouseDown;
            decreaseFBSButton.MouseUp += FBSButton_MouseUp;
            this.Controls.Add(decreaseFBSButton);

            Button increaseFBSButton = new Button
            {
                Text = "+",
                Location = new System.Drawing.Point(300, 60),
                Size = new System.Drawing.Size(40, 40),
                Font = new System.Drawing.Font("Arial", 14)
            };
            increaseFBSButton.MouseDown += IncreaseFBSButton_MouseDown;
            increaseFBSButton.MouseUp += FBSButton_MouseUp;
            this.Controls.Add(increaseFBSButton);

            fbLabel = new Label
            {
                Text = $"Beat Sound: {_controller.GetSound().Item2}",
                Location = new System.Drawing.Point(120, 100),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 14)
            };
            this.Controls.Add(fbLabel);

            Button decreaseFBButton = new Button
            {
                Text = "-",
                Location = new System.Drawing.Point(50, 100),
                Size = new System.Drawing.Size(40, 40),
                Font = new System.Drawing.Font("Arial", 14)
            };
            decreaseFBButton.MouseDown += DecreaseFBButton_MouseDown;
            decreaseFBButton.MouseUp += FBButton_MouseUp;
            this.Controls.Add(decreaseFBButton);

            Button increaseFBButton = new Button
            {
                Text = "+",
                Location = new System.Drawing.Point(300, 100),
                Size = new System.Drawing.Size(40, 40),
                Font = new System.Drawing.Font("Arial", 14)
            };
            increaseFBButton.MouseDown += IncreaseFBButton_MouseDown;
            increaseFBButton.MouseUp += FBButton_MouseUp;
            this.Controls.Add(increaseFBButton);
            increaseFBSTimer = new Timer { Interval = 120 };
            increaseFBSTimer.Tick += (s, e) => IncreaseFBS();

            decreaseFBSTimer = new Timer { Interval = 120 };
            decreaseFBSTimer.Tick += (s, e) => DecreaseFBS();

            increaseFBTimer = new Timer { Interval = 120 };
            increaseFBTimer.Tick += (s, e) => IncreaseFB();

            decreaseFBTimer = new Timer { Interval = 120 };
            decreaseFBTimer.Tick += (s, e) => DecreaseFB();
        }
        private void IncreaseFBSButton_MouseDown(object sender, MouseEventArgs e)
        {
            IncreaseFBS();
            increaseFBSTimer.Start();
        }

        private void DecreaseFBSButton_MouseDown(object sender, MouseEventArgs e)
        {

            DecreaseFBS();
            decreaseFBSTimer.Start();
        }

        private void FBSButton_MouseUp(object sender, MouseEventArgs e)
        {
            increaseFBSTimer.Stop();
            decreaseFBSTimer.Stop();
        }

        private void IncreaseFBS()
        {
            if (_controller.GetSound().Item1 < 2000)
            {
                _controller.SetSound((_controller.GetSound().Item1 + 10, _controller.GetSound().Item2));
                fbsLabel.Text = $"First Beat Sound: {_controller.GetSound().Item1}";
            }
        }

        private void DecreaseFBS()
        {
            if (_controller.GetSound().Item1 > 600)
            {
                _controller.SetSound((_controller.GetSound().Item1 - 10, _controller.GetSound().Item2));
                fbsLabel.Text = $"Beat Sound: {_controller.GetSound().Item1}";
            }
        }

        private void IncreaseFBButton_MouseDown(object sender, MouseEventArgs e)
        {
            IncreaseFB();
            increaseFBTimer.Start();
        }

        private void DecreaseFBButton_MouseDown(object sender, MouseEventArgs e)
        {
            DecreaseFB();
            decreaseFBTimer.Start();
        }

        private void FBButton_MouseUp(object sender, MouseEventArgs e)
        {
            increaseFBTimer.Stop();
            decreaseFBTimer.Stop();
        }

        private void IncreaseFB()
        {
            if (_controller.GetSound().Item2 < 1000)
            {
                _controller.SetSound((_controller.GetSound().Item1,_controller.GetSound().Item2 + 10));
                fbLabel.Text = $"Beat Sound: {_controller.GetSound().Item2}";
            }
        }

        private void DecreaseFB()
        {
            if (_controller.GetSound().Item2 > 300)
            {
                _controller.SetSound((_controller.GetSound().Item1, _controller.GetSound().Item2 - 10));
                fbLabel.Text = $"Beat Sound: {_controller.GetSound().Item2}";
            }
        }
    }
}