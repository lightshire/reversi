using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ReversiLibrary.GameModels;
namespace ReversiExe
{
    public partial class SubmitCoordinateForm : Form
    {
        int     playerNumber;
        Color   playerColor;
        Board   board;


        public SubmitCoordinateForm()
        {
            InitializeComponent();
            playerNumber    = 1;
            board           = Board.getInstance;
            playerColor     = board.teamColor;
        }

        public SubmitCoordinateForm(int playerNumber, Color playerColor)
        {
            this.playerNumber   = playerNumber;
            board               = Board.getInstance;
            this.playerColor    = playerColor;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
            this.Close();
        }
        

    }
}
