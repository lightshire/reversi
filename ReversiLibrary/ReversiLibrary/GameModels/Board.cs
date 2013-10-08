using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;




namespace ReversiLibrary.GameModels
{

    //create event to fetch the chips in the board
    public delegate void ChipAddedHandler(Point point, Chip chip);
    public delegate void ChipFlippedHandler(Point point, Chip chip);
    public delegate void AvailableMovesGeneratedHandler(List<Point> points);
    public delegate void AvailableAdjacentMovesGeneratedHandler(List<Point> points);
    

    public class Board : ICloneable
    {
        public event ChipAddedHandler ChipAdded;
        public event ChipFlippedHandler ChipFlipped;
        public event AvailableMovesGeneratedHandler AvailableMovesGenerated;
        public event AvailableAdjacentMovesGeneratedHandler AvailableAdjacentMovesGenerated;
    
        public struct Direction
        {
            public const int up             = 1;
            public const int down           = 2;
            public const int left           = 3;
            public const int right          = 4;
            public const int topLeft        = 5;
            public const int topRight       = 6;
            public const int bottomLeft     = 7;
            public const int bottomRight    = 8;
        
        }

        public static Board getInstance { get; set; }
        public Dictionary<Point, Chip> boardChips { get; set; }
        
        public double headChance { get; set; }
        public double tailsChance { get; set; }
        public double biasFactor { get; set; }

        public int blackFrontierCount { get; set; }
        public int whiteFrontierCount { get; set; }
        public int stateScore { get; set; }


        public bool initialState { get; set; }
        public Color teamColor { get; set; }
        public Color opponentColor { get; set; }

        
        
        public Board()
        {
            boardChips = new Dictionary<Point, Chip>();
            teamColor = Color.Black;
            opponentColor = Color.Yellow;

           
            headChance = 0;
            tailsChance = 0;
            initialState = false;

            stateScore = 0;

            biasFactor = 0;
        }

