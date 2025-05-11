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
    public partial class TunerView : Form
    {
        private ViewController _controller;
        private ActivityView _mainView;
        public TunerView(ViewController controller, ActivityView mainView)
        {
            this._controller = controller;
            this._mainView = mainView;
            CustomInitializeComponent();
        }

    }
}
