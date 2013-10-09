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
        public static Color myColor = Color.Black;
        public InitialForm()
        {
            InitializeComponent();
            textColor.Text = "Black";
            textBias.Text = "0";
            radioButton1.Checked = true;
           
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
                double bias = Double.Parse(textBias.Text);
                myColor = textColor.Text == "Black" ? Color.Black : Color.White;
                MainForm mf = new MainForm(textColor.Text, bias);
                this.Hide();

                mf.Show();
        }
      }
    }

