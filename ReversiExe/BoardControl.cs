using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReversiLibrary.GameModels;

namespace ReversiExe
{
    public partial class BoardControl : UserControl
    {
       
        ChipControl[,] chipControls = new ChipControl[8, 8];
        Board board;
        public BoardControl()
        {
            InitializeComponent();
            board = new Board();
            board.ChipAdded += new ChipAddedHandler(board_ChipAdded);
            board.ChipFlipped += new ChipFlippedHandler(board_ChipFlipped);
            board.AvailableMovesGenerated += new AvailableMovesGeneratedHandler(board_AvailableMovesGenerated);
            createBoard();
            board.setUpBoard();
            
        }

        void board_AvailableMovesGenerated(List<Point> points)
        {
            foreach (Point point in points)
            {
                chipControls[point.Y - 1, point.X - 1].BackColor = Color.Red;
            }
        }

        void board_ChipFlipped(Point point, Chip chip)
        {
            //MessageBox.Show("Testing");
            chipControls[point.Y - 1, point.X - 1].BackColor = chip.chipColor;
        }

        void board_ChipAdded(Point point, Chip chip)
        {
            chipControls[point.Y-1, point.X-1].BackColor = chip.chipColor;
            
        }

        public void createBoard()
        {
            int height          = this.Height;
            int width           = this.Width;
            int initialWidth    = 0;
            int initialHeight   = 0;
            int ctr             = 0;

            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++) 
                {
                    ChipControl control = new ChipControl(ctr);
                    control.Location    = new Point(initialWidth, initialHeight);
                    control.BackColor   = Color.Green;
                    chipControls[i, j]  = control;
                    this.Controls.Add(chipControls[i, j]);
                    initialWidth += 50;
                    if (initialWidth == 400)
                    {
                        initialWidth = 0;
                        initialHeight += 50;
                    }
                    ctr++;
                }
            }
        }
    }
}
