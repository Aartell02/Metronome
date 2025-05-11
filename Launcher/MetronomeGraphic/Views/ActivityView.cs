using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetronomeGraphic.Controllers;
namespace MetronomeGraphic.Views
{
    public partial class ActivityView : Form
    {
        private ViewController _controller;
        public ActivityView(ViewController controller)
        {
            this._controller = controller;
            CustomInitializeComponent();
        }
    }
}
