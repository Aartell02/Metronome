using MetronomeGraphic.Controllers;
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
    public partial class SavePresetView : Form
    {
        private ViewController _controller;
        private SettingView _mainView;

        public SavePresetView(ViewController controller, SettingView mainView)
        {
            this._controller = controller;
            this._mainView = mainView;
            InitializeComponent();
        }
    }
}
