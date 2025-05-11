using MetronomeGraphic.Controllers;
using MetronomeGraphic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetronomeGraphic.Views
{
    public partial class LoadPresetView : Form
    {
        private ViewController _controller;
        private ActivityView _mainView;
        private Presets _presets;
        private string[] names;

        public LoadPresetView(ViewController controller, ActivityView mainView)
        {
            this._controller = controller;
            this._mainView = mainView;
            this._presets = _controller.GetPresets();
            names = _presets.Select(p => p.name).ToArray();

            InitializeComponent();
        }
    }
}
