using System.Windows.Forms;
using System;

namespace MetronomeGraphic.Views
{
    public partial class SavePresetView : Form
    {
        private Label labelName;
        private TextBox textBoxName;
        private Button buttonSave;
        private Button buttonCancel;

        public void InitializeComponent()
        {
            this.Text = "Formularz";
            this.Size = new System.Drawing.Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;

            labelName = new Label();
            labelName.Text = "Nazwa:";
            labelName.Location = new System.Drawing.Point(20, 20);
            labelName.AutoSize = true;

            textBoxName = new TextBox();
            textBoxName.Location = new System.Drawing.Point(20, 50);
            textBoxName.Width = 240;

            buttonSave = new Button();
            buttonSave.Text = "Zapisz";
            buttonSave.Location = new System.Drawing.Point(20, 100);
            buttonSave.Click += new EventHandler(ButtonSave_Click);

            buttonCancel = new Button();
            buttonCancel.Text = "Anuluj";
            buttonCancel.Location = new System.Drawing.Point(120, 100);
            buttonCancel.Click += new EventHandler(ButtonCancel_Click);

            this.Controls.Add(labelName);
            this.Controls.Add(textBoxName);
            this.Controls.Add(buttonSave);
            this.Controls.Add(buttonCancel);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            string nazwa = textBoxName.Text;
            if (!string.IsNullOrWhiteSpace(nazwa))
            {
                if (_controller.AddPreset(nazwa)) MessageBox.Show($"Zapisano: {nazwa}", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show($"Istnieje już zapis o nazwie: {nazwa}", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("Pole nazwa nie może być puste!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            textBoxName.Text = string.Empty; // Czyści pole tekstowe
            this.Close();
        }

    }
}