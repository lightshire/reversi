using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ReversiLibrary.GameModels;

namespace ReversiExe
{
    public partial class MainForm : Form
    {
        private BoardControl boardControl;
        Color yourColor;
        Color oppColor;
        double biasFactor;
        String coin;

        public MainForm(String yColor, double bias)
        {
            boardControl = new BoardControl();
            InitializeComponent();
            coin = "";

            panel1.Controls.Add(boardControl);

            biasFactor = bias;
            if (yColor == "Black")
            {
                yourColor = Color.Black;
                oppColor = Color.White;
                btnOppMove.Enabled = false;
            }
            else
            {
                yourColor = Color.White;
                oppColor = Color.Black;
                btnYourMove.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (coin!="")
            {//first move
                SubmitPositionForm form = new SubmitPositionForm(1, oppColor, false);
                form.ShowDialog();

                if (coin == "tails")
                {//second move
                    SubmitPositionForm form2 = new SubmitPositionForm(1, oppColor, true);
                    form2.ShowDialog();
                }

                btnOppMove.Enabled = false;
                btnYourMove.Enabled = true;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //List<Point> headsAvailableMove = availableMoves();

            if (coin != "")
            { 
                //??
            }
            SubmitPositionForm form = new SubmitPositionForm(2, yourColor, false);
            form.ShowDialog();
            btnOppMove.Enabled = true;
            btnYourMove.Enabled = false;
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            coin = "heads";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            coin = "tails";
        }




    }
}
