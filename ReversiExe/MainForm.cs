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
        int numMyChips;
        int numOppChips;
        Board board;

        private InitialForm getColor;

        private void getOtherColor {
        
      //  label21.Text = getColor.gettextColor.Text;
        }

        public MainForm(String yColor, double bias)
        {
            boardControl = new BoardControl();
            InitializeComponent();
            coin = "";

            panel1.Controls.Add(boardControl);
            board = Board.getInstance;
            panel1.Controls.Add(boardControl);

            numMyChips = 2;
            numOppChips = 2;
            yourCtr.Text = numMyChips.ToString();
            oppCtr.Text = numOppChips.ToString();

            biasFactor = bias;
            if (yColor == "Black")
            {
                yourColor = Color.Black;
                oppColor = Color.White;
                btnOppMove.Enabled = true;
            }
            else
            {
                yourColor = Color.White;
                oppColor = Color.Black;
              
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
                

                numMyChips = board.myChips().Count;
                numOppChips = board.opponentChips().Count;
                yourCtr.Text = numMyChips.ToString();
                oppCtr.Text = numOppChips.ToString();
            }

          

        }

       

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            coin = "heads";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            coin = "tails";
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }




    }
}
