using System.Windows.Forms;
using System;

namespace MetronomeGraphic.Views
{
    partial class SettingView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnOne = new System.Windows.Forms.Button();
            this.btnTwo = new System.Windows.Forms.Button();
            this.btnThree = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.btnOne.Location = new System.Drawing.Point(30, 30);
            this.btnOne.Name = "btnOne";
            this.btnOne.Size = new System.Drawing.Size(100, 30);
            this.btnOne.Text = "Zapisz";
            this.btnOne.UseVisualStyleBackColor = true;
            this.btnOne.Click += new System.EventHandler(this.btnOne_Click);

            this.btnTwo.Location = new System.Drawing.Point(30, 80);
            this.btnTwo.Name = "btnTwo";
            this.btnTwo.Size = new System.Drawing.Size(100, 30);
            this.btnTwo.Text = "Wczytaj";
            this.btnTwo.UseVisualStyleBackColor = true;
            this.btnTwo.Click += new System.EventHandler(this.btnTwo_Click);

            this.btnThree.Location = new System.Drawing.Point(30, 130);
            this.btnThree.Name = "btnThree";
            this.btnThree.Size = new System.Drawing.Size(100, 30);
            this.btnThree.Text = "Dźwięk";
            this.btnThree.UseVisualStyleBackColor = true;
            this.btnThree.Click += new System.EventHandler(this.btnThree_Click);

            // Konfiguracja okna
            this.ClientSize = new System.Drawing.Size(160, 200);
            this.Controls.Add(this.btnOne);
            this.Controls.Add(this.btnTwo);
            this.Controls.Add(this.btnThree);
            this.Name = "Form1";
            this.Text = "Trzy Przyciski";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnOne;
        private System.Windows.Forms.Button btnTwo;
        private System.Windows.Forms.Button btnThree;
        private void btnOne_Click(object sender, EventArgs e)
        {
            SavePresetView savePresetView = new SavePresetView(_controller, this);
            savePresetView.ShowDialog();
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            LoadPresetView loadPresetView = new LoadPresetView(_controller, _mainView);
            loadPresetView.Size = new System.Drawing.Size(250, 400);

            int newX = this.Location.X - loadPresetView.Width - 10; 
            int newY = this.Location.Y;
            if (newX < 0) newX = 0; 
            loadPresetView.StartPosition = FormStartPosition.Manual;
            loadPresetView.Location = new System.Drawing.Point(newX, newY);
            loadPresetView.ShowDialog();
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            ChangeSoundView changeSoundView = new ChangeSoundView(_controller, this);
            changeSoundView.Size = new System.Drawing.Size(400, 250);

            int newX = this.Location.X - changeSoundView.Width - 10;
            int newY = this.Location.Y;
            if (newX < 0) newX = 0;
            changeSoundView.StartPosition = FormStartPosition.Manual;
            changeSoundView.Location = new System.Drawing.Point(newX, newY);
            changeSoundView.ShowDialog();
        }

    }
}
