using System.Collections.Generic;
using System;
using System.Linq;
using MetronomeGraphic.Models;

namespace MetronomeGraphic.Views
{
    public partial class LoadPresetView
    {
        private System.ComponentModel.IContainer components = null;
        private Presets presets;

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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lstNames = new System.Windows.Forms.ListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.txtSearch.Location = new System.Drawing.Point(20, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 23);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.lstNames.FormattingEnabled = true;
            this.lstNames.ItemHeight = 15;
            this.lstNames.Location = new System.Drawing.Point(20, 60);
            this.lstNames.Name = "lstNames";
            this.lstNames.Size = new System.Drawing.Size(200, 200);
            this.lstNames.TabIndex = 1;

            this.btnRefresh.Location = new System.Drawing.Point(20, 280);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Odśwież";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.btnLoad.Location = new System.Drawing.Point(130, 280);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(90, 30);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Wczytaj";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

            this.btnDelete.Location = new System.Drawing.Point(20, 320);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(200, 30);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Usuń";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.ClientSize = new System.Drawing.Size(250, 370);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lstNames);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnDelete);
            this.Name = "LoadPresetView";
            this.Text = "Wczytaj preset";
            this.ResumeLayout(false);
            this.PerformLayout();
            UpdateNameList();
        }

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListBox lstNames;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnDelete;

  
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredNames = names
                .Where(name => name.ToLower().Contains(searchText))
                .ToList();

            lstNames.DataSource = null;
            lstNames.DataSource = filteredNames;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            UpdateNameList();
        }

        private void UpdateNameList()
        {
            names = _presets.Select(p => p.name).ToArray();
            lstNames.DataSource = null;
            lstNames.DataSource = names;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (lstNames.SelectedItem != null)
            {
                string selectedPreset = lstNames.SelectedItem.ToString();
                _controller.LoadPreset(selectedPreset);
                _mainView.UpdateView();
                System.Windows.Forms.MessageBox.Show($"Wczytano preset: {selectedPreset}", "Informacja");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Nie wybrano presetu do wczytania.", "Błąd");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstNames.SelectedItem != null)
            {
                string selectedPreset = lstNames.SelectedItem.ToString();
                var result = System.Windows.Forms.MessageBox.Show(
                    $"Czy na pewno chcesz usunąć preset '{selectedPreset}'?",
                    "Potwierdzenie",
                    System.Windows.Forms.MessageBoxButtons.YesNo
                );

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    _controller.DeletePreset(selectedPreset);
                    UpdateNameList();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Nie wybrano presetu do usunięcia.", "Błąd");
            }
        }
    }
}