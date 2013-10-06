using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace ReversiLibrary.GameModels
{

    //create event to fetch the chips in the board
    public delegate void ChipAddedHandler(Point point, Chip chip);
    public delegate void ChipFlippedHandler(Point point, Chip chip);
    public delegate void AvailableMovesGeneratedHandler(List<Point> points);
    


    public class Board
    {
        public event ChipAddedHandler ChipAdded;
        public event ChipFlippedHandler ChipFlipped;
        public event AvailableMovesGeneratedHandler AvailableMovesGenerated;

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
        public bool initialState { get; set; }
        public Color teamColor { get; set; }
        
        public Board()
        {
            boardChips = new Dictionary<Point, Chip>();
            teamColor = Color.Black;
            Board.getInstance = this;
            headChance = 0;
            tailsChance = 0;
            initialState = false;
        }
        public Board(Dictionary<Point, Chip> boardChips, Color teamColor)
        {
            this.boardChips = boardChips;
            this.teamColor = teamColor;
            headChance = 0;
            tailsChance = 0;
            initialState = false;
        }
        public Board(Board board)
        {
            Board.getInstance = board;
            initialState = false;
        }


        //create initial state

        public void setUpBoard()
        {
            Debug.WriteLine("Board has been setuped");
            initialState = true;
            addChip(new Point(4, 4), new Chip(Color.Yellow, true));
            
            addChip(new Point(5, 5), new Chip(Color.Yellow, true));
          
            addChip(new Point(5, 4), new Chip(Color.Black, true));
         
            addChip(new Point(4, 5), new Chip(Color.Black, true));

            availableMoves(this.teamColor, boardChips);

            initialState = false;
        
        }

        public void declareMove()
        {

        }

        public List<Point> availableMoves(Color color, Dictionary<Point, Chip> boardState)
        {
            
            List<Point> points = new List<Point>();
            Dictionary<Point, Chip> chips = color == teamColor ? myChips() : opponentChips();
            
            
            
            foreach (var state in chips)
            {
                int end = 0;
                int start = 0;
                int x = 0;
                int y = 0;
                bool endFound = false;
                bool opponentFound = false;


                #region going to top
                end = 1;
                start = state.Key.Y;

                while (!endFound)
                {
                    start--;
                    Point cursor = new Point(state.Key.X, start);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor))
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
                end = 8;
                start = state.Key.Y;
                endFound = false;
                opponentFound = false;

                while (!endFound)
                {
                    start++;
                    Point cursor = new Point(state.Key.X, start);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor))
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
                start = state.Key.X;
                end = 1;

                while (!endFound)
                {
                    start--;
                    Point cursor = new Point(start, state.Key.Y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor))
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
                start = state.Key.X;
                end = 8;

                while (!endFound)
                {
                    start++;
                    Point cursor = new Point(start, state.Key.Y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor))
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

                x = state.Key.X;
                y = state.Key.Y;

                while (!endFound)
                {
                    x--;
                    y--;
                    Point cursor = new Point(x, y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor))
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

                x = state.Key.X;
                y = state.Key.Y;

                while (!endFound)
                {
                    x++;
                    y--;
                    Point cursor = new Point(x, y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor))
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

                x = state.Key.X;
                y = state.Key.Y;

                while (!endFound)
                {
                    x--;
                    y++;
                    Point cursor = new Point(x, y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor))
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

                x = state.Key.X;
                y = state.Key.Y;

                while (!endFound)
                {
                    x++;
                    y++;
                    Point cursor = new Point(x, y);
                    if (boardChips.ContainsKey(cursor))
                    {
                        if (boardChips[cursor].chipColor != color)
                        {
                            opponentFound = true;
                            //continue;
                        }
                    }
                    else if (opponentFound && !boardChips.ContainsKey(cursor))
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

        

        public void addChip(Point point, Chip chip)
        {
            

            boardChips.Add(point, chip);  
            instantiateMove(point.X, point.Y, chip.chipColor);

            if (ChipAdded != null)
            {
                ChipAdded(point, chip);
            }

            if (!initialState)
            {
                availableMoves(chip.chipColor, boardChips);
            }
            
            
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
                    checkCoords = start < end;
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
