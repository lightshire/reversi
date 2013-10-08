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

        public MainForm(String yColor, double bias)
        {
            
            InitializeComponent();
            coin = "";
            

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

            board = Board.getInstance;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (radioButton1.Checked = false && radioButton2.Checked == false) {
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
                       form2.ShowDialog();
                   }
                 
                   boardControl.createAIThread();

                   numMyChips = board.myChips().Count;
                   numOppChips = board.opponentChips().Count;
                   yourCtr.Text = numMyChips.ToString();
                   oppCtr.Text = numOppChips.ToString();
                   

               }
           }
           catch (Exception y)
           {
               MessageBox.Show("Invalid Input", "", MessageBoxButtons.OK);


           }
           radioButton1.Checked = false;
           radioButton2.Checked = false;
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
