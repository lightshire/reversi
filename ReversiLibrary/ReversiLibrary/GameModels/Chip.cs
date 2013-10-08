using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ReversiLibrary.GameModels
{
    
    

    public class Chip : ICloneable
    {
        public Color chipColor = Color.Black;
        public bool state = false;
        public bool empty = false;

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

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void changeState(Color color, bool state)
        {
            this.empty = false;
            this.chipColor = color;
            this.state = state;
        }

        



    }
}
