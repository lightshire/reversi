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
    public partial class Form1 : Form
    {
        private BoardControl boardControl;
        public Form1()
        {
            boardControl = new BoardControl();
            InitializeComponent();

            panel1.Controls.Add(boardControl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubmitCoordinateForm form = new SubmitCoordinateForm(1, Color.Black);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubmitCoordinateForm form = new SubmitCoordinateForm(2, Color.Yellow);
            form.ShowDialog();
        }


    }
}
