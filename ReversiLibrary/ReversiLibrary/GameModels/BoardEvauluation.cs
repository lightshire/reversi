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

        public int lookAheadDepth = 2;

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

        public Move bestMove(Board board, Color color, int depth)
        {

            Move point = new Move();
            //get available moves so error handling will be limited.
            
            List<Point> availableMoves = (board.initialState) ? board.availableMoves(color, board.boardChips) : board.availableMoves(color == Color.Black ? Color.White : Color.Black, board.boardChips);
            
            
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

                int opponentMoves = newBoard.availableMoves(nextColor, newBoard.boardChips).Count;

                if (opponentMoves == 0)
                {
                    nextColor = nextColor == Color.Black ? Color.White : Color.Black;

                    if (newBoard.availableMoves(nextColor, newBoard.boardChips).Count == 0)
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
                    Move lookAheadPoint = bestMove(newBoard, color, depth + 1);
                    point.score = lookAheadPoint.score;

                    if (point.point.X <= 0)
                    {
                        point = move;
                    }
                    else if (move.score > point.score)
                    {
                        point = move;
                    }
                    
                }
                
             
            }
            return point;
        }
    }
}
