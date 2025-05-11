using MetronomeGraphic.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MetronomeGraphic.Views
{
    public partial class SettingView : Form
    {
        private ViewController _controller;
        private ActivityView _mainView;

        public SettingView(ViewController controller, ActivityView mainView)
        {
            this._controller = controller;
            this._mainView = mainView;
            InitializeComponent();
        }
    }

}
