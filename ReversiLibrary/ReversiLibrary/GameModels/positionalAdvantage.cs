using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ReversiLibrary.GameModels
{
    public class positionalAdvantage
    {
        public static Dictionary<Point, int> positions
        {
            get
            {   int i;
                Dictionary<Point, int> points = new Dictionary<Point, int>();
                points.Add(new Point(1, 1), 99);
                points.Add(new Point(1, 2), -8);
                points.Add(new Point(1, 3), 8);
                points.Add(new Point(1, 4), 6);
                points.Add(new Point(1, 5), 6);
                points.Add(new Point(1, 6), 8);
                points.Add(new Point(1, 7), -8);
                points.Add(new Point(1, 8), 99);

                points.Add(new Point(2, 1), -8);
                points.Add(new Point(2, 2), -24);
                points.Add(new Point(2, 3), -4);
                points.Add(new Point(2, 4), -3);
                points.Add(new Point(2, 5), -3);
                points.Add(new Point(2, 6), -4);
                points.Add(new Point(2, 7), -24);
                points.Add(new Point(2, 8), -8);

                points.Add(new Point(3, 1), 8);
                points.Add(new Point(3, 2), -4);
                points.Add(new Point(3, 3), 7);
                points.Add(new Point(3, 4), 4);
                points.Add(new Point(3, 5), 4);
                points.Add(new Point(3, 6), 7);
                points.Add(new Point(3, 7), -4);
                points.Add(new Point(3, 8), 8);

                points.Add(new Point(4, 1), 6);
                points.Add(new Point(4, 2), -3);
                points.Add(new Point(4, 3), 4);
                points.Add(new Point(4, 4), 0);
                points.Add(new Point(4, 5), 0);
                points.Add(new Point(4, 6), 4);
                points.Add(new Point(4, 7), -3);
                points.Add(new Point(4, 8), 6);

                points.Add(new Point(5, 1), 6);
                points.Add(new Point(5, 2), -3);
                points.Add(new Point(5, 3), 4);
                points.Add(new Point(5, 4), 0);
                points.Add(new Point(5, 5), 0);
                points.Add(new Point(5, 6), 4);
                points.Add(new Point(5, 7), -3);
                points.Add(new Point(5, 8), 6);

                points.Add(new Point(6, 1), 8);
                points.Add(new Point(6, 2), -4);
                points.Add(new Point(6, 3), 7);
                points.Add(new Point(6, 4), 4);
                points.Add(new Point(6, 5), 4);
                points.Add(new Point(6, 6), 7);
                points.Add(new Point(6, 7), -4);
                points.Add(new Point(6, 8), 8);

                points.Add(new Point(7, 1), -8);
                points.Add(new Point(7, 2), -24);
                points.Add(new Point(7, 3), -4);
                points.Add(new Point(7, 4), -3);
                points.Add(new Point(7, 5), -3);
                points.Add(new Point(7, 6), -4);
                points.Add(new Point(7, 7), -24);
                points.Add(new Point(7, 8), -8);

                points.Add(new Point(8, 1), 99);
                points.Add(new Point(8, 2), -8);
                points.Add(new Point(8, 3), 8);
                points.Add(new Point(8, 4), 6);
                points.Add(new Point(8, 5), 6);
                points.Add(new Point(8, 6), 8);
                points.Add(new Point(8, 7), -8);
                points.Add(new Point(8, 8), 99);

                

                    return points;
            }
        }
    }
}
