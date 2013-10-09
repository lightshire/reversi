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
             numMyChips = 0;
            numOppChips = 0;

            InitializeComponent();
            coin = "";
            label21.Text = InitialForm.myColor.ToString();

           
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
            numMyChips = board.myChips().Count;
            numOppChips = board.opponentChips().Count;

            yourCtr.Text = numMyChips.ToString();
            oppCtr.Text = numOppChips.ToString();

           if (!radioButton1.Checked && !radioButton2.Checked) {
                MessageBox.Show("Select Heads or Tails First!!! The Default is Heads");
            }
            // insufficient error handling
           try
           {
               if (coin != "")
               {//first move
                   SubmitPositionForm form = new SubmitPositionForm(1, oppColor, false);


                   yourCtr.Text = numMyChips.ToString();
                   oppCtr.Text = numOppChips.ToString();

                   if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                   {

                       numMyChips = board.myChips().Count;
                       numOppChips = board.opponentChips().Count;
                       yourCtr.Text = numMyChips.ToString();
                       oppCtr.Text = numOppChips.ToString();

                       if (coin == "tails")
                       {//second move
                           SubmitPositionForm form2 = new SubmitPositionForm(1, oppColor, true);
                           boardControl.board.getAvailableAdjacentMoves(oppColor);


                           numMyChips = board.myChips().Count;
                           numOppChips = board.opponentChips().Count;
                           yourCtr.Text = numMyChips.ToString();
                           oppCtr.Text = numOppChips.ToString();


                           if (form2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                           {

                               numMyChips = board.myChips().Count;
                               numOppChips = board.opponentChips().Count;
                               yourCtr.Text = numMyChips.ToString();
                               oppCtr.Text = numOppChips.ToString();

                               DialogResult result = MessageBox.Show("Is AI Move Heads?", "Heads or Tails", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                               if (result == DialogResult.Yes)
                               {
                                 

                                   numMyChips = board.myChips().Count;
                                   numOppChips = board.opponentChips().Count;
                                   yourCtr.Text = numMyChips.ToString();
                                   oppCtr.Text = numOppChips.ToString();

                                   boardControl.createAIThread();
                                   BoardControl.isHeads = true;
                                  

                               }
                               else
                               {
                                


                                   numMyChips = board.myChips().Count;
                                   numOppChips = board.opponentChips().Count;
                                   yourCtr.Text = numMyChips.ToString();
                                   oppCtr.Text = numOppChips.ToString();


                                   boardControl.createAIThread();
                                   BoardControl.isHeads = false;

                               }
                           }

                       }
                       else
                       {
                           DialogResult result = MessageBox.Show("Is AI Move Heads?", "Heads or Tails", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                           if (result == DialogResult.Yes)
                           {

                             
                               numMyChips = board.myChips().Count;
                               numOppChips = board.opponentChips().Count;
                               yourCtr.Text = numMyChips.ToString();
                               oppCtr.Text = numOppChips.ToString();

                               boardControl.createAIThread();
                               BoardControl.isHeads = true;

                              

                           }
                           else
                           {
                              
                               numMyChips = board.myChips().Count;
                               numOppChips = board.opponentChips().Count;
                               yourCtr.Text = numMyChips.ToString();
                               oppCtr.Text = numOppChips.ToString();

                               boardControl.createAIThread();
                               BoardControl.isHeads = false;

                           }
                       }
                   }

                 
           
                   


                   


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
