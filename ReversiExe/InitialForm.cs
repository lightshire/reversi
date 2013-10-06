using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReversiExe
{
    public partial class InitialForm : Form
    {
        public InitialForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            double bias = Double.Parse(textBias.Text);
            MainForm mf = new MainForm(textColor.Text, bias);
            this.Hide();
            mf.Show();
        }
    }
}
