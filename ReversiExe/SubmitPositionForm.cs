using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReversiLibrary.GameModels;
namespace ReversiExe
{
    public partial class SubmitPositionForm : Form
    {
        int     playerNumber;
        Color   playerColor;
        Board   board;
        bool    tf; //false if 1st move, true if 2nd move


        public SubmitPositionForm()
        {
            InitializeComponent();
            playerNumber    = 1;
            board           = Board.getInstance;
            playerColor     = board.teamColor;
        }

        public SubmitPositionForm(int playerNumber, Color playerColor, bool tf)
        {
            this.playerNumber   = playerNumber;
            board               = Board.getInstance;
            this.playerColor    = playerColor;
            this.tf             = tf; 
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tf == false)
            {
                int x = 0;
                int y = 0;
                if (textBox1.Text != "" &&
                    textBox2.Text != "" &&
                    Int32.TryParse(textBox1.Text, out x) &&
                    Int32.TryParse(textBox2.Text, out y))
                {
                    Point p = new Point(x, y);
                    Chip chip = new Chip(playerColor, true);

                    board.addChip(p, chip);
                }
            }
            else
            {
                int x = 0;
                int y = 0;
                if (textBox1.Text != "" &&
                    textBox2.Text != "" &&
                    Int32.TryParse(textBox1.Text, out x) &&
                    Int32.TryParse(textBox2.Text, out y))
                {
                    Point p = new Point(x, y);
                    Chip chip = new Chip(playerColor, true);

                    board.addChip(p, chip);
                }
            }
            

            this.Close();
        }
        

    }
}
