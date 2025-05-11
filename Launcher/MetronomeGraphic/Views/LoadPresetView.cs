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
        private string[] Names;

        public LoadPresetView(ViewController controller, ActivityView mainView)
        {
            this._controller = controller;
            this._mainView = mainView;
            Names = _controller.GetPresets().Select(p => p.Name).ToArray();

            InitializeComponent();
        }
    }
}
