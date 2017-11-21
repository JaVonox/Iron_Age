using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exp2
{
    public partial class Observe : Form
    {
        static int xlen = 1400;
        static int ylen = 900;

        public Observe()
        {
            InitializeComponent();
        }

        private void Observe_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(xlen, ylen);
            this.Icon = Exp2.Properties.Resources.WorldGen;
        }

        private void Observe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