        public List<Point> getAvailableAdjacentMoves(Color color)
        {
            List<Point> point = new List<Point>();
            List<Point> _availableMoves = availableMoves(color, boardChips);
            Dictionary<Point, Chip> _boardChips = boardChips;

            foreach (var chip in _boardChips)
            {
                //cursor going up
                Point cursor = new Point(chip.Key.X, chip.Key.Y - 1);

                if (cursor.Y >= 1)
                {
                    if (!_availableMoves.Contains(cursor) && !_boardChips.ContainsKey(cursor))
                    {
                        point.Add(cursor);
                    }
                }

                //cursor going down
                cursor = new Point(chip.Key.X, chip.Key.Y + 1);

                if (cursor.Y <= 8)
                {
                    if (!_availableMoves.Contains(cursor) && !_boardChips.ContainsKey(cursor))
                    {
                        point.Add(cursor);
                    }
                }

                //cursor going right
                cursor = new Point(chip.Key.X + 1, chip.Key.Y);
                if (cursor.X <= 8)
                {
                    if (!_availableMoves.Contains(cursor) && !_boardChips.ContainsKey(cursor))
                    {
                        point.Add(cursor);
                    }
                }

                //cursor going left
                cursor = new Point(chip.Key.X - 1, chip.Key.Y);
                if (cursor.X >= 1)
                {
                    if (!_availableMoves.Contains(cursor) && !_boardChips.ContainsKey(cursor))
                    {
                        point.Add(cursor);
                    }
                }

               //cursor going top left
                cursor = new Point(chip.Key.X - 1, chip.Key.Y - 1);
                if (cursor.X >= 1 && cursor.Y >= 1)
                {
                    if (!_availableMoves.Contains(cursor) && !_boardChips.ContainsKey(cursor))
                    {
                        point.Add(cursor);
                    }
                }

                //cursor going top right

                cursor = new Point(chip.Key.X + 1, chip.Key.Y - 1);
                if (cursor.X <= 8 && cursor.Y >= 1)
                {
                    if (!_availableMoves.Contains(cursor) && !_boardChips.ContainsKey(cursor))
                    {
                        point.Add(cursor);
                    }
                }

                //cursor going bottom left

                cursor = new Point(chip.Key.X - 1, chip.Key.Y + 1);
                if (cursor.X >= 1 && cursor.Y <= 8)
                {
                    if (!_availableMoves.Contains(cursor) && !_boardChips.ContainsKey(cursor))
                    {
                        point.Add(cursor);
                    }
                }

                //cursor goint bottom right
                cursor = new Point(chip.Key.X + 1, chip.Key.Y + 1);
                if (cursor.X <= 8 && cursor.Y <= 8)
                {
                    if (!_availableMoves.Contains(cursor) && !_boardChips.ContainsKey(cursor))
                    {
                        point.Add(cursor);
                    }
                }

               
            }

            if (AvailableAdjacentMovesGenerated != null)
            {
                AvailableAdjacentMovesGenerated(point);
            }

            return point;
        }
        public Board(Dictionary<Point, Chip> boardChips, Color teamColor)
        {
            this.boardChips = new Dictionary<Point, Chip>();
            foreach (var chip in boardChips)
            {
                this.boardChips.Add(chip.Key, chip.Value);
            }
            this.teamColor = teamColor;
            this.opponentColor = Color.White;
            headChance = 0;
            tailsChance = 0;
            initialState = false;

            stateScore = 0;
            biasFactor = 0;

        }
        public Board(Board board)
        {

            this.boardChips = new Dictionary<Point, Chip>();
            foreach (var chip in board.boardChips)
            {
                this.boardChips.Add(chip.Key, chip.Value);
            }
            teamColor = board.teamColor;
            opponentColor = board.opponentColor;
            
            initialState = false;

            stateScore = 0;
            biasFactor = board.biasFactor;

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public Board(Color myMove, Color oppMove, double bias)
        {
            teamColor = myMove;
            opponentColor = oppMove;
            biasFactor = bias;
            boardChips = new Dictionary<Point, Chip>();
            headChance = 0;
            tailsChance = 0;
           
            initialState = false;

            stateScore = 0;



        }

        public void makeInstance()
        {
            Board.getInstance = this;
        }
        public  void computeRank(int mobilityScore)
        {
            int myChipCount = myChips().Count;
            int opponentChipCount = opponentChips().Count;
            int frontiers = FrontierChips(teamColor).Count;
            int stableChipCount = 0;
            int score = 0;

            score =  (myChipCount - opponentChipCount) - frontiers + stableChipCount;

            stateScore = score + mobilityScore;
        }
       
        //create initial state

        public void setUpBoard()
        {
            Debug.WriteLine("Board has been setuped");
            initialState = true;
            addChip(new Point(4, 4), new Chip(Color.White, true));
            
            addChip(new Point(5, 5), new Chip(Color.White, true));
          
            addChip(new Point(5, 4), new Chip(Color.Black, true));
         
            addChip(new Point(4, 5), new Chip(Color.Black, true));

            availableMoves(Color.Black, boardChips);

            initialState = false;
        
        }

        public List<Point> availableMoves(Color color, Dictionary<Point, Chip> boardState)
        {
            
            List<Point> points = new List<Point>();
            Dictionary<Point, Chip> chips = color == teamColor ? myChips() : opponentChips();
            
            
            
            foreach (var state in chips)
            {
                int start = 0;
                int x = 0;
                int y = 0;
                bool endFound = false;
                bool opponentFound = false;
                bool friendlyFound = false;

                #region going to top

                start = state.Key.Y;

                while (!endFound)
                {
                    start--;
                    Point cursor = new Point(state.Key.X, start);
                    if (start == 0)
                    {
                        break;
                    }
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                        else
                        {
                            if (opponentFound)
                            {
                                friendlyFound = true;
                            }
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor) && !friendlyFound)
                    {
                        points.Add(cursor);
                        endFound = true;
                    }
                    else
                    {
                        endFound = true;
                    }

                }
                #endregion

                #region going to bottom
                start = state.Key.Y;
                endFound = false;
                opponentFound = false;
                friendlyFound = false;
                while (!endFound)
                {
                    start++;
                    Point cursor = new Point(state.Key.X, start);
                    if (start >= 9) break;
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                        else
                        {
                            if (opponentFound)
                            {
                                friendlyFound = true;
                            }
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor) && !friendlyFound)
                    {
                        points.Add(cursor);
                        endFound = true;
                    }
                    else
                    {
                        endFound = true;
                    }

                }

                #endregion

                #region going left
                endFound = false;
                opponentFound = false;
                friendlyFound = false;
                start = state.Key.X;


                while (!endFound)
                {
                    start--;
                    Point cursor = new Point(start, state.Key.Y);
                    if (start <= 0) break;
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                        else
                        {
                            if (opponentFound)
                            {
                                friendlyFound = true;
                            }
                        }
                        
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor) && !friendlyFound)
                    {
                        points.Add(cursor);
                        endFound = true;
                    }
                    else
                    {
                        endFound = true;
                    }

                }

