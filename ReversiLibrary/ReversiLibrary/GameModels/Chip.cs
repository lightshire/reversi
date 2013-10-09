using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ReversiLibrary.GameModels
{
    public class Chip
    {
        public Color chipColor { get; set; }
        public bool state { get; set; }
        public bool empty { get; set; }

        public Chip()
        {
            chipColor = Color.Black;
            state = false;
            empty = true;
        }

        public Chip(Color color, bool state)
        { 
            this.chipColor = color;
            this.state = state;
            empty = false;

        }

        public void changeState(Color color, bool state)
        {
            this.empty = false;
            this.chipColor = color;
            this.state = state;
        }
    }
}
