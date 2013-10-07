using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReversiLibrary.GameModels;
using System.Threading;


namespace ReversiExe
{
    public partial class BoardControl : UserControl
    {
       
        ChipControl[,] chipControls = new ChipControl[8, 8];
        Board board;
        Color myMove, oppMove;
        Thread AIMoveThread;

        public BoardControl()
        {
            InitializeComponent();
            board = new Board();
            board.ChipAdded += new ChipAddedHandler(board_ChipAdded);
            board.ChipFlipped += new ChipFlippedHandler(board_ChipFlipped);
            board.AvailableMovesGenerated += new AvailableMovesGeneratedHandler(board_AvailableMovesGenerated);
            board.makeInstance();
            createBoard();
            board.setUpBoard();

            inistiateAIThread();
            

        }

        public BoardControl(Color myMove, Color oppMove, double bias)
        {
            this.myMove = myMove;
            this.oppMove = oppMove;

            InitializeComponent();
            board = new Board(myMove, oppMove, bias);
            board.ChipAdded +=new ChipAddedHandler(board_ChipAdded);
            board.ChipFlipped +=new ChipFlippedHandler(board_ChipFlipped);
            board.AvailableMovesGenerated  +=new AvailableMovesGeneratedHandler(board_AvailableMovesGenerated);
            board.makeInstance();
            createBoard();
            board.setUpBoard();

            inistiateAIThread();
               
        }

        void createAIThread()
        {
            BoardEvauluation evaluation = new BoardEvauluation(new Board(board));
            AIMoveThread = new Thread(inistiateAIThread);
            AIMoveThread.Start();

        }
        void inistiateAIThread()
        {
            Board currentBoard = board;

            BoardEvauluation evalv = new BoardEvauluation(currentBoard);
            BoardEvauluation.Move move = evalv.bestMove(currentBoard, Color.Black, 1);
            System.Diagnostics.Debug.WriteLine("initiatied AI Thread");
            
            board.addChip(move.point, new Chip(Color.Black, true));
        }
        
        void board_AvailableMovesGenerated(List<Point> points)
        {
            //reset board

            foreach (ChipControl control in chipControls)
            {
                if (control.BackColor != Color.Black && control.BackColor != Color.White)
                {
                    control.BackColor = Color.Green;
                }
            }

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
            if(point != new Point(0, 0))
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