                #endregion

                #region going right
                endFound = false;
                opponentFound = false;
                friendlyFound = false;
                start = state.Key.X;

                while (!endFound)
                {
                    start++;
                    Point cursor = new Point(start, state.Key.Y);
                    if (start >= 9) break;
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                        else
                        {
                            if (opponentFound)
                            {
                                friendlyFound = true;
                            }
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor) && !friendlyFound)
                    {
                        points.Add(cursor);
                        endFound = true;
                    }
                    else
                    {
                        endFound = true;
                    }

                }


                #endregion

                #region diagonal top left
                
                endFound = false;
                opponentFound = false;
                friendlyFound = false;

                x = state.Key.X;
                y = state.Key.Y;

                while (!endFound)
                {
                    x--;
                    y--;
                    if (x <= 0 || y <= 0) break;
                    Point cursor = new Point(x, y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;

                        }
                        else
                        {
                            if (opponentFound)
                            {
                                friendlyFound = true;
                            }
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor) && !friendlyFound)
                    {
                        points.Add(cursor);
                        endFound = true;
                    }
                    else
                    {
                        endFound = true;
                    }

                }

                #endregion

                #region top right
                endFound = false;
                opponentFound = false;
                friendlyFound = false;

                x = state.Key.X;
                y = state.Key.Y;

                
                while (!endFound)
                {
                    x++;
                    y--;

                    if (x >= 9 || y <= 0) break;
                    Point cursor = new Point(x, y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;

                        }
                        else
                        {
                            if (opponentFound)
                            {
                                friendlyFound = true;
                            }
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor) && !friendlyFound)
                    {
                        points.Add(cursor);
                        endFound = true;
                    }
                    else
                    {
                        endFound = true;
                    }

                }
                #endregion

                #region bottom left
                endFound = false;
                opponentFound = false;
                friendlyFound = false;
                x = state.Key.X;
                y = state.Key.Y;

                while (!endFound)
                {
                    x--;
                    y++;

                    if (x <= 0 || y >= 9) break;
                    Point cursor = new Point(x, y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                        else
                        {
                            if (opponentFound)
                            {
                                friendlyFound = true;
                            }
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor) && !friendlyFound)
                    {
                        points.Add(cursor);
                        endFound = true;
                    }
                    else
                    {
                        endFound = true;
                    }

                }
                #endregion

                #region bottom right
                endFound = false;
                opponentFound = false;
                friendlyFound = false;

                x = state.Key.X;
                y = state.Key.Y;

                while (!endFound)
                {
                    x++;
                    y++;

                    if (x >= 9 || y >= 9) break;
                    Point cursor = new Point(x, y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                        else
                        {
                            if (opponentFound)
                            {
                                friendlyFound = true;
                            }
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor) && !friendlyFound)
                    {
                        points.Add(cursor);
                        endFound = true;
                    }
                    else
                    {
                        endFound = true;
                    }

                }
                #endregion

            }

           

            if (AvailableMovesGenerated != null)
            {
                AvailableMovesGenerated(points);
            }

            Debug.WriteLine("Available points for move: "+points.Count);
            return points;
        }

        public Dictionary<Point, Chip> myChips()
        {
            Dictionary<Point, Chip> chips = new Dictionary<Point,Chip>();
            foreach (var chip in boardChips)
            {
                if (chip.Value.chipColor == teamColor)
                {
                    if (!chips.ContainsKey(chip.Key))
                        chips.Add(chip.Key, chip.Value);
                }
            }
            return chips;
        }

        public Dictionary<Point, Chip> opponentChips()
        {
            Dictionary<Point, Chip> chips = new Dictionary<Point,Chip>();
            foreach (var chip in boardChips)
            {
                if (chip.Value.chipColor != teamColor)
                {
                    if(!chips.ContainsKey(chip.Key))
                        chips.Add(chip.Key, chip.Value);
                }
            }
            return chips;
        }



        public Dictionary<Point, Chip> StableChips(Color color)
        {
            Dictionary<Point, Chip> mine = color == teamColor ? myChips() : opponentChips();
            Dictionary<Point, Chip> chips = new Dictionary<Point, Chip>();
            foreach (var chip in mine)
            {
                if (isStable(chip.Key))
                {
                    chips.Add(chip.Key, chip.Value);
                }
            }
            return chips;

        }

        

        public bool isStable(Point point)
        {
            bool _isStable = false;
            if (isFrontier(point))
            {
                return false;
            }

            if (isBoundary(point) || 
                inFilledRow(point))
            {
                return true;  
            }
            if (point.X > 0 && point.X <= 8 && point.Y > 0 && point.Y <= 8)
            {
                if (isStable(new Point(point.X - 1, point.Y)) &&
                      isStable(new Point(point.X + 1, point.Y)) &&
                      isStable(new Point(point.X, point.Y + 1)) &&
                      isStable(new Point(point.X, point.Y - 1)))
                {
                    return true;
                }
            }
            return _isStable;
        }

        public bool isBoundary(Point point)
        {
            return  point == new Point(1, 1) || 
                    point == new Point(1, 8) || 
                    point == new Point(8, 1) || 
                    point == new Point(8, 8) || 
                    point == new Point(2, 1) ||
                    point == new Point(1, 2) ||
                    point == new Point(1, 7) ||
                    point == new Point(2, 8) ||
                    point == new Point(8, 7) ||
                    point == new Point(7, 8);
        }



        public bool inFilledRow(Point point)
        {
            int row = point.Y;
            int col = 1;
            

            for (col = 0; col < 8; col++)
            {
                Point cursor = new Point(col, row);
                Point oppositeCursor = new Point(point.X, col);
                if (!boardChips.ContainsKey(cursor) && !boardChips.ContainsKey(oppositeCursor))
                {
                    return false;    
                }
               
            }
            return true;
        }

        

        public bool isFrontier(Point point)
        {
            bool isFrontier = false;
            //check going top
            Point cursor = new Point(point.X, point.Y - 1);
            if (!boardChips.ContainsKey(cursor) && cursor.Y >= 1)
            {
                return true;
            }

            //check going bottom
            cursor = new Point(point.X, point.Y + 1);
            if (!boardChips.ContainsKey(cursor) && cursor.Y <= 8)
            {
                return true;
            }

            //check going left

            cursor = new Point(point.X - 1, point.Y);
            if (!boardChips.ContainsKey(cursor) && cursor.X >= 1)
            {
                return true;
            }

            //check going right
            cursor = new Point(point.X + 1, point.Y);
            if (!boardChips.ContainsKey(cursor) && cursor.X <= 8)
            {
                return true;
            }

            //check top-left
            cursor = new Point(point.X - 1, point.Y - 1);
            if (!boardChips.ContainsKey(cursor) && cursor.X >= 1 && cursor.Y >= 1)
            {
                return true;
            }

            //check top right
            cursor = new Point(point.X + 1, point.Y - 1);
            if (!boardChips.ContainsKey(cursor) && cursor.X <= 8 && cursor.Y >= 1)
            {
                return true;
            }

            //check bottom left
            cursor = new Point(point.X - 1, point.Y + 1);
            if (!boardChips.ContainsKey(cursor) && cursor.X >= 1 && cursor.Y <= 8)
            {
                return true;
            }


            //check bottom right
            cursor = new Point(point.X + 1, point.Y + 1);
            if (!boardChips.ContainsKey(cursor) && cursor.X <= 8 && cursor.Y <= 8)
            {
                return true;
            }


            return isFrontier;
        }
        public Dictionary<Point, Chip> FrontierChips(Color color)
        {
            Dictionary<Point, Chip> mine = color == teamColor ?  myChips() : opponentChips();
            Dictionary<Point, Chip> frontiers = new Dictionary<Point, Chip>();
            foreach (var chip in mine)
            {
                if (isFrontier(chip.Key))
                {
                    frontiers.Add(chip.Key, chip.Value);
                }
            }
            return frontiers;
        }
        
        public void addChip(Point point, Chip chip)
        {
            try
            {
                boardChips.Add(point, chip);
                instantiateMove(point.X, point.Y, chip.chipColor);
            }
            catch (Exception t) {
            
            
            }
            if (ChipAdded != null)
            {
                ChipAdded(point, chip);
            }

            availableMoves(chip.chipColor == Color.Black ? Color.White : Color.Black, boardChips);
            
            
        }


        #region seeking region
        public void changeState(int x, int y, bool state, Color color)
        {

            Point p = new Point(x, y);
            boardChips[p].changeState(color, state);
        }

        public void instantiateMove(int x, int y, Color color)
        {

            //Seek traversed coordinates first
            //TODO
            //----------------------------------
            Point p = new Point(x, y);

            

            checkStateDirect(p, color, Direction.up);
            checkStateDirect(p, color, Direction.down);
            checkStateDirect(p, color, Direction.left);
            checkStateDirect(p, color, Direction.right);

            checkStateDirect(p, color, Direction.topLeft);
            checkStateDirect(p, color, Direction.topRight);
            checkStateDirect(p, color, Direction.bottomLeft);
            checkStateDirect(p, color, Direction.bottomRight);
            

            changeState(x, y, true, color);

            

        }

       

        public void checkStateDirect(Point p, Color color, int direction)
        {
            switch (direction)
            {
                case Direction.up: checkStatePoint(p, color, Direction.up, 'Y');
                    break;
                case Direction.down: checkStatePoint(p, color, Direction.down, 'Y');
                    break;
                case Direction.left: checkStatePoint(p, color, Direction.left, 'X');
                    break;
                case Direction.right: checkStatePoint(p, color, Direction.right, 'X');
                    break;
            
                ////check state for diagonal points
                case Direction.topLeft: checkDiagonalStatePointTopLeft(p, color);
                    break;
                case Direction.topRight: checkDiagonalStatePointTopRight(p, color);
                    break;
                case Direction.bottomLeft: checkDiagonalStatePointBottomLeft(p, color);
                    break;
                case Direction.bottomRight: checkDiagonalStatePointBottomRight(p, color);
                    break;
            }

        }

        #region DiagonalRegion
        public void checkDiagonalStatePointTopRight(Point p, Color color)
        {
            List<Point> points = new List<Point>();
            Point startingPoint = p;
            bool foundTeamColor = false;
            bool changeState = true;
            bool first = true;

            int CoordEnd = 8;  //coord is X-based any Y, but X needs to stop at 8


            while (changeState)
            {
                if (boardChips.ContainsKey(startingPoint) && startingPoint.X < CoordEnd)
                {
                    //board chip found

                    if (boardChips[startingPoint].chipColor == color && !first)
                    {
                        foundTeamColor = true;
                        changeState = false;
                    }
                    else
                    {
                        points.Add(startingPoint);
                    }

                    startingPoint = new Point(startingPoint.X + 1, startingPoint.Y - 1);

                }
                else
                {
                    changeState = false; // chip missing end of turn
                }
                first = false;
            }

            if (foundTeamColor)
            {
                foreach (Point _p in points)
                {
                    boardChips[_p].changeState(color, true);
                    if (ChipFlipped != null)
                    {
                        ChipFlipped(_p, boardChips[_p]);
                    }

                }
            }
        }
        public void checkDiagonalStatePointTopLeft(Point p, Color color)
        {
            List<Point> points = new List<Point>();
            Point startingPoint = p;
            bool foundTeamColor = false;
            bool changeState = true;
            bool first = true;

            int CoordEnd = 1;  //coord is Y-based any X, but Y needs to stop at 1


            while (changeState)
            {
                if (boardChips.ContainsKey(startingPoint) && startingPoint.Y >= CoordEnd)
                {
                    //board chip found

                    if (boardChips[startingPoint].chipColor == color && !first)
                    {
                        foundTeamColor = true;
                        changeState = false;
                    }
                    else
                    {
                        points.Add(startingPoint);
                    }

                    startingPoint = new Point(startingPoint.X - 1, startingPoint.Y - 1);

                }
                else
                {
                    changeState = false; // chip missing end of turn
                }
                first = false;
            }

            if (foundTeamColor)
            {
                foreach (Point _p in points)
                {
                    boardChips[_p].changeState(color, true);
                    if (ChipFlipped != null)
                    {
                        ChipFlipped(_p, boardChips[_p]);
                    }

                }
            }

        }
        public void checkDiagonalStatePointBottomRight(Point p, Color color)
        {
            List<Point> points = new List<Point>();
            Point startingPoint = p;
            bool foundTeamColor = false;
            bool changeState = true;
            bool first = true;

            int CoordEnd = 8;  //coord is X-based any Y, but X needs to stop at 8


            while (changeState)
            {
                if (boardChips.ContainsKey(startingPoint) && startingPoint.X < CoordEnd)
                {
                    //board chip found

                    if (boardChips[startingPoint].chipColor == color && !first)
                    {
                        foundTeamColor = true;
                        changeState = false;
                    }
                    else
                    {
                        points.Add(startingPoint);
                    }

                    startingPoint = new Point(startingPoint.X+1, startingPoint.Y+1);

                }
                else
                {
                    changeState = false; // chip missing end of turn
                }
                first = false;
            }

            if (foundTeamColor)
            {
                foreach (Point _p in points)
                {
                    boardChips[_p].changeState(color, true);
                    if (ChipFlipped != null)
                    {
                        ChipFlipped(_p, boardChips[_p]);
                    }
                   
                }
            }
        }

        public void checkDiagonalStatePointBottomLeft(Point p, Color color)
        {
            List<Point> points = new List<Point>();
            Point startingPoint = p;
            bool foundTeamColor = false;
            bool changeState = true;
            bool first = true;

            int CoordEnd = 1;  //coord is X-based any Y, but X needs to stop at 1


            while (changeState)
            {
                if (boardChips.ContainsKey(startingPoint) && startingPoint.X >= CoordEnd)
                {
                    //board chip found

                    if (boardChips[startingPoint].chipColor == color && !first)
                    {
                        foundTeamColor = true;
                        changeState = false;
                    }
                    else
                    {
                        points.Add(startingPoint);
                    }

                    startingPoint = new Point(startingPoint.X - 1, startingPoint.Y + 1);

                }
                else
                {
                    changeState = false; // chip missing end of turn
                }
                first = false;
            }

            if (foundTeamColor)
            {
                foreach (Point _p in points)
                {
                    boardChips[_p].changeState(color, true);
                    if (ChipFlipped != null)
                    {
                        ChipFlipped(_p, boardChips[_p]);
                    }

                }
            }
            
        }

        #endregion

        #region Check state Version 
        public void checkStatePoint(Point p, Color c, int direction, char coordinate)
        {
            int start = coordinate == 'Y' ? p.Y : p.X;
            int end = direction == Direction.up || direction == Direction.right ? 1 : 8;

            
            List<Point> points = new List<Point>();

            bool changeState    = true;
            bool foundTeamColor = false;
            bool first          = true;



            //record all chips that needs to be switched to team color
            while (changeState)
            {
                Point _p = coordinate == 'Y' ? new Point(p.X, start) : new Point(start, p.Y);
                bool checkCoords = false;
                //the initial point whereas the cursor will start

                if (direction == Direction.up || direction == Direction.right)
                {
                    checkCoords = start >= end;
                }
                else
                {
                    checkCoords = start <= end;
                }
                

                if (boardChips.ContainsKey(_p) && checkCoords)
                {
                    Chip chip = boardChips[_p];
                    if (chip.chipColor == c && !first)
                    { //if team color and chip color is the same, escape from loop
                        foundTeamColor = true;
                        changeState = false;
                    }
                    else
                    {
                        //else record point for switch
                        points.Add(_p);
                    }
                    first = false;
                }
                else
                {
                    //escape from loop if point not visible
                    changeState = false;
                }
                if (direction == Direction.up || direction == Direction.right)
                {
                    start--;
                }
                else
                {
                    start++;
                }
            }

            //switch all recorded points
            if (foundTeamColor)
            {
                foreach (Point _p in points)
                {
                    boardChips[_p].changeState(c, true);
                    if (ChipFlipped != null)
                    {
                        ChipFlipped(_p, boardChips[_p]);
                    }
                }
            }




        }
        #endregion


        #endregion

    }
}
