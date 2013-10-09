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
        public Board board;

        public MainForm(String yColor, double bias)
        {
            
            InitializeComponent();
            coin = "";
            board = Board.getInstance;

            label21.Text = InitialForm.myColor.ToString();
            
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
            boardControl = new BoardControl(yourColor, oppColor, biasFactor);
            panel1.Controls.Add(boardControl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (!radioButton1.Checked && !radioButton2.Checked) {
                MessageBox.Show("Select Heads or Tails First!!! The Default is Heads");
            }
            // insufficient error handling
           try
           {
               if (coin != "")
               {//first move
                   SubmitPositionForm form = new SubmitPositionForm(1, oppColor, false);
                   form.ShowDialog();

                   if (coin == "tails")
                   {//second move
                       SubmitPositionForm form2 = new SubmitPositionForm(1, oppColor, true);
                       boardControl.board.getAvailableAdjacentMoves(oppColor);
                       form2.ShowDialog();
                   }


                   DialogResult result = MessageBox.Show("Is AI Move Heads?", "Heads or Tails", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                   if (result == DialogResult.Yes)
                   {
                       boardControl.createAIThread();
                       BoardControl.isHeads = true;
                   }
                   else
                   {
                       boardControl.createAIThread();
                       BoardControl.isHeads = false;
                   }

               }
           }
           catch (Exception y)
           {
               MessageBox.Show("Invalid Input", "", MessageBoxButtons.OK);


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
