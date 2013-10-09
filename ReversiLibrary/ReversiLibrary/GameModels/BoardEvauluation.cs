using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace ReversiLibrary.GameModels
{
    public class BoardEvauluation
    {
        public Board currentBoard;
        public Board gameBoard;

        public int lookAheadDepth = 3;

        public struct Move
        {
            public Point point;
            public int score;

            public Move(Point point, int score)
            {
                this.point = new Point(0, 0);
                this.score = score;
            }
        }
        public BoardEvauluation()
        {
            currentBoard = new Board();
            gameBoard = new Board();
        }
        public BoardEvauluation(Board board)
        {
            gameBoard = Board.getInstance;
            currentBoard = board;
        }

        public Move bestMove(Board board, Color color, int depth, bool initialMove)
        {

            Move point = new Move();
            List<Point> availableMoves = new List<Point>();
            //get available moves so error handling will be limited.

            //if (initialMove)
            //{
            availableMoves = board.availableMoves(color, board.boardChips);
            //}
            //else
            //{
            //    availableMoves = board.availableMoves(color == Color.Black ? Color.White : Color.Black, board.boardChips);
            //}
            
            
            foreach (Point _point in availableMoves)
            {
                //create testboardq
                Move move = new Move();
                move.point = _point;

                Board newBoard = new Board(board);
                Chip chip = new Chip(color, true);
                newBoard.addChip(_point, chip);

                Color nextColor = color == Color.Black ? Color.White : Color.Black;
                bool endGame = false;
                List<Point> opponentAvailableMoves = newBoard.availableMoves(color == Color.Black ? Color.White : Color.Black, newBoard.boardChips);
                int opponentMoves = opponentAvailableMoves.Count;

                if (opponentMoves == 0)
                {
                    nextColor = nextColor == Color.Black ? Color.White : Color.Black;
                    List<Point> oneStepLookAheadMoves = newBoard.availableMoves(nextColor == Color.Black ? Color.White : Color.Black, newBoard.boardChips);
                    if (oneStepLookAheadMoves.Count == 0)
                    {
                        endGame = true;
                    }
                }

                if (endGame || depth == lookAheadDepth)
                {
                    if (!endGame)
                    {
                        newBoard.computeRank(availableMoves.Count - opponentMoves);
                        move.score = newBoard.stateScore;

                    }
                }
                else
                {
                    Move lookAheadPoint = bestMove(newBoard, color, depth + 1, false);
                    point.score = lookAheadPoint.score;

                    if (point.point.X <= 0)
                    {
                        point = move;
                    }

                    if (move.score > point.score)
                    {
                        point = move;
                    }
                    
                }
                
             
            }
            return point;
        }
    }
}
